// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: June 2nd 2022
// Description: The UI manager for the settings.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings_Manager : MonoBehaviour
{

    GameManager GM; //reference to the game manager.

    [Header ("Change Dynamically")]
    public float difficultyMode = 0; //is it easy, medium, or hard?
    public Toggle irlOverstimulation;

    public Text difficultyText; //the text for the button.
    public Image difficultyButton;
    // Start is called before the first frame update
     void Awake()
    {
        difficultyMode = GameManager.GM.difficulty; //gets the difficulty from the GM
        LoadDifficulty(); //Loads the current Difficulty.
        StimulationLoader(); //loads the stimulation toggle
    }

    // Changes how stimulated the player is at the start of the game
    public void ChangeDifficulty()
    {
        difficultyMode+= 5; //adds five to the difficulty

        LoadDifficulty();
        //on each case, change the difficulty 
    }

    public void StimulationChanger()
    {
        if (irlOverstimulation.isOn)
        {
            GameManager.GM.stilumationReducer = true;
        }
        else
        {
            GameManager.GM.stilumationReducer = false;
        }
    }

    public void StimulationLoader()
    {
        if (GameManager.GM.stilumationReducer)
        {
            irlOverstimulation.isOn = true;
        }
        else
        {
            irlOverstimulation.isOn = false;
        }
    }

    void LoadDifficulty()
    {
        switch (difficultyMode)
        {
            case 0:
                difficultyText.text = "Easy";
                difficultyText.color = Color.black; //make the text easier to see.
                difficultyButton.color = Color.green;
                break;
            case 5:
                difficultyText.text = "Medium";
                difficultyText.color = Color.black;
                difficultyButton.color = Color.yellow;
                break;
            case 10:
                difficultyText.text = "Hard";
                difficultyText.color = Color.white; //make the text easier to see.
                difficultyButton.color = Color.red;
                break;
            default:
                difficultyMode = 0;
                difficultyText.text = "Easy";
                difficultyText.color = Color.black; //make the text easier to see.
                difficultyButton.color = Color.green;
                break;
        }
        GameManager.GM.difficulty = difficultyMode; //stores the difficulty in the GM
    }

    public void ReturnToStart()
    {
        //go back to the start screen
        GameManager.GM.ExitGame(); //despite the name, the function just loads the start screen.
    }
}
