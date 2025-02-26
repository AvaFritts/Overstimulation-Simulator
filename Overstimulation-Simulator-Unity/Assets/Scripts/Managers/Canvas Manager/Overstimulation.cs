// Creator: Ava Fritts
//Date Created: May 6th 2022

// Last edited: November 28th, 2023
//Description: The UI manager showing how close to a meltdown the player is.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overstimulation : MonoBehaviour
{
    //VARIABLES

    GameManager GM;
    AudioManager AM;

    public Animator stateControll;

    public Slider stimulationGauge;
    public Button smallButton;
    public Button mediumButton;
    public Button largeButton;
    public Button meltdownButton;

    [Header("Set in Inspector: Button activation values")]
    public float activateSmall;
    public float activateMedium;
    public float activateLarge;

    [Space(10)]
    //Timer values
    public float smallTimerDuration;
    public float mediumTimerDuration;
    public float largeTimerDuration;

    public Image smallImage;
    public Image mediumImage;
    public Image largeImage;
    //Timer values start at set duration and count down to 0. Clamped to 0
    //private float smallTimer;
    //private float mediumTimer;
    //private float largeTimer;

    public GameObject[] stimulantSources;

    public float overStimMult = 0;

    [Header("Change Dynamically")]
    public float currentStimMult;
    public bool buttonPaused;
    public bool gaguePaused;
   
    // Update is called once per frame
    void Update()
    {
        if (GameManager.GM.gameState != GameManager.gameStates.Death) 
        {
            if (!gaguePaused)
            {
                currentStimMult = StimulationUpdater();
                //overStimMult = Mathf.Clamp(StimulationUpdater(), 2f, 100f);
                stimulationGauge.value += currentStimMult * Time.deltaTime;
                if (stimulationGauge.value.Equals(stimulationGauge.maxValue) && GameManager.GM.gameState != GameManager.gameStates.Death)
                {
                    smallButton.interactable = false;
                    mediumButton.interactable = false;
                    largeButton.interactable = false;

                    meltdownButton.interactable = true;
                    GameManager.GM.gameState = GameManager.gameStates.Death;
                    AudioManager.AM.DeathSong();
                }
            }

            //Timer logic
            ButtonCooldown(smallButton, smallImage, smallTimerDuration);
            ButtonCooldown(mediumButton, mediumImage, mediumTimerDuration);
            ButtonCooldown(largeButton, largeImage, largeTimerDuration);
        } //end "If not Paused"
    }

    /*public void PauseGame()
    {
        gaguePaused = true;
        buttonPaused = true;
        StimulationVolumeCtrl();
    }

    private void ResumeGame()
    {
        gaguePaused = false;
        buttonPaused = false;
    }*/

    public void ButtonAnimation(string triggerName)
    {
        stateControll.SetTrigger(triggerName);
    }

    //I need two or three functions. One to pause the game, and another to run the animation. 
    // The last thing needed is a way to reward the player
    public void CalmingDown(float reward)
    {    
        stimulationGauge.value -= reward;
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
            //smallButton.interactable = true; 
            //mediumButton.interactable = true;  
            //largeButton.interactable = true;
            stateControll.SetBool("stressed", true);

        }
        else
        {
            //smallButton.interactable = false;
            //mediumButton.interactable = false;
            //largeButton.interactable = false;
            stateControll.SetBool("stressed", false);
        }
    }
    public void MeltingDown()
    {
        //play animation and sound.
        stateControll.SetBool("Meltdown", true);
        //go to game over screen.
        Debug.Log("Game Over");
        //Invoke("GameOverCall", 7f); //Player Takes care of this.
    }

   /*public void GameOverCall()
    {
        GameManager.GM.GameOver();
    }*/

    private void StimulationVolumeCtrl()
    {
        foreach (GameObject stimulants in stimulantSources)
        {
            StimulationSource modGO = stimulants.GetComponent<StimulationSource>();
            if(modGO.badAudio.mute == true)
            {
                modGO.badAudio.mute = false;
            }
            else
            {
                modGO.badAudio.mute = true;
            }
        }   
    }

    private float StimulationUpdater()
    {
        float baseCount = overStimMult;
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

    private void ButtonCooldown (Button stimButton, Image stimImage, float stimTimerDuration)
    {
        if (stimButton.interactable == false)
        {
            stimImage.fillAmount = Mathf.Clamp(stimImage.fillAmount + stimTimerDuration * Time.deltaTime, 0, 1);
            if (stimImage.fillAmount >= 1)
            {
                stimButton.interactable = true;
                stimImage.fillAmount = 0;
            }
        }
    } //end ButtonCooldown

}
