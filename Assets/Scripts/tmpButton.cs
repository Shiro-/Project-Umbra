using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tmpButton : MonoBehaviour
{

    public void StartScene()
    {
        SceneManager.LoadScene("TestScene");
    }

}
