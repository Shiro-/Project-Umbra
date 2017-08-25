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

        //Mouse aim/rotation
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane mousePlane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (mousePlane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 dir = target - transform.position;
            float rotation = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            rb.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        //Vector3 movement = new Vector3(moveX, 0.0f, moveZ);
        //rb.AddForce(movement * spd);

        transform.Translate(moveX, 0.0f, moveZ);
    }
}
