﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayerController : MonoBehaviour
{
    //Testing, subject to change
    public float spd;

    void Update()
    {
        //If the player dies destroy the dummy controller
        if (GameObject.FindWithTag("Player") == null)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        //The dummy controller will handle the movement 
        //While the player itself will controller the rotation
        float moveX = Input.GetAxis("Horizontal") * spd * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * spd * Time.deltaTime;
        transform.Translate(moveX, 0.0f, moveZ);
    }
}
