using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Array enemies
    public GameObject[] enemies;
    public Light winLight;

    public int enemyCount;

    public float waitTime;
    public float spawnTime;

    public Vector3 spawnPosition;

    private int check;

    // Use this for initialization
    void Start()
    {
        check = 0;
        StartCoroutine(SpawnEnemies());
    }

    //https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
    IEnumerator SpawnEnemies()
    {
        winLight.enabled = false;
        yield return new WaitForSeconds(waitTime);

        while(true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                //Random ranges lol good joke
                Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(-spawnPosition.x, spawnPosition.x), spawnPosition.y, Random.Range(-spawnPosition.z, spawnPosition.z)), Quaternion.identity);
                yield return new WaitForSeconds(spawnTime);

                check++;
                print(check);
            }

            if (check == enemyCount)
            {
                winLight.enabled = true;
                break;
            }
        }
    }
}
