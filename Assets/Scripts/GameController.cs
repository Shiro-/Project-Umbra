using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject ghost;
    public Light winLight;
    public int ghostCount;
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
            for (int i = 0; i < ghostCount; i++)
            {
                Instantiate(ghost, new Vector3(Random.Range(-spawnPosition.x, spawnPosition.x), spawnPosition.y, Random.Range(-spawnPosition.z, spawnPosition.z)), ghost.transform.rotation);
                yield return new WaitForSeconds(spawnTime);

                check++;
                print(check);
            }

            if (check == ghostCount)
            {
                winLight.enabled = true;
                break;
            }
        }
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
