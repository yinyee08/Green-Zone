using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if(SceneUI.levelUI == "easy")
        {
            bronzeBadge.SetActive(true);
            bronzeScore.text = PhotonPlayer.score_earn.ToString();
        }
        else if(SceneUI.levelUI =="medium")
        {
            silverBadge.SetActive(true);
            silverScore.text = PhotonPlayer.score_earn.ToString(); 
        }
        else if(SceneUI.levelUI =="hard")
        {
            goldBadge.SetActive(true);
            goldScore.text = PhotonPlayer.score_earn.ToString(); 
        }
    }

    public void goLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
