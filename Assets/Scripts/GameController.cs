using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject ghost;
    public int ghostCount;
    public float waitTime;
    public float spawnTime;
    public Vector3 spawnPosition;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    //https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(waitTime);

        while(true)
        {
            for (int i = 0; i < ghostCount; i++)
            {
                Instantiate(ghost, new Vector3(Random.Range(-spawnPosition.x, spawnPosition.x), 1, Random.Range(-spawnPosition.z, spawnPosition.z)), ghost.transform.rotation);
                yield return new WaitForSeconds(spawnTime);
            }
            break;
        }
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
