// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: May 11th 2022
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

    public Text DifficultyButton; //the text for the button.
    // Start is called before the first frame update
     void Awake()
    {
        difficultyMode = GameManager.GM.difficulty; //gets the difficulty from the GM
        LoadDifficulty(); //Loads the current Difficulty.
    }

    // Changes how stimulated the player is at the start of the game
    public void ChangeDifficulty()
    {
        difficultyMode+= 5; //adds five to the difficulty

        LoadDifficulty();
        //on each case, change the difficulty 
    }

    void LoadDifficulty()
    {
        switch (difficultyMode)
        {
            case 0:
                DifficultyButton.text = "Easy";
                DifficultyButton.color = Color.green;
                break;
            case 5:
                DifficultyButton.text = "Medium";
                DifficultyButton.color = Color.yellow;
                break;
            case 10:
                DifficultyButton.text = "Hard";
                DifficultyButton.color = Color.red;
                break;
            default:
                difficultyMode = 0;
                DifficultyButton.text = "Easy";
                DifficultyButton.color = Color.green;
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
