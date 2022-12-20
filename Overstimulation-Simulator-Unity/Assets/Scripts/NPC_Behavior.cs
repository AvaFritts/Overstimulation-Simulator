// Creator: Ava Fritts
//Date Created: May 16th 2022

// Last edited: November 3rd 2022
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
    public Sprite neutralAngled;

    [Space(10)]

    public Sprite susAngled;

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
    }

    //The 
     private void FixedUpdate()
     {
         playerXDistance = this.transform.position.x - playerOBJ.transform.position.x;


         if((playerXDistance < spriteThreshold) && (playerXDistance > -spriteThreshold)) 
         {
            npcSprite.flipX = false;
            if (GameManager.GM.gameState == GameManager.gameStates.Death)
            {
                npcSprite.sprite = frontSprite;
            }
            else
            {
                npcSprite.sprite = backSprite;
            }
         } else if (playerXDistance < -spriteThreshold)
        {
            npcSprite.flipX = true;
            if (GameManager.GM.gameState == GameManager.gameStates.Death)
            {
                npcSprite.sprite = meanAngled;
            }
            else
            {
                npcSprite.sprite = neutralAngled;
            }
        }
        else
        {
            npcSprite.flipX = false;
            if (GameManager.GM.gameState == GameManager.gameStates.Death)
            {
                npcSprite.sprite = meanAngled;
            }
            else
            {
                npcSprite.sprite = neutralAngled;
            }
           
        }
     }//end Update

}
