/**** 
 * Created by: Ava Fritts
 * Date Created: November ??, 2022
 * 
 * Last Edited by: Ava Fritts
 * Last Edited: December 2nd, 2022
 * 
 * Description: Basic Audio Manager I made. 
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    /* VARIABLES */
    #region AudioManager Singleton
    static private AudioManager am; //refence GameManager
    static public AudioManager AM { get { return am; } } //public access to read only gm 

    //Check to make sure only one gm of the GameManager is in the scene
    void CheckAudioManagerIsInScene()
    {

        //Check if instnace is null
        if (am == null)
        {
            am = this; //set gm to this gm of the game object
            Debug.Log(am);
        }
        else //else if gm is not null a Game Manager must already exsist
        {
            Destroy(this.gameObject); //In this case you need to delete this gm
        }
        DontDestroyOnLoad(this); //Do not delete the GameManager when scenes load
        Debug.Log(am);
    }//end CheckGameManagerIsInScene()
    #endregion

    private AudioSource mainSongMaker;
    [Header("GENERAL SONGS")]
    [Tooltip("Order the songs in the exact same way as the levels in the Level Select Scene")]
    public AudioClip[] levelSongs;
    [Tooltip("This song plays throughout the menus")]
    public AudioClip menuSong;
    [Tooltip("The game over song")]
    public AudioClip sadSong;
    [Tooltip("The victory song")]
    public AudioClip victorySong;
    [Tooltip("The Song when you're about to Meltdown")]
    public AudioClip deathSong;



    // Start is called before the first frame update
    void Awake()
    {
        CheckAudioManagerIsInScene();
        mainSongMaker = this.GetComponent<AudioSource>();
    }

    public void MenuMusic()
    {
        if(mainSongMaker.clip != menuSong) //If the menu song is already playing, don't replay it.
        {
            mainSongMaker.clip = menuSong;
            mainSongMaker.Play();
        }
    }

    public void LevelEndMusic()
    {
        if (GameManager.GM.playerWon)
        {
            mainSongMaker.clip = victorySong;
        }
        else
        {
            mainSongMaker.clip = sadSong;
        }

        mainSongMaker.Play(); //Play the chosen song
    }

    public void LevelMusic(int arraySong) //The variable is the same as the one used to load the level
    {
        mainSongMaker.clip = levelSongs[arraySong];
        mainSongMaker.Play();
    }

    public void DeathSong()
    {
        mainSongMaker.Stop();
        mainSongMaker.clip = deathSong;
        mainSongMaker.Play();
    }
}
