using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Array enemies
    public GameObject[] enemies;
    public Light winLight;
    //at some point we may want to have multiple lights in one room
    //public Light[] Lights;

    //figure how to spawn at different positions
    public Vector3 spawnPosition;

    //public Transform player;

    public int initialEnemyCount;
    public int randomEnemyCount;

    public float waitTime;
    public float spawnTime;

    //change this later
    private int check;
    private int totalEnemyCount;

    private bool win;

    void Awake()
    {
        winLight.enabled = !winLight.enabled;
    }

    // Use this for initialization
    void Start()
    {
        check = 0;
        win = !win;
        totalEnemyCount = initialEnemyCount + randomEnemyCount;
        //https://docs.unity3d.com/Manual/Coroutines.html
        StartCoroutine(InitialEnemySpawn());
        StartCoroutine(RandomEnemySpawn());
    }

    void Update()
    {

        //There is probably a better way to do this
        if (check == totalEnemyCount && GameObject.FindWithTag("WispEnemy") == null)
        {
            win = !win;

            if (win == true)
            {
                winLight.enabled = !winLight.enabled;
                //Reset our checks so we never reach this point
                //This will be changed in the future with something better
                check = 0;
                win = !win;
            }
        }

        //Change scene moved here
        //If you add additional scenes you need to add it to the build settings so we can load it
        //File > Build Settings > Add Scene
        if(winLight.enabled == true && Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("DarkScene");
        }
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
            Instantiate(enemies[Random.Range(0, enemies.Length)], RandomPosition(spawnPosition), Quaternion.identity);

            check++;
        }

        yield break;
    }

    //https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
    IEnumerator RandomEnemySpawn()
    {
        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < randomEnemyCount; i++)
        {
            //Random ranges lol good joke
            Instantiate(enemies[Random.Range(0, enemies.Length)], RandomPosition(spawnPosition), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);

            check++;
            //print(check);
        }

        yield break;
        //This should definitly be changed so that the light turns on when all the enemies are killed
        //for now though we can leave it like this for testing purposes

        //To add:
        //Scoring
        //player hp and enemy hp
        //some other things
        //For later reference:
        //GameObject.FindWithTag("Enemy");
    }

    Vector3 RandomPosition(Vector3 pos)
    {
        //Create a random position
        Vector3 position = new Vector3(Random.Range(-pos.x, pos.x), pos.y, Random.Range(-pos.z, pos.z));

        return position;
    }
}
