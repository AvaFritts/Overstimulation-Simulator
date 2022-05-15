// Creator: Ava Fritts
//Date Created: May 10th 2022?

// Last edited: May 15th 2022
// Description: The script to manage all the tasks in a given level.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task_Manager : MonoBehaviour
{

    GameManager GM;

    public int numberTasks;

    public List<GameObject> tasks;
    public List<GameObject> activeTasks;

    public bool endlessMode = false; //DO NOT SET TO TRUE UNTIL YOU START TO WORK ON THE SECRET LEVEL
    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.GM.difficulty) //the difficulty determines how many tasks are needed.
        {
            case 0:
                numberTasks = 2;
                break;
            case 5:
                numberTasks = 3;
                break;
            case 10:
                numberTasks = 5;
                break;
            default:
                numberTasks = 2;
                break;
        }

        for(int i = 0; i < numberTasks; i++)
        {
            CreateTasks();
        }
    }

    void CreateTasks()
    {
            int taskPicked = Random.Range(0, tasks.Count);
            activeTasks.Add(tasks[taskPicked]);
            tasks.RemoveAt(taskPicked);
    }

    // Update is called once per frame
    void UpdateList()
    {
        
    }

    void FinishedTask(int taskNumber)
    {
        if (endlessMode) //if you are in the secret endless mode
        {
            tasks.Add(activeTasks[taskNumber]); //put the task back in the list
            activeTasks.RemoveAt(taskNumber); //remove from the completed list
            CreateTasks();
        }
        else
        {
            //
        }
    }
}
