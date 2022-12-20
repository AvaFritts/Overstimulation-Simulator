// Creator: Ava Fritts
//Date Created: May 6th 2022

// Last edited: November 3rd 2022
//Description: The Player script.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public Sprite idleSprite;
    //public Sprite walkingSprite;
    public int playerSpeed = 2;
    //This is to prevent the "panic" animation from happening 100 times.
    private bool alreadyDead = false;

    private Animator stateControll;
    private SpriteRenderer playerSprites;
    public GameObject overstimManagerObj;
    public Overstimulation dialManager;

    GameManager GM;
    private void Awake()
    {
        dialManager = overstimManagerObj.GetComponent<Overstimulation>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerSprites = this.GetComponent<SpriteRenderer>();
        stateControll = this.GetComponent<Animator>();

    }

    public void PauseDial()
    {
        dialManager.paused = true;
    }

    public void ResumeDial()
    {
        dialManager.paused = false;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        //I don't want anyone walking during a fight.
        if (GameManager.GM.gameState != GameManager.gameStates.Battle)
        {

            if (GameManager.GM.gameState == GameManager.gameStates.Death && alreadyDead == false)
            {
                playerSpeed = 0;
                stateControll.SetBool("Dead", true);
                stateControll.SetBool("stressed", false);
                stateControll.SetTrigger("Panic");
                alreadyDead = true;
            }

            //get the position
            pos.x = this.transform.position.x;
            //if not in Meltdown, let them move
            pos.x += Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
            //if(walking) in opposite direction, flip the sprite
            if (Input.GetAxis("Horizontal") < 0) //walking Left
            {
                //playerSprites.sprite = walkingSprite;
                stateControll.SetBool("isWalking", true);
                playerSprites.flipX = true;
            }
            else if (Input.GetAxis("Horizontal") > 0) //walking Right
            {
                //playerSprites.sprite = walkingSprite;
                stateControll.SetBool("isWalking", true);
                playerSprites.flipX = false;
            }
            else //standing Still
            {
                //playerSprites.sprite = idleSprite;
                stateControll.SetBool("isWalking", false);
            }

            //set the position
            this.transform.position = pos;
        }
       
    }
    public void GameOverCall()
    {
        GameManager.GM.GameOver();
    }
}
