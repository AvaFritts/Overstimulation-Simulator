// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: Feb 16th 2023 //Technically May 24th 2026.
// Description: The UI manager for the Game Over canvas.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
   
public class Ending_Canvas : MonoBehaviour
{
    // Start is called before the first frame update

    GameManager GM;

    public Text endingText;
    public SpriteRenderer endingSprite;
    public Sprite happySprite;
    public Sprite sadSprite;
    public GameObject topButton;
    public GameObject midButton;
    public GameObject bottomButton;

    [HideInInspector] public bool wonGame;

    void Start()
    {
        //endingText.text = GameManager.GM.endMsg;
    }

    private void Awake()
    {
        //GameManager.GM.GameOverEvent.AddListener(SetScene);
       wonGame = GameManager.GM.isHappy;
        SetScene(wonGame);
    }

    public void SetScene (bool goodEnd)
    {
        //Debug.Log("Found Event");
        endingText.text = GameManager.GM.endMsg;
        if (goodEnd == true)
        {
            endingText.rectTransform.localPosition = new Vector3(51.58f, 51.37f, 0); //Originally 39.3, 60.2, 0
            endingText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 407.5f);
            endingText.rectTransform.rotation = Quaternion.Euler(0, 0, -17.84f);
            endingText.fontSize = 75;

            endingSprite.sprite = happySprite;

            topButton.transform.localPosition = new Vector3(19.1f, 47.5f, 0); ////Originally 0, 45.55ff, 0
            topButton.transform.rotation = Quaternion.Euler(0, 0, -21.7f);

            midButton.transform.localPosition = new Vector3(-71.7f, -96f, 0); //Originally -99.5f, -104.3f, 0
            midButton.transform.rotation = Quaternion.Euler(0, 0, -23.58f);

            bottomButton.transform.localPosition = new Vector3(-150.4f, -226.23f, 0); //Originally -174.6f, -243.7f, 0
            bottomButton.transform.rotation = Quaternion.Euler(0, 0, -25.4f);
        }
        else
        {
            endingSprite.sprite = sadSprite;

            midButton.transform.localPosition = new Vector3(-553.5f, -4.31f, 0);
            topButton.transform.localPosition = new Vector3(553.5f, -4.31f, 0);
            bottomButton.transform.localPosition = new Vector3(553.5f, -397.95f, 0);

        }
    }

    public void RetryGame()
    {
        GameManager.GM.PlayGame();
    }

    public void ExitGame()
    {
        GameManager.GM.ExitGame();
    }

    public void StartingGame()
    {
        GameManager.GM.StartGame(); //go to level select
    }
}