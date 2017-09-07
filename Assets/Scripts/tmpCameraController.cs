using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpCameraController : MonoBehaviour
{
    //For now, the camera will follow the player
    //this may change in the future

    //We need the player to follow
    public GameObject player;
    public Vector3 cameraPosition;

    //more todo:
    //increase the size of the room
    void Start()
    {

    }

    void Update()
    {
        //We want the camera to follow the position of the player
        //We add camera position to the player position so that the camera remains in the air (this vector3 can be specified for now)
        //instead of having the camera exactly on the player
        transform.position = player.transform.position + cameraPosition;

        //Another solution could be to just hard code the position in the air because
        //I don't think the camera will ever move from its position
        //Subject to change
    }
}
