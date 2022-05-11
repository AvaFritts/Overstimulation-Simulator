// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: May 10th 2022
// Description: The UI manager for the Level Select.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Select_Manager : MonoBehaviour
{
    GameManager GM; //reference to the game manager.

    public Text levelDescription;

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        UpdateLevel(0);
        //put sprite at home
    }

    //Picks the Level 
    public void UpdateLevel(int level)
    {
        GameManager.GM.gameLevelsCount = level; //sets which level the player will go to once you hit Play

        UpdateLevelDescription(level);
    }

    //updates the level description text
    private void UpdateLevelDescription(int level)
    {
        switch (level)
        {
            case 0:
                levelDescription.text = "Every human needs to get groceries somehow. Sadly, your time is now.";
                break;
            case 1:
                levelDescription.text = "Humans have jobs. Hope you survive yours.";
                break;
            case 2:
                levelDescription.text = "Humans go to parties to be seen as 'social'.";
                break;
            default:
                levelDescription.text = "An Error has occured. Please try again.";
                break;
        }
    }

    //activate once the player hits the play button
    public void HitPlay()
    {
        GameManager.GM.PlayGame();
    }

    public void GoBack()
    {
        GameManager.GM.ExitGame();
    }

}
