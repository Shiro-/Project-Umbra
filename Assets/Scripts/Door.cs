using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Transform t;
    private GameObject player;

    [Tooltip("Rotation value to open the door.")]
    public float openR;

    // Use this for initialization
    void Start()
    {
        t = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //For now we will just destroy ourselves and the enemy
        //for future reference we will have hp and other things
        if (other.tag == "Player")
        {
            t.rotation = Quaternion.Euler(0, openR, 0);
        }
        //Changed tags to prepare for different enemies
        //and also hp values for the player
    }
}
