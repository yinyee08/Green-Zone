using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
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
        PhotonPlayer.score_earn = 0f;
        PhotonPlayer.starting = true;
        levelUI = "easy";
        sceneLoad.sceneName = "Scene1";
    }

    public void goMediumLevelScene()
    {
        PhotonPlayer.score_earn = 0f;
        PhotonPlayer.starting = true;
        levelUI = "medium";
        sceneLoad.sceneName = "Scene2";
    }

    public void goHardLevelScene()
    {
        PhotonPlayer.score_earn = 0f;
        PhotonPlayer.starting = true;
        levelUI = "hard";
        sceneLoad.sceneName = "Scene3";
    }

    public void Exit()
    {
        Application.Quit();
    }
}
