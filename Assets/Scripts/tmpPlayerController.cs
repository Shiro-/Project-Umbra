using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpPlayerController : MonoBehaviour
{
    //We need rigidbody for movement
    private Rigidbody rb;

    public float spd;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Updates every frame
    void Update()
    {
        
    }

    //Called before Physics step
    void FixedUpdate()
    {
        //Getting inputs
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0.0f, moveZ);
        rb.AddForce(movement * spd);
    }
}
