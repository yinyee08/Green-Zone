using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SVR;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PhotonPlayer : MonoBehaviour
{
    //   private PhotonView PV;
    public NetworkObjectHandler zombie;

    public Text timer;
    float timeLeft = 180.0f;
    string countdownTime = "0.00";

    public GameObject[] healthPlayer1;
    public GameObject[] healthPlayer2;
    GameObject playerObj1;
    GameObject playerObj2;
    float health_value1;
    float health_value2;
    public static float score_earn = 0f;

    public Text disinfectant_player1;
    public Text disinfectant_player2;
    public Text mask_player1;
    public Text mask_player2;
    public Text score;

    public GameObject winObject;
    public GameObject loseObject;

    public void Awake()
    {
        PhotonNetwork.SerializationRate = 5;
        PhotonNetwork.SendRate = 20;
        timer.text = "0.00";
    }

    // Start is called before the first frame update
    public void Start()
    {

    }


    // Update is called once per frame
    public void Update()
    {


        if (PhotonNetwork.CurrentRoom != null)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {

                playerObj1 = GameObject.FindGameObjectWithTag("player1");
                playerObj2 = GameObject.FindGameObjectWithTag("player2");
                score.text = score_earn.ToString();

                if (playerObj1 != null)
                {

                    health_value1 = playerObj1.GetComponent<NetworkObject>().GetHealth();
                    mask_player1.text = playerObj1.GetComponent<playerController>().GetMask().ToString();
                    disinfectant_player1.text = playerObj1.GetComponent<playerController>().GetDisinfectant().ToString();


                    if (!System.Single.IsNaN(health_value1))
                    {
                        UpdatePlayerHealth(healthPlayer1, health_value1);
                    }
                }

                if (playerObj2 != null)
                {
                    health_value2 = playerObj2.GetComponent<NetworkObject>().gameObject.GetComponent<NetworkObject>().GetHealth();
                    mask_player2.text = playerObj2.GetComponent<NetworkObject>().gameObject.GetComponent<playerController>().GetMask();
                    disinfectant_player2.text = playerObj2.GetComponent<NetworkObject>().gameObject.GetComponent<playerController>().GetDisinfectant();

                    if (!System.Single.IsNaN(health_value2))
                    {
                        UpdatePlayerHealth(healthPlayer2, health_value2);
                    }
                }


                timeLeft -= Time.deltaTime;
                string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
                string seconds = (timeLeft % 60).ToString("00");

                timer.text = minutes + ":" + seconds;
                countdownTime = timer.text;
                if (timeLeft <= 0.00f)
                {

                    //  timer.text = "UP !";
                    if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
                    {
                        //win game
                        winObject.SetActive(true);
                        StartCoroutine(CheckScoreData(int.Parse(score.text)));
                    }
                    else
                    {
                        //lose game
                        loseObject.SetActive(true);
                        StartCoroutine(CheckScoreData(int.Parse(score.text)));
                    }
                }


            }
            else
            {
                timer.text = "--:--";
                countdownTime = timer.text;
            }
        }

    }

    public void UpdatePlayerHealth(GameObject[] health, float health_value)
    {
        if (health_value == 1)
        {
            health[0].SetActive(true);
            health[1].SetActive(false);
            health[2].SetActive(false);
        }
        else if (health_value == 2)
        {
            health[0].SetActive(true);
            health[1].SetActive(true);
            health[2].SetActive(false);
        }
        else if (health_value == 3)
        {
            health[0].SetActive(true);
            health[1].SetActive(true);
            health[2].SetActive(true);
        }
        else
        {
            health[0].SetActive(false);
            health[1].SetActive(false);
            health[2].SetActive(false);
        }
    }

    public void GotoLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    IEnumerator RemoveScore(int score, string levelId, int playerhighscore)
    {
        int highScore = playerhighscore;
        score = score * (-1);
        using (UnityWebRequest www = UnityWebRequest.Get("http://api.tenenet.net/insertPlayerActivity?token=7bfe7669d12cb3f64681d71d4bb54a22&alias=" + GetTeamAlias.team + "&id=" + levelId + "&operator=remove&value=" + score))
        {
            yield return www.SendWebRequest();
            // var data = www.downloadHandler.text;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Success Remove Score");
                StartCoroutine(SetScore(highScore, levelId));


            }
        }
    }

    IEnumerator SetScore(int score, string levelId)
    {

        using (UnityWebRequest www = UnityWebRequest.Get("http://api.tenenet.net/insertPlayerActivity?token=7bfe7669d12cb3f64681d71d4bb54a22&alias=" + GetTeamAlias.team + "&id=" + levelId + "&operator=add&value=" + score))
        {
            yield return www.SendWebRequest();
            var data = www.downloadHandler.text;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Success Set Score");
                Debug.Log(data);

            }
        }
    }

    IEnumerator CheckScoreData(int score)
    {

        int level = 1;
        string curLevelId = "";
        if (level == 1)
        {
            curLevelId = "easy_score";
        }
        else if (level == 2)
        {
            curLevelId = "medium_score";
        }
        else if (level == 3)
        {
            curLevelId = "hard_score";
        }

        using (UnityWebRequest www = UnityWebRequest.Get("http://api.tenenet.net/getPlayer?token=7bfe7669d12cb3f64681d71d4bb54a22&alias=" + GetTeamAlias.team))
        {
            yield return www.SendWebRequest();
            var data = www.downloadHandler.text;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Data player = JsonUtility.FromJson<Data>(data);
                for (int i = 0; i < player.message.score.Length; i++)
                {
                    //  Debug.Log(j+ " ID : "+player.message.score[j].metric_id);
                    if (player.message.score[i].metric_id == curLevelId)
                    {
                        if (player.message.score[i].value < score)
                        {
                            Debug.Log("SetScore");
                            StartCoroutine(RemoveScore(player.message.score[i].value, curLevelId, score));
                        }
                        else
                        {
                            Debug.Log("NotSetScore");
                        }
                    }
                }
            }

        }
    }

}
