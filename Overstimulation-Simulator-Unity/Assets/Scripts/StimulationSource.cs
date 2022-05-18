// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: May 16th 2022
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

    public Overstimulation overstimGO;
    public GameObject playerChar;
    public bool paused;
    
    //public Transform target;
    public Collider objectCollider; //was originally SphereCollider. Might Change back.
    private ParticleSystem _stimulationSystem;


    void Start()
    {
        paused = true;
        _stimulationSystem = this.GetComponent<ParticleSystem>();
        _stimulationSystem.Stop();
    }

        // Update is called once per frame
        void Update()
    {
        if (!paused) //DO NOT UPDATE IF YOU ARE NOT IN RANGE
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
