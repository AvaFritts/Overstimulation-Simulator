// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: July 21st 2025
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

    public GameManager GameManager
    {
        get => default;
        set
        {
        }
    }

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        destinationPosition = new Vector3(3f, -1.33f, 0f); //sprite is placed at Home
        UpdateLevel(-1);
    }

    void Update()
    {
        if (!movingCar.transform.position.Equals(destinationPosition)) //if a new position is selected
        {
            Vector3 newCarPosition = Vector3.MoveTowards(movingCar.transform.position, destinationPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destinationPosition) > 5) //if you are in a certain distance
            {
                if (movingCar.transform.position.x - destinationPosition.x < 0)
                {
                    movingCar.transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    movingCar.transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
            

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
                destinationPosition = new Vector3(3f, -1.33f, 0f);
                break;
            case 0: //Tutorial
                levelDescription.text = "Ah. Home sweet home: A great place to relax without the gaze of total strangers.";
                destinationPosition = new Vector3(3f, -1.33f, 0f);
                break;
            case 1: //The Store
                levelDescription.text = "Every human needs to get groceries somehow. Sadly, your time is now.";
                destinationPosition = new Vector3(-2.37f, 3.59f, 0f);
                break;
            case 3: //The Office
                levelDescription.text = "Humans have jobs. Hope you survive yours.";
                destinationPosition = new Vector3(6.25f, 2.15f, 0f);
                break;
            case 2: //The Party
                levelDescription.text = "<i>It seems certain assets are placeholders...</i> the party must be getting set up.";
                //levelDescription.text = "Humans go to parties to be seen as 'social'."; //the official text.
                destinationPosition = new Vector3(-2.5f, -1f, 0f);
                break;
            case 4: //Endless.
                levelDescription.text = "You found the park, huh? Seems it's under construction.";
                //levelDescription.text = "You always loved theme parks. Felt like you could spend forever there..."; //the official text.
                destinationPosition = new Vector3(7.75f, -3.09f, 0f);
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
