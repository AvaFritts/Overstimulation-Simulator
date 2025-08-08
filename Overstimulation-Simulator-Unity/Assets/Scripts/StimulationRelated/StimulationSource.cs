// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: July 21st 2025
// Description: The script for the sources of stimulation
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StimulationSource : MonoBehaviour
{
    //Variables//

    [Header("Set in Inspector")]
    public float multModifier; //the modifier for the stimulation.
    public float maxModifier;
    public bool staticValue;

    [Space(10)]

    //public Overstimulation overstimGO;
    public GameObject playerChar;

    [Space(10)]

    //public Transform target;
    public Collider objectCollider; //was originally SphereCollider. Might Change back.
    private ParticleSystem _stimulationSystem;
    private AudioReverbFilter muffler;
    public int minParticles;
    //public int normalParticles; //uncomment if the particle number doesn't reset between rounds.

    [Header("Set Dynamically")]
    public bool paused;
    public AudioSource badAudio;

    void Awake()
    {
        badAudio = GetComponent<AudioSource>();
        paused = true;
        muffler = this.GetComponent<AudioReverbFilter>();

        _stimulationSystem = GetComponent<ParticleSystem>(); //get the particle system

        //As the static value only needs to me calculated once...
        if (staticValue)
        {
            multModifier = maxModifier;
        }

        if (GameManager.GM.stilumationReducer) //if the stimulation reducer is on, reduce particles
        {
            var main = _stimulationSystem.main;
            main.maxParticles = minParticles;
        }
        _stimulationSystem.Stop();
        GameManager.GM.MuffledNoises.AddListener(ActivateMuffle);
        GameManager.GM.NormalNoises.AddListener(DeactivateMuffle);
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused) //DO NOT UPDATE IF YOU ARE NOT IN RANGE
        {
            if (!staticValue) //this update is for dynamic sources.
            {
                float distanceToTarget = Vector3.Distance(playerChar.transform.position, objectCollider.transform.position);

                //Debug.Log("Current Position: " + playerChar.transform.position);
                //Debug.Log("Current Distance: " + distanceToTarget);
                if (distanceToTarget < 1) { distanceToTarget = 1; }
                //if (distanceToTarget < objectCollider.radius)
                //{
                multModifier = maxModifier / distanceToTarget;
                //}
            }
 
        }

        //if(GameManager.GM.gameState != 
    }

    public void ActivateMuffle()
    {
        muffler.enabled = true;
    }

    public void DeactivateMuffle()
    {
        muffler.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            paused = false;
            _stimulationSystem.Play();
            //overstimGO.overStimMult += multModifier; //add the multiplier to the modifier
        }
    }

    //for Endless Mode
    private void OnDisable()
    {
        _stimulationSystem.Stop();
        paused = true;
    }

    /*private void OnEnable()
    {
        _stimulationSystem.Stop();
        paused = true;
    }*/

    private void OnTriggerExit(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            paused = true;
            _stimulationSystem.Stop();
            //overstimGO.overStimMult -= multModifier; //add the multiplier to the modifier
        }
    }

}