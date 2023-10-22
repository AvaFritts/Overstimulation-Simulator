// Creator: Ava Fritts
//Date Created: May 8th 2023

// Last edited: May 8th 2023
//Description: The Template for Conversation Tasks.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "Asset/Encounter")]
public class Convo_Task_Obj : ScriptableObject
{
    public string encounterText;
    public Sprite encounterSprite;

    //After pressing a button
    public string[] responses;
    public int[] punishment;

    //Idea: put the nextQuestion method in the boss object. Idk if the boss will use this object.
}
