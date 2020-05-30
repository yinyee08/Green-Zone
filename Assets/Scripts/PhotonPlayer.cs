using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SVR;
using UnityEngine.SceneManagement;

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
                    }
                    else
                    {
                        //lose game
                        loseObject.SetActive(true);
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
        SceneManager.LoadScene("Scene");
    }


}
