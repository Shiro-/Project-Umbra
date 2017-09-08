using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpPlayerController : MonoBehaviour
{
    //We need rigidbody for movement
    private Rigidbody rb;

    public Light flashlight;

    //https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
    void Awake()
    {
        flashlight.enabled = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Updates every frame
    void Update()
    {
        //https://docs.unity3d.com/ScriptReference/Input.GetKeyUp.html
        if (Input.GetKeyUp(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }

    //Called before Physics step
    void FixedUpdate()
    {
        //this movement definitely needs a fix but for testing we use it
        //Getting inputs
        //float moveX = Input.GetAxis("Horizontal");
        //float moveZ = Input.GetAxis("Vertical");

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

        //transform.Translate(moveX, 0.0f, moveZ);
    }

    //check collisions
    private void OnTriggerEnter(Collider other)
    {
        //For now we will just destroy ourselves and the enemy
        //for future reference we will have hp and other things
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
