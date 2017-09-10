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
    [Tooltip("Time between picking a new target point")]
    public float wanderT;

    private Rigidbody rb;
    private Vector3 moveForce;
    private Vector3 wanderPoint;
    //removed for now
    //private float maxSeek = 1.5f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("ChangeDirection", 0.0f, wanderT);
        wanderPoint.y = home.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        moveForce = (wanderPoint - rb.position) * speed * Time.deltaTime;
        rb.AddForce(moveForce);
    }

    void ChangeDirection()
    {
        //if (Mathf.Abs(rb.position.x - home.x) > range
        //    || Mathf.Abs(rb.position.y - home.y) > range)
        //{
        wanderPoint.x = home.x + Random.Range(-range, range);
        wanderPoint.z = home.z + Random.Range(-range, range);
        //}
        //else
        //{
        //    wanderPoint.x = rb.position.x + Random.Range(-dunno, dunno);
        //    wanderPoint.z = rb.position.z + Random.Range(-dunno, dunno);
        //}
    }
}
