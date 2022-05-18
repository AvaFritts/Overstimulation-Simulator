// Creator: Ava Fritts
//Date Created: May 16th 2022

// Last edited: May 17th 2022
// Description: The script to manage any encounter in a given level.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter_Manager : MonoBehaviour
{
    //Variables//
    [Header("Set Dynamically")]
    public int numberOfQuestions = 1; //Bosses usually have multiple: everyone else has one.

    public Encounter currentBattle;

    public Text battleQuestion;
    public int correctAnswer;
    public Sprite enemySprite;

    
    //for whenever an encounter is started
    public void StartEncounter(GameObject encounterBase)
    {
        //enable Battle Mode if it isn't in it already.
        if(GameManager.GM.gameState != GameManager.gameStates.Battle)
        {
            GameManager.GM.gameState = GameManager.gameStates.Battle;
            currentBattle = encounterBase.GetComponent<Encounter>();

            currentBattle.battleCamera.SetActive(true);
        }


        //put up the current question, set the correct answer, and change the sprite accordingly.
        battleQuestion.text = currentBattle.activeString;
        correctAnswer = currentBattle.activeAnswer;
        enemySprite = currentBattle.activeSprite;
        /*if (currentBattle.scaryMode)
        {
            //change the button names
        }*/


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