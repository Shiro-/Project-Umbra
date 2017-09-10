using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpCollision : MonoBehaviour
{

    //this should also be considered for change
    //i think
    private void OnTriggerEnter(Collider other)
    {

        //Revolver bullet
        //Preparing for the implementation other types of weapons
        if(other.tag == "RevolverBullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
