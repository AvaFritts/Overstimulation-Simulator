// Creator: Ava Fritts
//Date Created: May 16th 2022

// Last edited: July 5th, 2025
// Description: The script to manage any encounter in a given level.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter_Manager : MonoBehaviour
{
    //Variables//
    [Header("Set in Inspector")]
    public GameObject[] theFourButtons; //the four buttons used in the encounters
    public Image[] theFourFaces;
    public GameObject background;
    public GameObject battleButton; //the button that appears to end the encounter
    public Text battleButtonText;
    public float rotationSpeed;

    [Space(5)]

    [Tooltip("The strings here should have the button labels for the 'good' responses")]
    public Sprite[] theCalmFaces;
    // public string[] theCalmChoices;
    [Tooltip("The strings here should have the button labels for the 'scary mode' responses")]
    public Sprite[] theStressedFaces;
    //public string[] theAnxiousChoices;

    [Space(5)]

    public Text battleQuestion;

    [Header("Set Dynamically")]
    public int numberOfQuestions = 0; //Bosses usually have multiple: everyone else has one.
    public float rotatZ;

    [Space(10)]

    //public Encounter currentBattle; //Change this so that it is the game object
    public Convo_Task_Obj currentTemplate;
    public Sprite currentSprite;

    public int currentPunishment;
    public GameObject enemySprite;
    public bool finishedFight = false; //Used to tell the Encounter when to disable the fight.


    public void Update() //All this does is rotate the Spiral background
    {
        if (!GameManager.GM.stilumationReducer) //if IRL stimultaion Reduction is turned off
        {
            rotatZ += Time.deltaTime * rotationSpeed; //adding to a rotation counter over time

            if (rotatZ < -360.0f || rotatZ > 360.0f) //if it has gone through a loop, reset it
            {
                rotatZ = 0.0f;
            }
                background.transform.localRotation = Quaternion.Euler(0, 0, rotatZ); //apply to the background
        }
    }

    //for whenever an encounter is started
    public void StartEncounter() //I am already passing the encounter... Wow.
    {
        battleButton.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            //theFourButtons[i].text = theAnxiousChoices[i];
            theFourButtons[i].SetActive(true);
        }
        //enable Battle Mode if it isn't in it already. (Update on Oct. 22: Why do I have it like this?)
        if (GameManager.GM.gameState != GameManager.gameStates.Battle)
        {
            GameManager.GM.gameState = GameManager.gameStates.Battle;
            //currentBattle = encounterBase.GetComponent<Encounter>(); //Change this so that the Scriptable Objects are used instead.

            //currentBattle.battleCamera.SetActive(true); //The Encounter Manager shouldn't deal with this if the encounter is what has the reference to it.

            if (currentTemplate.scaryMode) //if scary mode is active
            {
                for (int i = 0; i < 4; i++)
                {
                    //theFourButtons[i].text = theAnxiousChoices[i];
                    theFourFaces[i].sprite = theStressedFaces[i];
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    //theFourButtons[i].text = theCalmChoices[i];
                    theFourFaces[i].sprite = theCalmFaces[i];
                }
            } //end else;
        }//end If (GameManager's state isn't in battle)


        //put up the current question, set the correct answer, and change the sprite accordingly.
        battleQuestion.text = currentTemplate.encounterText[0];
        currentSprite = currentTemplate.encounterSprite[0]; //this line only exists because it was giving me weird errors otherwise.
        enemySprite.GetComponent<Image>().sprite = currentSprite;
        battleButtonText.text = "How do you feel?";
        battleButtonText.fontStyle = FontStyle.Italic;
    }

    //Wait how am I going to do this?! I need to send the information *back* to the Encounter task
    //so that it can send the task information back to the To-Do list AND the Overstimulation meter!
    //...Man my code is really entangled.
    //One idea: Put a communicator script *on a special button* that I use to close out the encounter.
    public void SubmittedAnswer (int choice)
    {
        currentPunishment = currentTemplate.punishment[choice];
        if (currentTemplate.isABoss && currentPunishment == 0)
        {
            finishedFight = true;
        }
        else
        {
            battleQuestion.text = currentTemplate.responses[choice];
            for (int i = 0; i < 4; i++)
            {
                //theFourButtons[i].text = theAnxiousChoices[i];
                theFourButtons[i].SetActive(false);
            }
            battleButton.SetActive(true);
            battleButtonText.text = "+" + currentPunishment.ToString() + " Stimulation"; //Show the effect of their actions
            battleButtonText.fontStyle = FontStyle.BoldAndItalic;
        }
       
        //and then something for the point awarding.... you know.
        
        //WAIT: THE ENCOUNTER HAS AN UPDATE FUNCTION THAT CHECKS THINGS FOR BATTLE. I'll have it do the checking!
        //Strange that a canvas isn't the one doing it, but oh well.
    }

    public void StopEncounter()
    {
        //disable Battle Mode
        finishedFight = true;
    }

}