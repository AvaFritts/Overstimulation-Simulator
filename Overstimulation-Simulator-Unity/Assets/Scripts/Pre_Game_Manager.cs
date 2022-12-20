// Creator: Ava Fritts
// Date Created: Oct. 22nd 2022

// Last edited: Oct. 22nd 2022
// Description: Manages the "Scenes" in the main menu.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pre_Game_Manager : MonoBehaviour
{
    GameManager GM;
    public GameObject[] scenes;
    [SerializeField] public int sceneToLoad;

    /*// Start is called before the first frame update
    void Start()
    {
        sceneToLoad = GameManager.GM.mainMenuState;
        for (int i = 0; i < scenes.Length; i++)
        {
            if (i == sceneToLoad)
            {
                scenes[i].SetActive(true);
            }
            else
            {
                scenes[i].SetActive(false);
            }
        }
    }

    void ChangeScene()
    {
        sceneToLoad = GM.mainMenuState;
        for (int i = 0; i < scenes.Length; i++)
        {
            if (i == sceneToLoad)
            {
                scenes[i].SetActive(true);
            }
            else
            {
                scenes[i].SetActive(false);
            }
        }
    }*/


}

