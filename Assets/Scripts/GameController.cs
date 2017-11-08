using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Array enemies
    public GameObject[] enemies;
    public Light winLight;
    //at some point we may want to have multiple lights in one room
    //public Light[] Lights;

    //figure how to spawn at different positions
    //public Vector3 spawnPosition;
    public GameObject[] spawnPosition;

    //Handling multiple spawn positions:
    //public GameObject[] spawnPosition;

    //public Transform player;

    public int initialEnemyCount;
    public int randomEnemyCount;

    public float waitTime;
    public float spawnTime;

    public Text enemyCountText;

    //change this later
    private int check;
    private int totalEnemyCount;

    private bool win;

    public enum SpawningStyle
    {
        RANDOM,
        ORIGINAL
    }

    public SpawningStyle spawningStyle;

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
        switch(spawningStyle)
        {
            case SpawningStyle.RANDOM:
                StartCoroutine(InitialRandomEnemySpawn());
                StartCoroutine(RandomEnemySpawn());
                break;
            case SpawningStyle.ORIGINAL:
                StartCoroutine(InitialOriginalEnemySpawn());
                StartCoroutine(OriginalEnemySpawn());
                break;
        }
        
    }

    void Update()
    {

        SetEnemyCountText();

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
        if(winLight.enabled == true /*&& Input.GetKeyUp(KeyCode.Space)*/)
        {
            SceneManager.LoadScene("Nice");
        }
    }

    //Initially, we want to spawn x amount of enemies before we randomly spawn them
    //Subject to change in the future
    IEnumerator InitialRandomEnemySpawn()
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
        //For later reference:
        //GameObject.FindWithTag("Enemy");
    }

    IEnumerator InitialOriginalEnemySpawn()
    {
        yield return new WaitForSeconds(1.0f);

        //Spawn x enemies in random locations
        for (int i = 0; i < initialEnemyCount; i++)
        {
            Instantiate(enemies[Random.Range(0, (enemies.Length - 1))], OriginalPosition(spawnPosition), Quaternion.identity);

            check++;
        }

        yield break;
    }

    //https://docs.unity3d.com/Manual/InstantiatingPrefabs.html
    IEnumerator OriginalEnemySpawn()
    {
        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < randomEnemyCount; i++)
        {
            //Random ranges lol good joke
            Instantiate(enemies[Random.Range(0, (enemies.Length - 1))], OriginalPosition(spawnPosition), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);

            check++;
            //print(check);
        }

        yield break;
    }

    //In the two coroutines above, instead of passing in one position from the gameobject for spawning,
    //We will pass in an array of gameobjects

    //This function will change to accept the array of gameobjects instead of one position
    Vector3 RandomPosition(GameObject[] pos)
    {
        //In here we will check the size of the array
        //Then once we know how big the array is, we get the position of each spawn point
        //Then we choose a /Random/ position for now

        //In the future, instead of choosing random positions, we will have different spawnpoints in the
        //level.

        //Within the editor, I've already made a prefab of a gameobject that contains the spawnpositions

        //Create a random position
        //Vector3 position = new Vector3(Random.Range(-pos.x, pos.x), pos.y, Random.Range(-pos.z, pos.z));

        //int conSize = pos.Length;
        Vector3[] newPositions = new Vector3[pos.Length];
        //Vector3[] finalPosition = new Vector3[2];

        for(int i = 0; i < pos.Length; i++)
        {
            newPositions[i] = new Vector3(Random.Range(-pos[i].transform.position.x, pos[i].transform.position.x), pos[i].transform.position.y, Random.Range(-pos[i].transform.position.z, pos[i].transform.position.z));
        }

        //switch(Random.Range(1, 2))
        //{
        //    case 1:
        //        //The new random position
        //        finalPosition[0] = newPositions[Random.Range(0, (pos.Length - 1))];
        //        break;
        //        //return newPositions[Random.Range(0, conSize)];
        //        //break;
        //    case 2:
        //        //The base position, meaning the original position of the spawnposition
        //        finalPosition[1] = pos[Random.Range(0, (pos.Length - 1))].transform.position;
        //        break;
        //        //return pos[Random.Range(0, conSize)].transform.position;
        //        //break;
        //}

        //Choose between the random position or the base position
        return newPositions[Random.Range(0, (pos.Length - 1))];
    }

    Vector3 OriginalPosition(GameObject[] pos)
    {
        //Yes, I know the function name is "Original Position" yet we return a random position
        //This will be fixed later

        //The plan for this function is as follows
        //If you want to create some sort of coordinated spawn or pattern it would be done here

        //How I vision this being done:
        //You set your spawn points in unity
        //Then within another file, maybe a text document, you specify the order in which you want to spawn
        //then we pass it to this function or the coroutine and make cool things happen

        //For now leaving it like this is fine
        return pos[Random.Range(0, (pos.Length - 1))].transform.position;
    }

    void SetEnemyCountText()
    {
        enemyCountText.text = "Enemy: " + totalEnemyCount.ToString();
    }
}
