using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SVR;
using TMPro;

public class PhotonPlayer : MonoBehaviour
{
    //   private PhotonView PV;
    //public NetworkObjectHandler networkObjecthandler1;
    //public NetworkObjectHandler networkObjecthandler2;

    public TextMeshProUGUI timer;
    float timeLeft = 120.0f;
    string countdownTime = "0.00";

    public GameObject[] healthPlayer1;
    public GameObject[] healthPlayer2;
    GameObject playerObj1;
    GameObject playerObj2;
    float health_value1;
    float health_value2;

    public Text disinfectant_player1;
    public Text disinfectant_player2;
    public Text mask_player1;
    public Text mask_player2;

    private PhotonView pv;

    public void Awake()
    {
        PhotonNetwork.SerializationRate = 5;
        PhotonNetwork.SendRate = 20;
        timer.text = "0.00";
    }

    // Start is called before the first frame update
    public void Start()
    {
        pv = GetComponent<PhotonView>();

    }

    // Update is called once per frame

    /*  public void Update()
      {
          pv.RPC("UpdateInfo",RpcTarget.All);

      }*/

    public void Update()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {

                playerObj1 = GameObject.FindGameObjectWithTag("player1");
                playerObj2 = GameObject.FindGameObjectWithTag("player2");

                if (playerObj1 != null && playerObj2 != null)
                {
                    health_value1 = playerObj1.GetComponent<NetworkObject>().GetHealth();
                    health_value2 = playerObj2.GetComponent<NetworkObject>().gameObject.GetComponent<NetworkObject>().GetHealth();
                    mask_player1.text = playerObj1.GetComponent<NetworkObject>().gameObject.GetComponent<playerController>().GetMask();
                    mask_player2.text = playerObj2.GetComponent<NetworkObject>().gameObject.GetComponent<playerController>().GetMask();
                    disinfectant_player1.text = playerObj1.GetComponent<NetworkObject>().gameObject.GetComponent<playerController>().GetDisinfectant();
                    disinfectant_player2.text = playerObj2.GetComponent<NetworkObject>().gameObject.GetComponent<playerController>().GetDisinfectant();


                    if (!System.Single.IsNaN(health_value1))
                    {
                        UpdatePlayerHealth(healthPlayer1, health_value1);
                    }

                    if (!System.Single.IsNaN(health_value2))
                    {
                        UpdatePlayerHealth(healthPlayer2, health_value2);
                    }

                    timeLeft -= Time.deltaTime;
                    string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
                    string seconds = (timeLeft % 60).ToString("00");

                    timer.text = minutes + ":" + seconds;
                    countdownTime = timer.text;
                    if (timeLeft < 0)
                    {
                        timer.text = "UP !";
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


}
