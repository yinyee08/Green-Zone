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
        levelUI = "easy";
        sceneLoad.sceneName = "Scene1";
    }

    public void goMediumLevelScene()
    {
        levelUI = "medium";
        sceneLoad.sceneName = "Scene2";
    }

    public void goHardLevelScene()
    {
        levelUI = "hard";
        sceneLoad.sceneName = "Scene3";
    }

    public void ReConnected()
    {


       /* if (!PhotonNetwork.ConnectUsingSettings())
        {
            PhotonNetwork.ConnectUsingSettings();

        }*/
    }
}
