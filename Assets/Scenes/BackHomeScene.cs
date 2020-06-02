using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackHomeScene : MonoBehaviour
{
    public void loadScene(int sceneNumber)
    {
        Debug.Log("sceneBuildIndex to load: " + sceneNumber);
        SceneManager.LoadScene(sceneNumber);
    }
}
