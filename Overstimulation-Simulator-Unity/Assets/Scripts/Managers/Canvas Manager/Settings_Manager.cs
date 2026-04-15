// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: March 25th 2026
// Description: The UI manager for the settings.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings_Manager : MonoBehaviour
{

    GameManager GM; //reference to the game manager.

    //[Header ("Change Dynamically")]
    [Header("Visual Settings")]
    [SerializeField]
    [Tooltip("changes if the encounter background moves.")]
    private Toggle irlOverstimulation;
    [SerializeField]
    private Toggle fullScreenCheck;
    [SerializeField]
    private Text difficultyText; //the text for the button.
    [SerializeField]
    private Image difficultyButton;
    private float difficultyMode = 0; //is it easy, medium, or hard?
    //[Space(10)]

    [Header("All Pages")]
    [SerializeField]
    private Image backgroundPage;
    [SerializeField]
    private GameObject[] settingPages;

    //[Space(10)]
   //[Header("Audio Settings")]
    //private float audioValue;

    public GameManager GameManager
    {
        get => default;
        set
        {
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        difficultyMode = GameManager.GM.stimulationDifficulty; //gets the difficulty from the GM
        LoadDifficulty(); //Loads the current Difficulty. THE DIFFICULTY WILL BE THE PARTICLE COUNT. THIS NEEDS TO BE EDITED.
        StimulationLoader(); //loads the stimulation toggle
    }

    // Changes how many particles the player will see during gameplay.
    public void ChangeDifficulty()
    {
        difficultyMode+= 1; //adds 1 to the difficulty
        LoadDifficulty();
        //on each case, change the difficulty 
    }

    // Changes which setting is being accessed.
    public void ChangePage(int pageNum)
    {
        Color pageColor = new Color(1f, 1f, 1f, 1f);
        switch (pageNum)
        {
            case 0:
                Debug.Log("Visual page");
                pageColor = new Vector4(0.814581f, 0.7971698f, 1f, 1f);//purple.
                break;
            case 1:
                pageColor = new Vector4(1f, 0.9855981f, 0.7960784f, 1f);//yellow.
                break;
            case 2:
                pageColor = new Vector4(1f, 0.7960784f, 0.8203946f, 1f); //red.
                break;
            case 3:
                pageColor = new Vector4(0.7960784f, 1f, 0.9153805f, 1f); //green.
                break;
            default:
                backgroundPage.color = new Vector4(1f, 1f, 1f, 1f); //make the text easier to see.
                break;
        }
        //foreach game object in the page, if the order matches the variable, then set active. Else set false.
        int x = 0;
        foreach (GameObject textPage in settingPages){
            if (x == pageNum)
            {
                textPage.SetActive(true);
            }
            else
            {
                textPage.SetActive(false);
            }
            x += 1;
        }
        backgroundPage.color = pageColor;
    }

    public void StimulationChanger()
    {
        if (irlOverstimulation.isOn)
        {
            GameManager.GM.stilumationReducer = true;
        }
        else
        {
            GameManager.GM.stilumationReducer = false;
        }
    }

    public void StimulationLoader()
    {
        if (GameManager.GM.stilumationReducer)
        {
            irlOverstimulation.isOn = true;
        }
        else
        {
            irlOverstimulation.isOn = false;
        }
    }

    void LoadDifficulty() //Less particles makes it harder to play.
    {
        switch (difficultyMode)
        {
            case 0:
                difficultyText.text = "Many (Easy)";
                difficultyText.color = Color.black; //make the text easier to see.
                difficultyButton.color = Color.green;
                break;
            case 1:
                difficultyText.text = "Average (Default)";
                difficultyText.color = Color.black;
                difficultyButton.color = Color.yellow;
                break;
            case 2:
                difficultyText.text = "Few (Hard)";
                difficultyText.color = Color.white; //make the text easier to see.
                difficultyButton.color = Color.red;
                break;
            default:
                difficultyMode = 0;
                difficultyText.text = "Many (Easy)";
                difficultyText.color = Color.black; //make the text easier to see.
                difficultyButton.color = Color.green;
                break;
        }
        GameManager.GM.stimulationDifficulty = difficultyMode; //stores the difficulty in the GM
    }

    public void toggleScreen() //change between fullscreen and windowed.
    {
        if (fullScreenCheck.isOn)
        {
            Screen.SetResolution(1600, 900, FullScreenMode.FullScreenWindow);
            Debug.Log("Switched to Full Screen Mode");
        }
        else
        {
            Screen.SetResolution(1600, 900, FullScreenMode.Windowed);
            Debug.Log("Switched to Windowed Mode");
        }
        //If fullscreen, change to window. Else put it in fullscreen
    }

    public void ReturnToStart()
    {
        //go back to the start screen
        GameManager.GM.ExitGame(); //despite the name, the function just loads the start screen.
    }
}
