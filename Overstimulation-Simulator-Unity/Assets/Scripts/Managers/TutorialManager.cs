// Creator: Ava Fritts
//Date Created: July 4th 2025

// Last edited: July 5th 2025
//Description: The script used to move the tutorial canvas along.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject overstimulationChecker;
    private Overstimulation meterValue;
    //public GameObject tutorialText;
    public GameObject tutorialPanel;
    public GameObject mainPick;
    public GameObject[] arrows;
    
    void Awake()
    {
        //if players have played the tutorial before, close out.
        meterValue = overstimulationChecker.GetComponent<Overstimulation>();
        //meterValue.PauseGame(); //This breaks the Tutorial.
        overstimulationChecker.SetActive(false);
        Time.timeScale = 0;
    }

   void PauseMeter()
    {

    }

    void ResumeMeter()
    {

    }

    public void StartGame() //Used if Player says "No."
    {
        ArrowSet();
        overstimulationChecker.SetActive(true);
        meterValue.ResumeGame(); //not needed, but kept all the same.
        Time.timeScale = 1;
        mainPick.SetActive(false);
    }

    public void StartTutorial() //If the player says "Yes" on the tutorial button.
    {
        Time.timeScale = 1;
        //ArrowSet();
        //tutorialText.SetActive(true);
        tutorialPanel.SetActive(true);
        mainPick.SetActive(false);
    }

    private void ArrowSet()
    {
        foreach (GameObject arrow in arrows)
        {
            arrow.GetComponent<TutorialTrigger>().enabled = false;
            arrow.SetActive(true);
        }
    }

}
