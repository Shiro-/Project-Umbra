using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Willowisp : MonoBehaviour
{
    [Tooltip("The wisp will try to remain within range of its home.")]
    public Vector3 home;
    public float speed;
    [Tooltip("Wisp's max range from home")]
    public float range;
    public float wanderT;

    private Rigidbody rb;
    private Vector3 moveForce;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("ChangeDirection", 1.0f, wanderT);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void ChangeDirection ()
    {
        Vector3 wanderPoint = new Vector3(home.x + Random.Range(-range, range), home.y, home.z + Random.Range(-range, range));
        moveForce = (wanderPoint - rb.position) * speed;
        rb.AddForce(moveForce);
    }
}
