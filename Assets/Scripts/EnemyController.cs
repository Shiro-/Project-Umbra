using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float enemySpeed;
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
}
