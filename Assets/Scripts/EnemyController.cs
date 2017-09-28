using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //public float enemySpeed;
    public float enemyHP;

    //This state stuff might be moved to gamecontroller
    //subject to change
    private bool state;
    private bool chase;
    private bool attack;
    private bool wander;

    public enum State
    {
        chase,
        attack,
        wander
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RevolverBullet" && enemyHP > 0)
        {
            enemyHP -= 10;
            Destroy(other.gameObject);
            if(enemyHP == 0)
            {
                //Testing for the implementation of a new weapon type
                //https://docs.unity3d.com/ScriptReference/Light.html
                //Instantiate(light object?, position, rotation);
                GameObject lightGameObject = new GameObject("The Light");
                Light lightComp = lightGameObject.AddComponent<Light>();
                lightComp.intensity = 100;
                lightComp.color = Color.red;
                lightGameObject.transform.position = gameObject.transform.position;

                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

        if(other.tag == "Player" && enemyHP > 0)
        {
            //For now if both the enemy and player collide with each other
            //They get destroyed
            //Subject to change
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    //Notes on what to do:
    /*
     * 
     * For now we will have 1 other enemy (Subject to change)
     * Unlike the wisp/ghost, it will probably have different states that determine what actions it will take
     * For example:
     * 
     * Wander - The enemy wanders around aimlessly unless triggered by the player
     * Chase - If the player comes within a certain distance of the enemy or
     *       - If the player shines the flashlight at the enemy or
     *       - If the player makes too much noise
     *       - Then the enemy will start chasing the player
     *       (These are subject to change)
     *       - If the enemy is close enough to the player, the player will take damage
     * 
     * Other things to be determined
     * 
     */
}
