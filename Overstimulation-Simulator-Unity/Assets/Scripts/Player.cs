// Creator: Ava Fritts
//Date Created: May 6th 2022

// Last edited: May 6th 2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        //get the position
        pos.x = this.transform.position.x;
        pos.x += Input.GetAxis("Horizontal") * 2 * Time.deltaTime;

        //set the position
        this.transform.position = pos;
    }
}
