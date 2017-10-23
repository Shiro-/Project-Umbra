using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public int health;
    public float speed;
    public float dmgMod = 1.0f;

    public float actDelay = 0.5f;

    private Rigidbody rb;
    private GameObject player;
    private Vector3 seekTarget;
    private Vector3 chaseDir;

    private LightCheck check;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        seekTarget = player.GetComponent<Rigidbody>().position;

        check = GetComponentInParent<LightCheck>();
    }
	
	void FixedUpdate ()
    {
		if (health <= 0)
        {
            Destroy(gameObject);
        }

        //Set player as target and get direction vector
        seekTarget = player.GetComponent<Rigidbody>().position;
        chaseDir = Vector3.Normalize(seekTarget - rb.position);

        rb.velocity = new Vector3 (chaseDir.x * speed, 0, chaseDir.z * speed);

        //Turns to stone when lit
        if (check.isLit)
        {
            rb.velocity = Vector3.zero;
            dmgMod = 0.1f;
            //rb.velocity += new Vector3(0f, 10.0f, 0f);
        }
        else
        {
            dmgMod = 1.0f;
        }
    }

    //Bullet collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            //Should look like this later probably
            //health - other.damage 

            health -= Mathf.RoundToInt(10 * dmgMod);
            Destroy(other.gameObject);
        }
    }
}
