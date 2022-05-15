// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: May 11th 2022
// Description: The UI manager for the Level Select.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Select_Manager : MonoBehaviour
{
    
    //VARIABLES
    GameManager GM; //reference to the game manager.

    public Text levelDescription;

    public GameObject movingCar;

    public Vector3 destinationPosition;

    public int speed = 20; // Number of frames to completely interpolate between the 2 positions

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        destinationPosition = new Vector3(3.3f, 0.1f, 0f); //sprite is placed at Home
        UpdateLevel(-1);
    }

    void Update()
    {
        if (!movingCar.transform.position.Equals(destinationPosition)) //if a new position is selected
        {
            Vector3 newCarPosition = Vector3.MoveTowards(movingCar.transform.position, destinationPosition, speed * Time.deltaTime);
            
            movingCar.transform.position = newCarPosition;
           
        }

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
            case -1:
                levelDescription.text = "Click a location to select it.";
                destinationPosition = new Vector3(3.3f, 0.1f, 0f);
                break;
            case 0: //The Store
                levelDescription.text = "Every human needs to get groceries somehow. Sadly, your time is now.";
                destinationPosition = new Vector3(-3.7f, 4.8f, 0f);
                break;
            case 1: //The Office
                levelDescription.text = "Humans have jobs. Hope you survive yours.";
                destinationPosition = new Vector3(3.28f, 3.26f, 0f);
                break;
            case 2: //The Party
                levelDescription.text = "Humans go to parties to be seen as 'social'.";
                destinationPosition = new Vector3(-2.86f, 0.6f, 0f);
                break;
            default:
                levelDescription.text = "An Error has occured. Please try again.";
                destinationPosition = new Vector3(0f, 0f, 0f);
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
