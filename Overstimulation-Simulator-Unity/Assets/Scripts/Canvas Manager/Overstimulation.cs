// Creator: Ava Fritts
//Date Created: May 6th 2022

// Last edited: July 18th 2022
//Description: The UI manager showing how close to a meltdown the player is.

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

    public GameObject[] stimulantSources;

    public float overStimMult = 0;
    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        stimulationGauge.value = GameManager.GM.difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused) 
        {
            overStimMult = StimulationUpdater();
            //overStimMult = Mathf.Clamp(StimulationUpdater(), 2f, 100f);
            stimulationGauge.value += overStimMult * Time.deltaTime;
            if (stimulationGauge.value.Equals(stimulationGauge.maxValue))
            {
                smallButton.interactable = false;
                mediumButton.interactable = false;
                largeButton.interactable = false;

                meltdownButton.interactable = true;
            }
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

    private float StimulationUpdater()
    {
        float baseCount = 2;
        foreach (GameObject stimulants in stimulantSources) 
        {
            StimulationSource modGO = stimulants.GetComponent<StimulationSource>();
            if (!modGO.paused) //if the item is active
            {
                baseCount += modGO.multModifier; //add the multiplier of the item to the count
            }
            
        }
        /*if (baseCount == 0) //if there are no stimulants in the area
        {
            return 2;
        }*/
        return baseCount; //it will always have a base number. Currently it is 2.
    }
}
