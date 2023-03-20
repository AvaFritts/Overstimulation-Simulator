// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: Feb 16th 2023
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
            endingText.rectTransform.localPosition = new Vector3(39.3f, 60.62f, 0);
            endingText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 407.5f);
            endingText.rectTransform.rotation = Quaternion.Euler(0, 0, -17.84f);
            endingText.fontSize = 75;

            endingSprite.sprite = happySprite;

            topButton.transform.localPosition = new Vector3(0, 45.55f, 0);
            topButton.transform.rotation = Quaternion.Euler(0, 0, -21.7f);

            midButton.transform.localPosition = new Vector3(-99.5f, -104.3f, 0);
            midButton.transform.rotation = Quaternion.Euler(0, 0, -23.58f);

            bottomButton.transform.localPosition = new Vector3(-174.6f, -243.7f, 0);
            bottomButton.transform.rotation = Quaternion.Euler(0, 0, -12.5f);
        }
        else
        {
            endingSprite.sprite = sadSprite;

            topButton.transform.localPosition = new Vector3(-553.5f, -4.31f, 0);
            midButton.transform.localPosition = new Vector3(553.5f, -4.31f, 0);
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