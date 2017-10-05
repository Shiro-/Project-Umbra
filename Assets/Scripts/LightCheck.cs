using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCheck : MonoBehaviour
{
    private GameObject[] lightObjs;
    private Light lightComp;
    private GameObject player;

    //Use this in case we're inside of multiple light colliders
    private int collisions;

    public bool isLit;

	// Use this for initialization
	void Start ()
    {
        //This array will collect anything with the GlobalLight tag like the room light or lightning flash
        lightObjs = GameObject.FindGameObjectsWithTag("GlobalLight");
        player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (globalLight() && lightObjs != null)
        {
            isLit = true;
        }
        else if (collisions == 0)
        {
            isLit = false;
        }
	}

    bool globalLight()
    {
        for (int i = 0; i < lightObjs.Length; i++)
        {
            lightComp = lightObjs[i].GetComponent<Light>();
            //These are 'global' light sources so just check if they're on
            if(lightComp.enabled)
            {
                return true;
            }
        }

     
        return false;
    }

    //Hit light collider
    private void OnTriggerEnter(Collider other)
    {
        //Check for colliders with this tag (ex. flashlight)
        if (other.tag == "LightCollider")
        {
            isLit = true;
            collisions++;
        }
    }

    //Outside of light collider
    private void OnTriggerExit(Collider other)
    {
        //Check for colliders with this tag (ex. flashlight)
        if (other.tag == "LightCollider")
        {
            collisions--;
        }
    }
}
