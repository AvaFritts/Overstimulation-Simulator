// Creator: Ava Fritts
//Date Created: July 27th 2025

// Last edited: July 27th 2025
//Description: The script for items in a level that can help destimulate a player.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestimulationProp : MonoBehaviour
{
    //VARIABLES
    [SerializeField] //Access to private variables in editor
    private Overstimulation overStimManager;

    [Space(10)]

    [Tooltip("The Player's animator.")]
    public Animator pStateControll;
    private SpriteRenderer activeNotice;
    [Tooltip("The animation associated with this item.")]
    public string pTrigger;

    [Space(10)]

    [Tooltip("The sprite for when you can activate the item.")]
    public Sprite readyImage;
    [Tooltip("The sprite for when the player cannot activate.")]
    public Sprite cooldownImage;
    [Tooltip("The sprite for when the player can activate but are not in range.")]
    public Sprite inactiveImage;

    [Space(10)]
    //Timer values
    [SerializeField] //Access to private variables in editor
    private float timerDuration;
    [SerializeField]
    private float timer;
    [Tooltip("This number MUST be a positive value, as it will be subtracted from the rest.")]
    public float overStimMult = 0;

    [Header("Change Dynamically")]
    [SerializeField]
    private bool refillPaused; //Should be allowed to recharge even if out of range.
    private bool paused;
    // Start is called before the first frame update

    private void Awake()
    {
        activeNotice = this.GetComponent<SpriteRenderer>();
        activeNotice.sprite = inactiveImage;
        refillPaused = true;
        paused = true; //Otherwise, the player can press space and recharge from other rooms.
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GM.gameState == GameManager.gameStates.Playing)
        {
            if (!refillPaused) //Do I need to keep this? //Maybe, as the destim buttons use it last I remember
            {
                Recharge();
            }
            else
            {
                if (Input.GetButtonDown("Jump") && !paused) //if the player interacts with it.
                {
                    CalmingDown(pTrigger);
                    activeNotice.sprite = cooldownImage;
                }
            }

        } //end "If not Paused"
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            paused = false;
            //overstimGO.overStimMult += multModifier; //add the multiplier to the modifier
            if (refillPaused) //Do I need to keep this? //Maybe, as the destim buttons use it last I remember
            {
                activeNotice.sprite = readyImage;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            paused = true;
            if (refillPaused) //Do I need to keep this? //Maybe, as the destim buttons use it last I remember
            {
                activeNotice.sprite = inactiveImage;
            }
        }
    }

    public void Recharge()
    {
        timer = Mathf.Clamp(timer + timerDuration * Time.deltaTime, 0, 1);

        if (timer >= 1)
        {
            refillPaused = true;
            timer = 0;
            activeNotice.sprite = readyImage;
        }
    }


    public void CalmingDown(string triggerName)
    {
        pStateControll.SetTrigger(triggerName);
        overStimManager.stimulationGauge.value -= overStimMult;
        refillPaused = false;
    }
}
