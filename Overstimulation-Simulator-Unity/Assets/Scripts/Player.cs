// Creator: Ava Fritts
//Date Created: May 6th 2022

// Last edited: Oct 4th 2024
//Description: The Player script.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("This value is the player's max speed. Their actual speed will vary.")]
    public int playerSpeed = 2;

    private int currentSpeed;
    //This is to prevent the "panic" animation from happening 100 times.
    private bool alreadyDead = false;

    private Animator stateControll;
    private SpriteRenderer playerSprites;
    public GameObject overstimManagerObj;
    public Overstimulation dialManager;
    Vector3 pos;

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
        currentSpeed = playerSpeed;
        pos = transform.position;
    }

    public void PauseDial()
    {
        dialManager.gaguePaused = true;
        currentSpeed = 0;
    }

    public void ResumeDial()
    {
        dialManager.gaguePaused = false;
        currentSpeed = playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        //I don't want anyone walking during a fight.
        if (GameManager.GM.gameState != GameManager.gameStates.Battle)
        {

            if (GameManager.GM.gameState == GameManager.gameStates.Death && alreadyDead == false)
            {
                currentSpeed = 0;
                stateControll.SetBool("Dead", true);
                stateControll.SetBool("stressed", false);
                stateControll.SetTrigger("Panic");
                alreadyDead = true;
            }

            //get the position
            pos.x = this.transform.position.x;
            //if not in Meltdown, let them move
            pos.x += Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
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
