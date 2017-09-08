using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Array enemies
    public GameObject[] enemies;
    public Light winLight;
    //at some point we may want to have multiple lights in one room
    //public Light[] Lights;

    public int initialEnemyCount;
    public int randomEnemyCount;

    public float waitTime;
    public float spawnTime;

    //figure how to spawn at different positions
    public Vector3 spawnPosition;

    //change this later
    private int check;

    // Use this for initialization
    void Start()
    {
        check = 0;
        //https://docs.unity3d.com/Manual/Coroutines.html
        StartCoroutine(InitialEnemySpawn());
        StartCoroutine(RandomEnemySpawn());
    }

    //Initially, we want to spawn x amount of enemies before we randomly spawn them
    //Subject to change in the future
    IEnumerator InitialEnemySpawn()
    {
        //We want this to run before the RandomEnemySpawn
        //There is probably a better solution but this will do for now
        yield return new WaitForSeconds(1.0f);

        //Spawn x enemies in random locations
        for (int i = 0; i < initialEnemyCount; i++)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(-spawnPosition.x, spawnPosition.x), spawnPosition.y, Random.Range(-spawnPosition.z, spawnPosition.z)), Quaternion.identity);
        }
    }

    //https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
    IEnumerator RandomEnemySpawn()
    {
        winLight.enabled = false;
        yield return new WaitForSeconds(waitTime);

        while (true)
        {
            for (int i = 0; i < randomEnemyCount; i++)
            {
                //Random ranges lol good joke
                Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(-spawnPosition.x, spawnPosition.x), spawnPosition.y, Random.Range(-spawnPosition.z, spawnPosition.z)), Quaternion.identity);
                yield return new WaitForSeconds(spawnTime);

                check++;
                //print(check);
            }


            //This should definitly be changed so that the light turns on when all the enemies are killed
            //for now though we can leave it like this for testing purposes

            //To add:
            //Scoring
            //player hp and enemy hp
            //some other things
            if (check == randomEnemyCount)
            {
                winLight.enabled = true;
                break;
            }

            //For later reference:
            //GameObject.FindWithTag("Enemy");
        }
    }
}
