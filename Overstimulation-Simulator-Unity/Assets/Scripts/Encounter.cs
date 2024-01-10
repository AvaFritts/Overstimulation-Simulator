// Creator: Ava Fritts
//Date Created: May 17th 2022

// Last edited: October 22nd, 2023
// Description: The base script for all encounters.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    //VARIABLES//

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

    public Convo_Task_Obj[] goodTemplate;

    [Tooltip("Even good conversations can be stimulation. The value should be less than a bad one, though.")]
    public float winningPunishment;
    public float losingPunishment;
    //public UnityEvent Example;

    [Space(10)]

    //[Tooltip("How many questions does the player have to answer to finish the encounter?")]
    //public int numberOfQuestions = 0; //Bosses usually have multiple: everyone else has one.

    public Sprite[] associatedSprite; //the sprites for the encounter. Might make them only have one scary sprite idk.
    public string[] encounterText; // the text used for the encounter
    [Tooltip("The numbers should range from 1-4 and be ordered according to the encounter text")]
    public int[] correctAnswer; //the value of the correct answer.

    [Space(10)]

    public Convo_Task_Obj[] scaryTemplate;

    /*public string[] scaryEncounterText; // the text used for the encounter
    public int[] scaryCorrectAnswer; //the value of the correct answer.
    public Sprite[] associatedScarySprite; //the sprites used for the scary encounters*/



    [Header("Set Dynamically")]
    public bool canActivate; //is the player in range?
    public bool scaryMode;
    public int questionsAnswered;

    public string activeString;
    public int activeAnswer;
    public Sprite activeSprite;

    private void Update()
    {
        if (canActivate)
        {
            if (GameManager.GM.gameState == GameManager.gameStates.Playing || GameManager.GM.isTesting)
            {
                if (Input.GetButtonDown("Jump")) //if the player interacts with it.
                {
                    //Initiate human interaction.
                    PickAnswers(); //starts an encounter.
                }
            }
            else if (GameManager.GM.gameState == GameManager.gameStates.Battle)
            {
                if (conversationStarter.finishedFight == true)
                {
                    conversationStarter.finishedFight = false;
                    SendConversationData();
                }
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

    public void CorrectResponse() //Only used for Bosses
    {
        questionsAnswered++;
       /* if(conversationStarter.currentTemplate.nextQuestion = null)
        {

        }*/
        if (questionsAnswered >= conversationStarter.currentTemplate.encounterText.Length) //change to look for null linked list
        {
            GameManager.GM.playerWon = true;
        }
        else //only happens in bosses
        {
            PickAnswers();
        }

    } //end correct response

    public void IncorrectResponse() //Only used for Bosses
    {
        meterChecker.stimulationGauge.value += losingPunishment;

        //set game state to "Playing"  
        GameManager.GM.gameState = GameManager.gameStates.Playing;

        //deactivate the encounter camera 
        battleCamera.SetActive(false);

        battleCanvas.SetActive(false);

        if (!isBoss)
        {
            associatedTask.UpdateTask(); //wait why do I have this in a Boss Exclusive piece?
        }
        else
        {
            questionsAnswered = 0; //reset the boss if you failed it.
        }
        //maybe put some text in?
    } //end incorrect response

    public void BattleStart()
    {
        //Put in here EVERYTHING in the "Pick Answers" task that SHOULD ONLY BE DONE ONCE PER BATTLE.
        if (!battleCanvas.activeSelf) //if the encounter isn't already active. MOVE TO BATTLE START
        {
            battleCanvas.SetActive(true);
            battleCamera.SetActive(true);
        }

    }

    //pick the answer for the fight.
    public void PickAnswers() //Way too cluttered.
    {
        //I think I should make the "first time set-up" on a different piece.
        if (!battleCanvas.activeSelf) //if the encounter isn't already active. MOVE TO BATTLE START
        {
            battleCanvas.SetActive(true);
            battleCamera.SetActive(true);
        }

        if (!isBoss) //One day I'll have "Scary Bosses" (if "questions answered = 0", do this)
        {
            //if the gague is over 75%
            if (meterChecker.stimulationGauge.value >= (meterChecker.stimulationGauge.maxValue * 3 / 4))
            {
                scaryMode = true;

                int taskPicked = Random.Range(0, scaryTemplate.Length);
                conversationStarter.currentTemplate = scaryTemplate[taskPicked];
                /*int taskPicked = Random.Range(0, scaryEncounterText.Length); //picks a random task //Change to the template.
                activeString = scaryEncounterText[taskPicked];
                activeAnswer = scaryCorrectAnswer[taskPicked];
                activeSprite = associatedScarySprite[taskPicked];*/
            }
            else
            {
                scaryMode = false;

                int taskPicked = Random.Range(0, goodTemplate.Length);
                //Debug.Log(taskPicked);
                conversationStarter.currentTemplate = goodTemplate[taskPicked];
            }

        } //end "If encounter isn't a boss"
        else //if it is a boss encounter
        {
            conversationStarter.numberOfQuestions = questionsAnswered;
            conversationStarter.currentTemplate = goodTemplate[0];
        }
        conversationStarter.StartEncounter(); //starts an encounter.
        //Example.Invoke(); //
    }//end pick answers

    public void SendConversationData()
    {
        //set game state to "Playing"
        if(isBoss)
        {
            if (conversationStarter.currentPunishment.Equals(0))
            {
                CorrectResponse();
            }
            else
            {
                IncorrectResponse();
            } 
        }
        else
        {
            GameManager.GM.gameState = GameManager.gameStates.Playing;
            meterChecker.stimulationGauge.value += conversationStarter.currentPunishment;
            //deactivate the encounter camera
            battleCamera.SetActive(false);
            questionsAnswered = 0;
            associatedTask.UpdateTask();

            battleCanvas.SetActive(false);
        }
    }
} //end Encounter
