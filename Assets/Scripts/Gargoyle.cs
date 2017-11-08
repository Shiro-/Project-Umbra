using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle : MonoBehaviour
{
    public float speed;

    public float actDelay = 0.5f;

    private Rigidbody rb;
    private GameObject player;
    private Vector3 seekTarget;
    private Vector3 chaseDir;

    private LightCheck check;
    private EnemyController controller;

    private float lastLit;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        seekTarget = player.GetComponent<Rigidbody>().position;

        check = GetComponentInParent<LightCheck>();
        controller = GetComponentInParent<EnemyController>();
    }
	
	void FixedUpdate ()
    {
        //Set player as target and get direction vector
        seekTarget = player.GetComponent<Rigidbody>().position;
        chaseDir = Vector3.Normalize(seekTarget - rb.position);

        //Enemy moves after it has been in darkness for some time
        if ((Time.time - lastLit > actDelay))
        {
            //Move straight towards the player (for now)
            rb.velocity = new Vector3(chaseDir.x * speed, 0, chaseDir.z * speed);
        }
        

        //Turns to stone when illuminated
        if (check.isLit)
        {
            //Immobile, and takes much less damage for the duration
            rb.velocity = Vector3.zero;
            controller.dmgMod = 0.1f;

            lastLit = Time.time;
        }
        else
        {
            controller.dmgMod = 1.0f;
        }
    }
}
