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

    //Subject to change
    public Vector3 target;
    public Vector3[] patrolTargets;

    //private bool change;
    //private int state;

    //This value controls how long the enemy will wander in one direction
    //Subject to change if needed
    public float wanderTime;
    private float _wanderTime;

    private EnemyController eController;

    void Start()
    {
        //change = false;
        //state = 0;
        _wanderTime = Random.Range(0.1f, wanderTime);
        ChangeDirection();
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
        //if (GameObject.FindWithTag("Player") != null)
        //    transform.LookAt(player.transform);


    }

    void FixedUpdate()
    {

        //Another if statement goes here for when everything gets added

        //if (eController.change == true)
        //{
        //    switch (eController.cState)
        //    {
        //        case EnemyController.State.chase:
        //            StopAllCoroutines();
        //            StartCoroutine(Chase());
        //            break;
        //        case EnemyController.State.wander:
        //            StopAllCoroutines();
        //            StartCoroutine(Wander());
        //            break;
        //    }
        //}

        //We want the enemy to start wandering at the beginning
        Wander();

        if (Vector3.Distance(transform.position, player.transform.position) <= min && GameObject.FindWithTag("Player") != null)
        {
            //If the enemy is within the minimum dist from the player
            //Look at the player and start moving towards them
            transform.LookAt(player.transform);
            Chase();
            //chase = !chase;
        }
        else
        {
            //Timer for how long the enemy should wander in  one direction
            _wanderTime -= Time.deltaTime;

            //When the time reaches zero we choose a new radom target and
            //wander in that direction
            //We also reset our changeTime
            if(_wanderTime <= 0)
            {
                ChangeDirection();
                Wander();
                _wanderTime = Random.Range(0.1f, wanderTime);
            }
        }
    }

    //temp
    private void Chase()
    {

        transform.position += transform.forward * spd * Time.deltaTime;

    }

    private void Wander()
    {

        //random directions
        transform.position += transform.forward * spd * Time.deltaTime;
        //changeTime = 2.0f;
    }

    private void Patrol()
    {
        //Just testing
        //This could be done using a navmesh as well, I think

        /*
         * We give unity a set of points on the field
         * Because it is an array of vector3, we can go through it using a loop
         * (patrolTarget)
         * 
         * How it would work:
         * It loops to the first point,
         * The enemy then faces that direction and moves towards it
         * Once the enemy has reached the point, we loop to the second point and move towards that
         * 
         * Once the enemy has looped through all the points, we go back to the beginning of the loop
         * and start over again
         * 
         * This will probably use the wanderTime variable too
         * 
         */

        //When will we ever use this function if we have wander?
        //I was thinking the enemy initially wanders and once it detects the player the enemy will chase them
        //But since the player is faster then the enemy(for now) if we go outside the min distance of the enemy,
        //It will go into patrol instead of wander
    }

    private void ChangeDirection()
    {
        transform.LookAt(RandomTarget(target));
    }

    //Similar to the one in gamecontroller
    private Vector3 RandomTarget(Vector3 temp)
    {
        Vector3 newTarget = new Vector3(Random.Range(-temp.x, temp.x), temp.y, Random.Range(-temp.z, temp.z));

        return newTarget;
    }
}
