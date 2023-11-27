// Creator: Ava Fritts
//Date Created: May 10th 2022?

// Last edited: May 20th 2022
// Description: The script to manage all the tasks in a given level.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task_Manager : MonoBehaviour
{
    //Variables//

    //GameManager GM; //I plan to move certain variables to the game Manager, which means this will soon be needed.

    [Header("Tasks: Set in Inspector")]
    public List<GameObject> tasks;
    [Tooltip("This task is only activated once the number of uncompleted tasks is at 0. It must contain an arrow.")]
    public GameObject bossTask; //the final task
    public Text[] activeText; //the text boxes in the area... Needs Improvement.
    public Text bossText; //the text that shows up for the boss task

    [Space(10)]

    [Header ("Set Dynamically")]
    public int numberTasks; //how many tasks are going to be active in the Level
    public List<GameObject> activeTasks;
    

    public bool endlessMode = false; //DO NOT SET TO TRUE UNTIL YOU START TO WORK ON THE SECRET LEVEL

    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.GM.difficulty) //the difficulty determines how many tasks are needed.
        {
            case 0: //Easy
                numberTasks = 2;
                break;
            case 5: //Medium
                numberTasks = 3;
                break;
            //case 10: //Hard
              //  numberTasks = 4;
                //break;
            default:
                numberTasks = 2;
                break;
        }

        for(int i = 0; i < numberTasks; i++)
        {
            int taskPicked = Random.Range(0, tasks.Count); //picks a random task
            activeTasks.Add(tasks[taskPicked]);
            Task taskText = tasks[taskPicked].GetComponent<Task>(); //get the corresponding task.
            activeText[i].text = taskText.CurrentText(); //set the text to the text in the task
            tasks.RemoveAt(taskPicked);
        }

        foreach (GameObject selectedTasks in activeTasks)
        {
            selectedTasks.SetActive(true);
        }

    } //end Start

    void CreateEndlessTasks(int indexForText) //USE ONLY WHEN WORKING ON ENDLESS MODE
    {
        //Dev log: The way the list is being worked, if the completed task is not at the end, the text goes weird

        //Pick a task from the range
        int taskPicked = Random.Range(0, tasks.Count - 1); //it is minus 1 since the newest task is at the end.

        //put the task into the active list
        //activeTasks.Add(tasks[taskPicked]);
        activeTasks.Insert(indexForText, tasks[taskPicked]);
        //set the parent active
        activeTasks[indexForText].SetActive(true);
        Task taskText = tasks[taskPicked].GetComponent<Task>();

        tasks.RemoveAt(taskPicked);
        taskText.RestartTask(); //re-enable the children.
        //activeText[indexForText].text = taskText.CurrentText(); //get the new, updated text.
    } 

    public void UpdateList(GameObject targetTask)
    {
        Debug.Log(targetTask.name); //makes sure it actually can read the task.
        Task taskText = targetTask.GetComponent<Task>(); //idk why but it glitched out so badly earlier.
        int taskNumber = activeTasks.IndexOf(targetTask);
        activeText[taskNumber].text = taskText.CurrentText(); //set the text to the text in the task
    }

    public void FinishedTask(GameObject finalizedTask)//int taskNumber)
    {
        int taskNumber = activeTasks.IndexOf(finalizedTask);

        if (endlessMode) //if you are in the secret endless mode
        {
            tasks.Add(activeTasks[taskNumber]); //put the task back in the list
            activeTasks.RemoveAt(taskNumber); //remove from the completed list
            CreateEndlessTasks(taskNumber);
        }
        else
        {
            activeText[taskNumber].text = "DONE"; //change the tasks
            numberTasks--;
        }
        
        if(numberTasks <= 0) //if you have finished all the tasks
        {
            InitializeBossTask();
            //tasks finished event invoke
        }
    } //end FinishedTask

    public void InitializeBossTask()
    {
        foreach (Text taskDescription in activeText)
        {
            taskDescription.text = "";
        }
        bossTask.SetActive(true); //activate the Boss task

        Task currentBossText = bossTask.GetComponent<Task>(); //get the task from the boss
        bossText.text = currentBossText.CurrentText(); //put the boss text on the screen;
    } //end InitializeBossTask
}
