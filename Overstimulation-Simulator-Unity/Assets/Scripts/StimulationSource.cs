// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: May 10th 2022
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

    public Overstimulation overstimGO;
    
    // Start is called before the first frame update
    void Start()
    {

        //overstimGO = GetComponent<Overstimulation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            overstimGO.overStimMult += multModifier; //add the multiplier to the modifier
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            overstimGO.overStimMult -= multModifier; //add the multiplier to the modifier
        }
    }
}
