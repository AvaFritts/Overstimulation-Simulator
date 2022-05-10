// Creator: Ava Fritts
//Date Created: May 6th 2022

// Last edited: May 6th 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overstimulation : MonoBehaviour
{
    //VARIABLES

    GameManager GM;

    public Slider stimulationGauge;
    public Button smallButton;
    public Button mediumButton;
    public Button largeButton;
    public Button meltdownButton;

    [Header("Set in Inspector: Button activation values")]
    public float activateSmall;
    public float activateMedium;
    public float activateLarge;

    public float overStimMult = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stimulationGauge.value += overStimMult * Time.deltaTime;
        if (stimulationGauge.value.Equals(stimulationGauge.maxValue))
        {
            smallButton.interactable = false;
            mediumButton.interactable = false;
            largeButton.interactable = false;

            meltdownButton.interactable = true;
        }
    }

    public void CalmingDown(float delay)
    {
            stimulationGauge.value -= delay;
           /* if (overStimMult < 0) //if the value isn't going up
            {
                //Now, this value WILL need to be changed. But it is just a concept.
                Invoke("CalmingDown(float)", delay);
            } */
        }

        public void WindingUp()
        {
            if(stimulationGauge.value >= activateLarge)
            {
                smallButton.interactable = true;
                mediumButton.interactable = true;
            largeButton.interactable = true;
        }
        else if (stimulationGauge.value >= activateMedium)
        {
            smallButton.interactable = true;
            mediumButton.interactable = true;
            largeButton.interactable = false;
        }
        else if (stimulationGauge.value >= activateSmall)
        {
            smallButton.interactable = true;
            mediumButton.interactable = false;
            largeButton.interactable = false;
        }
        else
        {
            smallButton.interactable = false;
            mediumButton.interactable = false;
            largeButton.interactable = false;
        }
    }
    public void MeltingDown()
    {
        //play animation and sound.

        //go to game over screen.
        Debug.Log("Game Over");
        Invoke("GameOverCall", 5f);
    }

    void GameOverCall()
    {
        GameManager.GM.GameOver();
    }
}
