// Creator: Ava Fritts
// Date Created: May 10th 2022

// Last edited: Feb 17th 2023
// Description: The script for the starting scene.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starting_Canvas : MonoBehaviour
{
    // Start is called before the first frame update

    GameManager GM;

    public Text titleText;

    void Start()
    {
        titleText.text = GameManager.GM.gameTitle;
    }

    public void LoadSettings()
    {
        GameManager.GM.SettingsScene();

    }

    public void QuittingGame()
    {
        GameManager.GM.AbortGame();
    }

    public void StartingGame()
    {
        GameManager.GM.StartGame();
    }
}
