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

    private bool chase;
    private int state;

    void Start()
    {
        chase = !chase;
        state = 0;
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

        if (Vector3.Distance(transform.position, player.transform.position) <= min && GameObject.FindWithTag("Player"))
        {
            Debug.Log("beeb");
            transform.LookAt(player.transform);
            state = 1;
        }
        else
        {
            state = 2;
        }
    }

    void FixedUpdate()
    {
        switch(state)
        {
            case 1:
                StopAllCoroutines();
                StartCoroutine(Chase());
                break;
            case 2:
                StopAllCoroutines();
                StartCoroutine(Wander());
                break;
        }
    }

    //temp
    IEnumerator Chase()
    {
        transform.position += transform.forward * spd * Time.deltaTime;
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Wander()
    {
        //random directions
        transform.LookAt(RandomTarget(target));
        transform.position += transform.forward * spd * Time.deltaTime;

        yield return new WaitForSeconds(5.0f);
    }

    //Similar to the one in gamecontroller
    private Vector3 RandomTarget(Vector3 temp)
    {
        Vector3 newTarget = new Vector3(Random.Range(-temp.x, temp.x), temp.y, Random.Range(-temp.z, temp.z));

        return newTarget;
    }
}
