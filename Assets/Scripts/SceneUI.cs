using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SVR;

public class SceneUI : MonoBehaviour
{
    public NetworkLoadScene sceneLoad;

    public void goLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void goEasyLevelScene()
    {
        sceneLoad.sceneName = "Scene2";
    }

    public void goMediumLevelScene()
    {
        sceneLoad.sceneName = "Scene";
    }

    public void goHardLevelScene()
    {
        sceneLoad.sceneName = "Scene";
    }
}
