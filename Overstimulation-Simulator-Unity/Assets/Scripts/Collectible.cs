// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: May 20th 2022
// Description: The base script for the settings.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    private bool isPaused = true; //if the player isn't in range, it "pauses" the effects

    GameManager GM;

    public Task associatedTask;
   
    // Update is called once per frame
    void Update()
    {
        if (!isPaused && (GameManager.GM.gameState == GameManager.gameStates.Playing || GameManager.GM.isTesting))
        {
            if (Input.GetButtonDown("Fire1")) //if the player interacts with it.
            {
                Debug.Log("Recieved Piece for task");
                //update task

                associatedTask.UpdateTask(); //update the task value

                this.gameObject.SetActive(false);
            }
        }
    }

    //For endless mode.
    private void OnDisable()
    {
        isPaused = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            isPaused = false;
        }
    }//end OnTriggerEnter

    private void OnTriggerExit(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            isPaused = true;
        }
    } //end OnTriggerExit
}
