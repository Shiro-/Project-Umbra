﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tmpPlayerController : MonoBehaviour
{
    //We need rigidbody for movement
    private Rigidbody rb;

    private GameObject flashCone;
    //private Collider flashCollider;
    private Transform flashT;

    //Controller functionality
    private bool controllerMode;
    private Vector3 lastMousePos;

    //Lamp stuff
    public Light flashlight;
    public float lightLevel;
    private float lightIntensity;

    public int playerHP;
    public float flashlightBat;

    public Text playerHPText;

    //https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
    void Awake()
    {
        flashlight.enabled = !flashlight.enabled;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        flashCone = GameObject.FindWithTag("Flashlight");
        //flashCollider = flashCone.GetComponent<Collider>();
        flashT = flashCone.GetComponent<Transform>();
        //Flashlight is off to start, so we lower it at the beginning
        flashT.Translate(new Vector3(0f, 0f, -10f));

        lightIntensity = lightLevel;
    }

    //Updates every frame
    void Update()
    {

        SetHPText();

        //https://docs.unity3d.com/ScriptReference/Input.GetKeyUp.html
        //This looks so bad and there is probably a better way to do this
        if ((Input.GetKeyUp(KeyCode.F) || Input.GetButtonDown("Flashlight")) && flashlightBat > 0.0f)
        {
            flashlight.enabled = !flashlight.enabled;
            //flashCollider.enabled = !flashCollider.enabled;

            //Move flashlight collider up and down
            if (!flashlight.enabled)
            {
                flashT.Translate(new Vector3(0f, 0f, -10f));
            }
            else
            {
                flashT.Translate(new Vector3(0f, 0f, 10f));
            }


        }

        if (flashlight.enabled && flashlightBat > 0.0f)
        {
            flashlightBat -= 0.01f;

            if(flashlightBat <= 0.0f)
            {
                flashlight.enabled = !flashlight.enabled;
            }
        }

        //Give a bit of variance so the light isn't flickering between the same intensity all the time
        lightIntensity = Random.Range(lightLevel - 0.3f, lightLevel + 0.3f);

        //Light flicker
        flashlight.GetComponent<Light>().intensity = lightIntensity
            / 2f + Mathf.Lerp(lightIntensity - 0.6f, lightIntensity + 0.3f, Mathf.Cos(Time.time * 30));
    }

    //Called before Physics step
    void FixedUpdate()
    {
        //this movement definitely needs a fix but for testing we use it
        //Getting inputs
        //float moveX = Input.GetAxis("Horizontal");
        //float moveZ = Input.GetAxis("Vertical");

        //Mouse aim/rotation
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane mousePlane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        //Won't update aim using mouse cursor unless the mouse has moved (allows controller aiming)
        if (mousePlane.Raycast(ray, out distance) && lastMousePos != Input.mousePosition)
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 dir = target - transform.position;
            float rotation = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            rb.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        lastMousePos = Input.mousePosition;

        //Controller aim/rotation
        if (Input.GetAxis("Right Horizontal") != 0.0f || Input.GetAxis("Right Vertical") != 0.0f)
        {
            controllerMode = true;
            Vector3 shootDir = Vector3.right * Input.GetAxis("Right Horizontal") + Vector3.forward * Input.GetAxis("Right Vertical");
            rb.transform.rotation = Quaternion.LookRotation(shootDir, Vector3.up);
        }

        //Vector3 movement = new Vector3(moveX, 0.0f, moveZ);
        //rb.AddForce(movement * spd);

        //transform.Translate(moveX, 0.0f, moveZ);
    }

    //check collisions
    private void OnTriggerEnter(Collider other)
    {
        //For now we will just destroy ourselves and the enemy
        //for future reference we will have hp and other things
        if(other.tag == "WispEnemy" && playerHP > 0)
        {    
            //Destroy(other.gameObject);
            //Destroy(gameObject);
            playerHP -= 10;
            Destroy(other.gameObject);
            if(playerHP == 0)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
        //Changed tags to prepare for different enemies
        //and also hp values for the player
    }

    void SetHPText()
    {
        playerHPText.text = "Player HP: " + playerHP.ToString();

        if(playerHP == 0 && GameObject.FindWithTag("Player") == null)
        {
            playerHPText.text = "Dead ";
        }
    }
}
