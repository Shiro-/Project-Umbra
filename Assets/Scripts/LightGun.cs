using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGun : MonoBehaviour
{
    //Drag in the Bullet Emitter from the Component Inspector.
    public GameObject Bullet_Emitter;

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;

    public float bulletSpeed;
    public float fireRate;
    public int maxRounds;
    public float reloadSpeed;
    public float interruptDelay;

    private float lastFire;
    private int currentRounds;
    private float rTime;
    private bool reloading;


    // Use this for initialization
    void Start()
    {
        currentRounds = 6;
        lastFire = 0.0f;
        rTime = 0.0f;
        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse1 
        if (Input.GetButtonDown("Fire1"))
        {
            if (reloading)
            {
                reloading = false;
                rTime = Time.time;
            }
            else if (Time.time - lastFire > fireRate && currentRounds > 0 && Time.time - rTime > interruptDelay)
            {
                Fire();
            }
        }

        //Moved reloading to update and removed fixedupdate
        if (Input.GetKeyDown(KeyCode.R) && !reloading && currentRounds < maxRounds)
        {
            reloading = true;
            rTime = Time.time;
        }

        if (reloading)
        {
            Reload();
        }
    }

    private void Fire()
    {
        currentRounds--;

        //The Bullet instantiation happens here.
        GameObject tmpBulletHandler;
        tmpBulletHandler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

        //Fx
        //to be added soon
        //Instantiate(bulletFx, some position, Quaternion.identity);

        //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
        //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
        tmpBulletHandler.transform.Rotate(Vector3.left * 90);

        //Retrieve the Rigidbody component from the instantiated Bullet and control it.
        Rigidbody tmpRB;
        tmpRB = tmpBulletHandler.GetComponent<Rigidbody>();

        //tmpRB.AddForce(transform.forward * Bullet_Forward_Force);
        tmpRB.useGravity = false;
        tmpRB.velocity = transform.forward * bulletSpeed;

        //Bullets self destruct after x seconds
        Destroy(tmpBulletHandler, 5.0f);
    }

    private void Reload()
    {
        if (Time.time - rTime > reloadSpeed)
        {
            currentRounds++;
            rTime = Time.time;
        }

        if (currentRounds >= maxRounds)
        {
            reloading = false;
        }
    }

    //private void OnCollisionEnter(Collision other, Collider otherx)
    //{
    //    if(otherx.tag == "Something")
    //    {
    //        //Create a light on the position of the game object
    //        ContactPoint contact = other.contacts[0];
    //        Vector3 pos = contact.point;

    //        //Light
    //        //Testing for the implementation of a new weapon type
    //        //https://docs.unity3d.com/ScriptReference/Light.html
    //        //Instantiate(light object?, position, rotation);
    //        GameObject lightGameObject = new GameObject("The Light");
    //        Light lightComp = lightGameObject.AddComponent<Light>();
    //        lightComp.intensity = 100;
    //        lightComp.color = Color.red;
    //        lightGameObject.transform.position = pos;
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 pos = contact.point;
        GameObject lightTest = new GameObject("The Light");
        Light lightcomp = lightTest.AddComponent<Light>();
        lightcomp.intensity = 100;
        lightcomp.color = Color.green;
        lightTest.transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WispEnemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
