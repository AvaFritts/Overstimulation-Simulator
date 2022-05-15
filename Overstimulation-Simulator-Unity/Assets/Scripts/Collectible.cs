// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: May 12th 2022
// Description: The base script for the settings.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    private bool isPaused = true; //if the player isn't in range, it "pauses" the effects

    public int taskNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (Input.GetButtonDown("Fire1")) //if the player interacts with it.
            {
                Debug.Log("Recieved Piece for task");
                //update task

                this.gameObject.SetActive(false);
            }
        }
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
