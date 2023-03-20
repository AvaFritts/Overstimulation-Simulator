// Creator: Ava Fritts
//Date Created: March 19th 2023

// Last edited: March 19th 2023
//Description: The Buttons for Conversation Tasks.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Emotion", menuName ="Asset/Emotion")]
public class ConversationButton : ScriptableObject
{
    public string NormalName;
    public string StressedName;
    public Sprite NormalFace;
    public Sprite StressedFace;
}
