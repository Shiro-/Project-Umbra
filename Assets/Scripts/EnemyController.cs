using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject player;

    //public float enemySpeed;
    public float enemyHP;
    public float dmgMod = 1.0f; //Damage multiplier to allow for some interesting enemy behaviours

    public float min;

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
            enemyHP -= (10 * dmgMod);
            Destroy(other.gameObject);
            if(enemyHP == 0)
            {
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

}
