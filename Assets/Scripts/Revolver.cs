﻿using UnityEngine;
using System.Collections;

public class Revolver : MonoBehaviour
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

    private float lastFire;
    private int currentRounds;
    private float rTime;
    private bool reloading;

    public AudioSource pAudio;

    [Range(0.0f, 1.0f)]
    public float audioLevel;

    private AudioClip fire;
    private AudioClip fire_empty;
    private AudioClip reload;
    private AudioClip reload_end;

    // Use this for initialization
    void Start()
    {
        currentRounds = 6;
        lastFire = 0.0f;
        rTime = 0.0f;
        reloading = false;

        pAudio = GetComponent<AudioSource>();

        //Revolver sounds needed: maybe reload end/interrupt 
        fire = Resources.Load<AudioClip>("Sounds/GDC2016/Bullet-Time-14");
        fire_empty = Resources.Load<AudioClip>("Sounds/GDC2016/no_ammo");
        reload = Resources.Load<AudioClip>("Sounds/GDC2016/revolver_load_1");
        reload_end = Resources.Load<AudioClip>("Sounds/GDC2016/revolver_reload_end");
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

                pAudio.clip = reload_end;
                pAudio.PlayOneShot(reload_end, audioLevel);
            }
            else if (currentRounds <= 0)
            {
                pAudio.clip = fire_empty;
                pAudio.PlayOneShot(fire_empty, audioLevel);
            }
            else if (Time.time - lastFire > fireRate && Time.time - rTime > interruptDelay)
            { 
                currentRounds--;

                pAudio.clip = fire;
                pAudio.PlayOneShot(fire, audioLevel);

                //The Bullet instantiation happens here.
                GameObject tmpBulletHandler;
                tmpBulletHandler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

                //Fx
                //to be added soon
                //Instantiate(bulletFx, some position, Quaternion.identity);

                //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
                //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
                tmpBulletHandler.transform.Rotate(Vector3.left * 90);

                //Retrieve the Rigidbody component from the instantiated Bullet and control it.
                Rigidbody tmpRB;
                tmpRB = tmpBulletHandler.GetComponent<Rigidbody>();

                //tmpRB.AddForce(transform.forward * Bullet_Forward_Force);
                tmpRB.useGravity = false;
                tmpRB.velocity = transform.forward * bulletSpeed;

                //Bullets self destruct after x seconds
                Destroy(tmpBulletHandler, 5.0f);
            } 
        }

        //Moved reloading to update and removed fixedupdate
        if ((Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Reload")) && !reloading && currentRounds < maxRounds)
        {
            reloading = true;
            rTime = Time.time;
        }

        if (reloading)
        {
            if (Time.time - rTime > reloadSpeed)
            {
                pAudio.PlayOneShot(reload, audioLevel);

                currentRounds++;
                rTime = Time.time;
            }

            if (currentRounds >= maxRounds)
            {
                reloading = false;

                pAudio.clip = reload_end;
                pAudio.PlayOneShot(reload_end, audioLevel);
            }
        }
    }
}