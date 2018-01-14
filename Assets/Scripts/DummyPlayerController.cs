using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayerController : MonoBehaviour
{
    //Testing, subject to change
    public float spd;

    public GameObject boundaryX;
    public GameObject boundaryZ;

    private float moveX;
    private float moveZ;

    void Update()
    {
        //If the player dies destroy the dummy controller
        if (GameObject.FindWithTag("Player") == null)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        //The dummy controller will handle the movement 
        //While the player itself will controller the rotation
        moveX = Input.GetAxis("Horizontal") * spd * Time.deltaTime;
        moveZ = Input.GetAxis("Vertical") * spd * Time.deltaTime;
        transform.Translate(moveX, 0.0f, moveZ);

        //Boundary check
        /*
         * In simplest terms, This will prevent the player from leaving the play area
         * AKA, the size of the purple floor.
         * 
         * There are other ways to create this solution such as checking for collisions with walls.
         * But since our player is a trigger, we can't use that method.
         * 
         * So while we have walls, they are mostly decoration since they won't do anything.
         * 
         * https://docs.unity3d.com/ScriptReference/Mathf.Clamp.html
         */
        Vector3 position = transform.position;
        //position.x = Mathf.Clamp(position.x + moveX, -14.0f, 14.0f);
        //position.z = Mathf.Clamp(position.z + moveZ, -14.0f, 14.0f);

        position.x = Mathf.Clamp(position.x + moveX, -(boundaryX.transform.position.x), boundaryX.transform.position.x);
        position.z = Mathf.Clamp(position.z + moveZ, -(boundaryZ.transform.position.z), boundaryZ.transform.position.z);

        transform.position = position;
    }
}
