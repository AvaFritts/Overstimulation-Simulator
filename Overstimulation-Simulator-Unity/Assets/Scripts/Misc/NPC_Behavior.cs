// Creator: Ava Fritts
//Date Created: May 16th 2022

// Last edited: July 17th, 2025 //November 3rd 2022
// Description: The base script for all the NPCs in a given level.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Behavior : MonoBehaviour
{
   
    public bool isMoving;
    public GameObject playerOBJ;
    public float spriteThreshold;

    [Header("NORMAL SPRITES")]

    public Sprite backSprite;
    //public Sprite neutralAngled;

    [Space(10)]

    public Sprite halfAngled;

    [Space(10)]

    public Sprite frontSprite;
    public Sprite meanAngled;


    GameManager GM;
    private SpriteRenderer npcSprite;
    [Tooltip("To make the script work, the entire scene must be ABOVE X = 0.")]
    private float playerXDistance;


    // Start is called before the first frame update
    void Start()
    {
        npcSprite = this.GetComponent<SpriteRenderer>();
        npcSprite.sprite = backSprite;
    }

    //I put so much calculation into them looking at her, but I think most of that isn't being used right now.
    //So I reworked it to only make the calculation during a game-over.
     private void FixedUpdate()
     {
        if (GameManager.GM.gameState == GameManager.gameStates.Death)
        {
            playerXDistance = this.transform.position.x - playerOBJ.transform.position.x;
            if(playerXDistance <= 0) //if negative
            {
                npcSprite.flipX = true;
            //} 
            //else
            //{
                playerXDistance *= -1; //turn positive for the calculations.
            }

            if (playerXDistance < spriteThreshold) //&& (playerXDistance > -spriteThreshold/2))
            {
                //if (GameManager.GM.gameState == GameManager.gameStates.Death)
                npcSprite.sprite = frontSprite;
                /*else npcSprite.sprite = backSprite;*/
            }
            else if (playerXDistance < spriteThreshold * 2)
            {
                //npcSprite.flipX = false;
                npcSprite.sprite = halfAngled;
            }
            else
            {
                //npcSprite.flipX = true;
                npcSprite.sprite = meanAngled;
            }
        }
     }//end Update

}
