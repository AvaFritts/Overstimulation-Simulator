// Creator: Ava Fritts
//Date Created: May 8th 2023

// Last edited: July 19th 2025 //October 22nd 2023
//Description: The Template for Conversation Tasks.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "Asset/Encounter")]
public class Convo_Task_Obj : ScriptableObject
{
    //MAKE SINGLE LINES, NOT ARRAYS
    public string encounterText;
    public Sprite encounterSprite;
    public Convo_Task_Obj nextQuestion;

    [Space(15)]

    //After pressing a button
    public string[] responses;
    public int[] punishment;

    [Tooltip("Check this if the encounter is used when Emma is Overstimulated")]
    public bool scaryMode;
    public bool isABoss; //I have a thing written down where they "get" the next one and return if it != null.

    //Idea: put the nextQuestion method in the boss object. Idk if the boss will use this object.
}
