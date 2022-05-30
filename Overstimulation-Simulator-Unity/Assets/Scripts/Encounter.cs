// Creator: Ava Fritts
//Date Created: May 17th 2022

// Last edited: May 20th 2022
// Description: The base script for all encounters.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    //VARIABLES//
    GameManager GM;

    [Header("Set in Inspector")]
    public Task associatedTask;
    public Encounter_Manager conversationStarter;
    public GameObject battleCamera; //the camera for encounters
    public GameObject battleCanvas; //if the canvas is always enabled it leads to weird issues.
    public Overstimulation meterChecker; //needed to pause the meter
    //[Tooltip("Put the item this is attached to here")]
    //public GameObject encounterDatabase;

    [Space(10)]

    public bool isBoss; //is this a boss encounter?

    [Tooltip("Even good conversations can be stimulation. The value should be less than a bad one, though.")]
    public float winningPunishment;
    public float losingPunishment;

    [Space(10)]

    [Tooltip("How many questions does the player have to answer to finish the encounter?")]
    public int numberOfQuestions = 1; //Bosses usually have multiple: everyone else has one.

    public Sprite[] associatedSprite; //the sprites for the 
    public string[] encounterText; // the text used for the encounter
    [Tooltip("The numbers should range from 1-4 and be ordered according to the encounter text")]
    public int[] correctAnswer; //the value of the correct answer.

    [Space(10)]

    public string[] scaryEncounterText; // the text used for the encounter
    public int[] scaryCorrectAnswer; //the value of the correct answer.

    
    
    [Header("Set Dynamically")]
    public bool canActivate; //is the player in range?
    public bool scaryMode;
    public int questionsAnswered;

    public string activeString;
    public int activeAnswer;
    public Sprite activeSprite;

    private void Update()
    {
        if (canActivate && (GameManager.GM.gameState == GameManager.gameStates.Playing || GameManager.GM.isTesting))
        {
            if (Input.GetButtonDown("Fire2")) //if the player interacts with it.
            {
                //Initiate human interaction.
                PickAnswers(); //starts an encounter.
            }
        }
    }//end Update

    private void OnTriggerEnter(Collider collision)
    {
        GameObject colGO = collision.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        GameObject colGO = collision.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            canActivate = false;
        }
    }

    //Prevents the player from activating the encounter on the other side of the map.
    private void OnDisable()
    {
        canActivate = false;
    }

    public void CorrectResponse()
    {
        questionsAnswered++;
        if(questionsAnswered >= numberOfQuestions)
        {
            if (isBoss)
            {
                GameManager.GM.playerWon = true;
            }
            meterChecker.stimulationGauge.value += winningPunishment;
            
            //set game state to "Playing"
            GameManager.GM.gameState = GameManager.gameStates.Playing;
            
            //deactivate the encounter camera
            battleCamera.SetActive(false);

            associatedTask.UpdateTask();

            battleCanvas.SetActive(false);
            //maybe put some text in?
        }
        else //only happens in bosses
        {
            PickAnswers();
        }

        meterChecker.paused = false;
    } //end correct response

    public void IncorrectResponse()
    {  
        meterChecker.stimulationGauge.value += losingPunishment;
    
        //set game state to "Playing"  
        GameManager.GM.gameState = GameManager.gameStates.Playing;

        //deactivate the encounter camera 
        battleCamera.SetActive(false);
        
        battleCanvas.SetActive(false);

        if (!isBoss)
        {
            associatedTask.UpdateTask();
        }
        else
        {
            questionsAnswered = 0; //reset the boss if you failed it.
        }
        //maybe put some text in?

        meterChecker.paused = false;
    } //end incorrect response

    //pick the answer for the fight.
    public void PickAnswers()
    {
        if (!battleCanvas.activeSelf) //if the encounter isn't already active
        {
            battleCanvas.SetActive(true);
            meterChecker.paused = true;
        }
        

        if (!isBoss) 
        {
            //if the gague is over 75%
            if (meterChecker.stimulationGauge.value >= (meterChecker.stimulationGauge.maxValue * 3 / 4))
            {
                scaryMode = true;

                int taskPicked = Random.Range(0, scaryEncounterText.Length); //picks a random task
                activeString = scaryEncounterText[taskPicked];
                activeAnswer = scaryCorrectAnswer[taskPicked];
                //activeSprite = associatedSprite[taskPicked * 2];
            }
            else
            {
                scaryMode = false;

                int taskPicked = Random.Range(0, encounterText.Length); //picks a random task
                activeString = encounterText[taskPicked];
                activeAnswer = correctAnswer[taskPicked];
                //activeSprite = associatedSprite[taskPicked];
                
            }

        } //end "If encounter isn't a boss"
        else //if it is a boss encounter
        {      
            activeString = encounterText[questionsAnswered];    
            activeAnswer = correctAnswer[questionsAnswered];  
            //activeSprite = associatedSprite[0];  
        } 
        conversationStarter.StartEncounter(this.gameObject); //starts an encounter.
    }//end pick answers

} //end Encounter
