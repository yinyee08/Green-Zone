using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.Networking;

public class BadgeSystem : MonoBehaviour
{

    public Text bronzeScore;
    public Text silverScore;
    public Text goldScore;

    public GameObject bronzeBadge;
    public GameObject silverBadge;
    public GameObject goldBadge;

    public static float player1bonus=0f;
    public static float player2bonus=0f;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            Debug.Log("PLayer 1 : "+player1bonus +" Player 2 : " + player2bonus);

            if(player1bonus>0)
            {
                PhotonPlayer.score_earn = PhotonPlayer.score_earn + player1bonus;
            }

            if(player2bonus > 0)
            {
                PhotonPlayer.score_earn = PhotonPlayer.score_earn + player2bonus;
            }
           
            StartCoroutine(CheckScoreData(int.Parse(PhotonPlayer.score_earn.ToString())));
        }

        if (!PhotonNetwork.LeaveRoom())
        {
            PhotonNetwork.LeaveRoom();
            Debug.Log("LeftRoom ");

        }
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Connect ");
        }

        if (GetTeamAlias.curlevel == "easy")
        {
            bronzeBadge.SetActive(true);

            if (PhotonPlayer.score_earn < 50)
            {
                bronzeBadge.transform.GetChild(1).gameObject.SetActive(false);
                bronzeBadge.transform.GetChild(2).gameObject.SetActive(false);
                bronzeBadge.transform.GetChild(3).gameObject.SetActive(false);
                bronzeBadge.transform.GetChild(4).GetComponent<Text>().text = "Level Failed ! Take courage and try again";
            }
            bronzeScore.text = PhotonPlayer.score_earn.ToString();
        }
        else if (GetTeamAlias.curlevel == "medium")
        {
            silverBadge.SetActive(true);
            if (PhotonPlayer.score_earn < 100)
            {
                silverBadge.transform.GetChild(1).gameObject.SetActive(false);
                silverBadge.transform.GetChild(2).gameObject.SetActive(false);
                silverBadge.transform.GetChild(3).gameObject.SetActive(false);
                silverBadge.transform.GetChild(4).GetComponent<Text>().text = "Level Failed ! Take courage and try again";
            }
            silverScore.text = PhotonPlayer.score_earn.ToString();
        }
        else if (GetTeamAlias.curlevel == "hard")
        {

            goldBadge.SetActive(true);
            if (PhotonPlayer.score_earn < 100)
            {
                goldBadge.transform.GetChild(1).gameObject.SetActive(false);
                goldBadge.transform.GetChild(2).gameObject.SetActive(false);
                goldBadge.transform.GetChild(3).gameObject.SetActive(false);
                goldBadge.transform.GetChild(4).GetComponent<Text>().text = "Level Failed ! Take courage and try again";
            }
            goldScore.text = PhotonPlayer.score_earn.ToString();
        }
    }

    public void goLeaderboard()
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
                Debug.Log("Success score : " + score);
                Debug.Log(data);

            }
        }
    }

    IEnumerator CheckScoreData(int score)
    {

        string levelname = SceneUI.levelUI;
        string curLevelId = "";
        if (levelname == "easy")
        {
            curLevelId = "easy_score";
        }
        else if (levelname == "medium")
        {
            curLevelId = "medium_score";
        }
        else if (levelname == "hard")
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
