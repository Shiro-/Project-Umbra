using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpSeeking : MonoBehaviour
{
    //Seek target
    public GameObject player;

    //Enemy properties
    public float spd;
    public float min;
    //public float moveX;
    //public float moveZ;
    //public float max;

    private bool chase;

    void Start()
    {
        chase = !chase;
    }

    void Update()
    {
        //Not Tested yet
        //Check if player is within distance of enemy
        //Check if chasing is true
        //Check if the object found is the Player
        //https://docs.unity3d.com/ScriptReference/Vector3.Distance.html
        //if (transform.position.z <= min && chase == false && GameObject.FindWithTag("Player") != null)
        //if (Vector3.Distance(transform.position, player.transform.position) >= min && chase == false && GameObject.FindWithTag("Player") != null)
        //{
        //    //If the enemy is within the minimum dist from the player
        //    //Look at the player and start moving towards them
        //    transform.LookAt(player.transform);
        //    chase = !chase;
        //}

        //if (chase == true)
        //{
        //    transform.position += transform.forward * spd;
        //}
        //else
        //{
        //    //transform.position(Random.RandomRange(), 0.0f, )
        //}
        if (GameObject.FindWithTag("Player") != null)
            transform.LookAt(player.transform);

        if (Vector3.Distance(transform.position, player.transform.position) <= min)
            Debug.Log("beeb");

    }
}
