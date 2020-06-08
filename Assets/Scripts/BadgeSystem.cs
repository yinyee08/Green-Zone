using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class BadgeSystem : MonoBehaviour
{

    public Text bronzeScore;
    public Text silverScore;
    public Text goldScore;

    public GameObject bronzeBadge;
    public GameObject silverBadge;
    public GameObject goldBadge;

    // Start is called before the first frame update
    void Start()
    {
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

        if (SceneUI.levelUI == "easy")
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
        else if(SceneUI.levelUI =="medium")
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
        else if(SceneUI.levelUI =="hard")
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
}
