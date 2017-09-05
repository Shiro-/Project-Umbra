using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpCollision : MonoBehaviour
{

    //this should also be considered for change
    //i think
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
