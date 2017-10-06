using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject player;

    //public float enemySpeed;
    public float enemyHP;
    public float min;

    //This state stuff might be moved to gamecontroller
    //subject to change
    //Temporary
    //private int state;
    private bool state;
    private bool chase;
    private bool attack;
    private bool wander;
    private bool change;

    private State cState;

    public enum State
    {
        chase,
        //attack,
        wander
    }

    void Start()
    {

    }

    void Update()
    {
        //We call this badboy
        CheckStateChange();
    }

    //Not working properly yet
    private void CheckStateChange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= min && GameObject.FindWithTag("Player"))
        {
            Debug.Log("beeb");
            transform.LookAt(player.transform);
            //state = 1;
            chase = true;
            wander = false;
            //change = true;
            Transition(State.chase);
        }
        else
        {
            //state = 2;
            chase = false;
            wander = true;
            //change = true;
            Transition(State.wander);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RevolverBullet" && enemyHP > 0)
        {
            enemyHP -= 10;
            Destroy(other.gameObject);
            if(enemyHP == 0)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

        if(other.tag == "Player" && enemyHP > 0)
        {
            //For now if both the enemy and player collide with each other
            //They get destroyed
            //Subject to change
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void Transition(State state)
    {
        change = true;
        cState = state;
    }

    //Notes on what to do:
    /*
     *
     * For now we will have 1 other enemy (Subject to change)
     * Unlike the wisp/ghost, it will probably have different states that determine what actions it will take
     * For example:
     *
     * Wander - The enemy wanders around aimlessly unless triggered by the player
     * Chase - If the player comes within a certain distance of the enemy or
     *       - If the player shines the flashlight at the enemy or
     *       - If the player makes too much noise
     *       - Then the enemy will start chasing the player
     *       (These are subject to change)
     *       - If the enemy is close enough to the player, the player will take damage
     *
     * Other things to be determined
     *
     */
}
