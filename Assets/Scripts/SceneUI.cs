using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SVR;

public class SceneUI : MonoBehaviour
{
    public NetworkLoadScene sceneLoad;
    public static string levelUI;

    public void goLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void goEasyLevelScene()
    {
        levelUI = "easy";
        sceneLoad.sceneName = "Scene2";
    }

    public void goMediumLevelScene()
    {
        levelUI = "medium";
        sceneLoad.sceneName = "Scene";
    }

    public void goHardLevelScene()
    {
        levelUI = "hard";
        sceneLoad.sceneName = "Scene";
    }
}
