// Creator: Ava Fritts
//Date Created: May 13th 2022

// Last edited: July 3rd 2025
// Description: The base script for the arrows.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    //VARIABLES 
    private bool isPaused = true; //if the player isn't in range, it "pauses" the effects
    GameManager GM;

    [Header("Movement Vectors: Set in Inspector")]
    public Vector3 positionOffset;
    public Vector3 cameraPositionOffset;

    public GameObject playerGO;
    private Animator stateControll;

    private void Awake()
    {
        stateControll = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && (GameManager.GM.gameState == GameManager.gameStates.Playing || GameManager.GM.isTesting))
        {
            if (Input.GetButtonDown("Jump")) //if the player interacts with it.
            {
                Debug.Log("Moving up");
                //update task

                playerGO.transform.position += positionOffset;
                Camera.main.transform.position += cameraPositionOffset; //Moves the camera.
            }
        }
    }

    /*private void OnTriggerStay(Collider other)
    {

    }*/

    private void OnTriggerEnter(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            isPaused = false;
            stateControll.SetBool("InRange", true);
        }
    }//end OnTriggerEnter

    private void OnTriggerExit(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            isPaused = true;
            stateControll.SetBool("InRange", false);
        }
    } //end OnTriggerExit
}
