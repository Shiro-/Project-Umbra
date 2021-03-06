﻿using UnityEngine;
using System.Collections;

public class ShotgunPump : MonoBehaviour
{
    //Drag in the Bullet Emitter from the Component Inspector.
    public GameObject Bullet_Emitter;

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;

    public float bulletSpeed;
    public float fireRate;
    public int maxRounds;
    public float reloadSpeed;
    public float interruptDelay;

    //Shotgun specific stuff
    public int pellets;
    public float spread;
    public float pumpTime;

    private float lastFire;
    private int currentRounds;
    private float rTime;
    private bool reloading;
    private int roundChambered;

    //private float spreadVel;
    private Vector3 spreadVec;

    // Use this for initialization
    void Start()
    {
        currentRounds = 6;
        lastFire = 0.0f;
        rTime = 0.0f;
        reloading = false;
        roundChambered = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse1 
        if (Input.GetButtonDown("Fire1"))
        {
            if (reloading)
            {
                reloading = false;
                rTime = Time.time;
            }
            else if (Time.time - lastFire > fireRate && currentRounds > 0 
                && Time.time - rTime > interruptDelay + (pumpTime * roundChambered))
            {
                currentRounds--;

                for (int i = 0; i < pellets; i++)
                {
                    //The Bullet instantiation happens here.
                    GameObject tmpBulletHandler;
                    tmpBulletHandler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

                    //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
                    //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
                    tmpBulletHandler.transform.Rotate(Vector3.left * 90);

                    //Retrieve the Rigidbody component from the instantiated Bullet and control it.
                    Rigidbody tmpRB;
                    tmpRB = tmpBulletHandler.GetComponent<Rigidbody>();

                    //tmpRB.AddForce(transform.forward * Bullet_Forward_Force);
                    tmpRB.useGravity = false;

                    tmpRB.velocity = transform.forward * bulletSpeed;

                    //This ensures at least one pellet will always go down the middle
                    if (i > 0)
                    {
                        tmpRB.velocity += transform.right * Random.Range(-spread, spread);
                    }
                    
                    //Bullets self destruct after x seconds
                    Destroy(tmpBulletHandler, 5.0f);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R) && !reloading && currentRounds < maxRounds)
        {
            reloading = true;
            rTime = Time.time;
        }

        if (reloading)
        {
            if (Time.time - rTime > reloadSpeed)
            {
                currentRounds++;
                rTime = Time.time;
            }

            if (currentRounds >= maxRounds)
            {
                reloading = false;
            }
        }

    }
}