// Creator: Ava Fritts
//Date Created: May 15th 2022

// Last edited: May 16th 2022
// Description: The script for the individual tasks in a given level.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
   
    //Variables//

    [Header("Set In Inspector")]
    public Task_Manager toDoList; //the reference to the To-Do List
    public string taskDescription;
    public int numItems;

    [Tooltip("Check this box if the task involves an NPC encounter or is a Boss Task.")]
    public bool isTypeHuman;

    [Space(10)]

    public GameObject taskParent;
    public GameObject[] taskChildren;

    [Header("Set Dynamically")]
    public int itemsCollected;

    public string CurrentText()
    {
        if (isTypeHuman)
        {
            return taskDescription;
        }
        return taskDescription + " [" + itemsCollected + "/" + numItems + "]";
    }

    // Update is called once per frame
    public void UpdateTask()
    {
        itemsCollected++;
        toDoList.UpdateList(taskParent);
        if (itemsCollected.Equals(numItems) || isTypeHuman) //Human tasks do not have items.
        {
            toDoList.FinishedTask(taskParent);
            taskParent.SetActive(false); //deactivates the task. Really only matters for Endless Mode
        }
    }

    //A method only needed for endless mode: It re-enables each child so that the task can start anew. 
    public void RestartTask()
    {
        foreach(GameObject needyChild in taskChildren)
        {
            needyChild.SetActive(true);
        }
        itemsCollected = 0;
        toDoList.UpdateList(taskParent);
        
    }
}
