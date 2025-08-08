// Creator: Ava Fritts
//Date Created: July 4th 2025

// Last edited: July 5th 2025
//Description: The script used to move the tutorial along.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    [Header("Tutorial: Set in Inspector")]
    public GameObject nextObject;
    //private GameObject itself;
    [Tooltip("0 = standard, 1 = if object disappears once clicked, 2 = multiple objects.")]
    public int nextObjectCheck;
    [Header("If 2")]
    public GameObject otherObject;
    [Header("Set for UI purposes")]
    public GameObject tutPanel;
    public Vector3 tutTransform;
    public Text tutText;
    public string nextText;
    [Header("Set Dynamically")]
    private bool isPaused = true;

    //public TutorialManager findMain;

    private void OnEnable()
    {
        tutText.text = nextText;
        RectTransform tutPanelTransform = tutPanel.GetComponent<RectTransform>();
        tutPanelTransform.localPosition = new Vector3 (tutTransform.x, tutTransform.y);
    }

    //I hate this. But when I do "On collider Stay" the arrow's trigger doesn't work correctly.this
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isPaused) //if the player interacts with it.
        {
            if (nextObjectCheck > -1)
            {
                //nextObject.GetComponent<TutorialTrigger>().enabled = true;
                nextObject.SetActive(true);
                if (nextObjectCheck == 2)
                {
                    otherObject.SetActive(true);
                }
            }
            this.GetComponent<TutorialTrigger>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            isPaused = false;
        }
    }//end OnTriggerEnter
    private void OnTriggerExit(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            isPaused = true;
        }
    } //end OnTriggerExit

    private void OnDisable()
    {
        isPaused = false;
        if (nextObjectCheck == 1)
        {
            nextObject.SetActive(true);
            this.GetComponent<TutorialTrigger>().enabled = false;
        }
    }
}
