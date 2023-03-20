// Creator: Ava Fritts
//Date Created: May 16th 2022

// Last edited: March 19th 2023
// Description: The script to manage any encounter in a given level.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter_Manager : MonoBehaviour
{
    //Variables//
    [Header("Set in Inspector")]
    public Text[] theFourButtons; //the four buttons used in the encounters
    public Image[] theFourFaces;
    public GameObject background;
    public float rotationSpeed;
    [Tooltip("The strings here should have the button labels for the 'good' responses")]
    public Sprite[] theCalmFaces;
    // public string[] theCalmChoices;
    [Tooltip("The strings here should have the button labels for the 'scary mode' responses")]
    public Sprite[] theStressedFaces;
    //public string[] theAnxiousChoices;

    [Space(5)]

    public Text battleQuestion;

    [Header("Set Dynamically")]
    public int numberOfQuestions = 1; //Bosses usually have multiple: everyone else has one.
    public float rotatZ;

    [Space(10)]

    public Encounter currentBattle;

    public int correctAnswer;
    public GameObject enemySprite;


    public void Update()
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
    public void StartEncounter(GameObject encounterBase)
    {
        //enable Battle Mode if it isn't in it already.
        if(GameManager.GM.gameState != GameManager.gameStates.Battle)
        {
            GameManager.GM.gameState = GameManager.gameStates.Battle;
            currentBattle = encounterBase.GetComponent<Encounter>();

            currentBattle.battleCamera.SetActive(true);

            if (currentBattle.scaryMode) //if scary mode is active
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
        battleQuestion.text = currentBattle.activeString;
        correctAnswer = currentBattle.activeAnswer;
        enemySprite.GetComponent<Image>().sprite = currentBattle.activeSprite;


        //disable any sound save for the music, if any. Probably through a boolean.

    }

    public void SubmittedAnswer (int choice)
    {
        if (choice == correctAnswer)
        {
            currentBattle.CorrectResponse();
        }
        else
        {
            currentBattle.IncorrectResponse();
        }
    }

    public void StopEncounter()
    {
        //disable Battle Mode
        GameManager.GM.gameState = GameManager.gameStates.Playing;

    }

}