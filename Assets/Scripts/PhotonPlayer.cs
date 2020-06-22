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
    public NetworkObjectHandler reptileCharacter;

    public Text timer;
    public float timeLeft = 300.0f;
    float gametime = 3f;

    public GameObject[] healthPlayer1;
    public GameObject[] healthPlayer2;
    GameObject playerObj1;
    GameObject playerObj2;
    float health_value1 = 0f;
    float health_value2 = 0f;
    public static float score_earn = 0f;

    public Text disinfectant_player1;
    public Text disinfectant_player2;
    public Text disinfectantPower_player1;
    public Text disinfectantPower_player2;
    public Text mask_player1;
    public Text mask_player2;
    public Text score;
    public Text playersInfo;
    public Text gameCountdown;

    public GameObject winObject;
    public GameObject loseObject;
    public AudioSource musicStart;
    public AudioSource warningSound;
    public AudioSource reptileSpawnSound;
    public AudioSource reptileSound;

    int spawnReptile = 0;
    int warningCount = 0;

    public static bool starting = true;

    public void Awake()
    {
        timer.text = "0.00";
    }

    // Start is called before the first frame update
    public void Start()
    {
        musicStart.Play();
    }


    // Update is called once per frame
    public void Update()
    {


        if (PhotonNetwork.CurrentRoom != null)
        {

            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {

                StartCoroutine("startGame");
            }
            if (!starting)
            {
                playerObj1 = GameObject.FindGameObjectWithTag("player1");
                playerObj2 = GameObject.FindGameObjectWithTag("player2");
                playersInfo.text = "";
                score.text = score_earn.ToString();

                if (playerObj1 != null)
                {

                    health_value1 = playerObj1.GetComponent<NetworkObject>().GetHealth();
                    mask_player1.text = playerObj1.GetComponent<playerController>().GetMask().ToString();
                    disinfectant_player1.text = playerObj1.GetComponent<playerController>().GetDisinfectant().ToString();
                    disinfectantPower_player1.text = playerObj1.GetComponent<playerController>().GetDisinfectantPower().ToString();

                    if (!System.Single.IsNaN(health_value1))
                    {
                        UpdatePlayerHealth(healthPlayer1, health_value1);
                    }
                }
                else
                {
                    health_value1 = 0f;
                    mask_player1.text = "0.00";
                    disinfectant_player1.text = "0.00";
                    disinfectantPower_player1.text = "0.00";
                    UpdatePlayerHealth(healthPlayer1, health_value1);
                }

                if (playerObj2 != null)
                {
                    health_value2 = playerObj2.GetComponent<NetworkObject>().gameObject.GetComponent<NetworkObject>().GetHealth();
                    mask_player2.text = playerObj2.GetComponent<NetworkObject>().gameObject.GetComponent<playerController>().GetMask();
                    disinfectant_player2.text = playerObj2.GetComponent<NetworkObject>().gameObject.GetComponent<playerController>().GetDisinfectant();
                    disinfectantPower_player2.text = playerObj2.GetComponent<playerController>().GetDisinfectantPower().ToString();

                    if (!System.Single.IsNaN(health_value2))
                    {
                        UpdatePlayerHealth(healthPlayer2, health_value2);
                    }
                }
                else
                {
                    health_value2 = 0f ;
                    mask_player2.text = "0.00";
                    disinfectant_player2.text = "0.00";
                    disinfectantPower_player2.text = "0.00";
                    UpdatePlayerHealth(healthPlayer2, health_value2);
                }

                timeLeft -= Time.deltaTime;
                string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
                string seconds = (timeLeft % 60).ToString("00");
                timer.text = minutes + ":" + seconds;

                if (timer.text == "01:05")
                {
                    if(warningCount==0)
                    {
                        StartCoroutine(reptileSoundEffect());
                        warningCount++;
                    }
                }

                    if (timer.text == "01:00")
                {
                        if (PhotonNetwork.IsMasterClient==true && spawnReptile==0)
                        {
                        reptileSpawnSound.Play();
                        spawnReptile = spawnReptile + 1;
                        reptileCharacter.SpawnNetworkObject();
                        reptileSound.Play();
                    }
                    
                }

                if (timeLeft <= 0.00f)
                {

                    //  timer.text = "UP !";
                    if (GameObject.FindGameObjectsWithTag("enemy").Length == 0 && (health_value1 > 0 && health_value2 > 0))
                    {
                        //win game
                        BadgeSystem.player1bonus = health_value1 * 5;
                        BadgeSystem.player2bonus = health_value2 * 5;
                        winObject.SetActive(true);
                    }
                    else
                    {
                        //lose game
                        BadgeSystem.player1bonus = health_value1 * 5;
                        BadgeSystem.player2bonus = health_value2 * 5;
                        loseObject.SetActive(true);
                    }
                }

                else if (timeLeft > 0.00f && health_value1 <= 0 && health_value2 <= 0)
                {
                    BadgeSystem.player1bonus = health_value1 * 5;
                    BadgeSystem.player2bonus = health_value2 * 5;
                    loseObject.SetActive(true);
                }

            }
            else
            {
                timer.text = "--:--";
            }
        }
        else
        {
            BadgeSystem.player1bonus = 0;
            BadgeSystem.player2bonus = 0;
            loseObject.SetActive(true);
        }



    }

    public IEnumerator startGame()
    {
        while (starting == true)
        {
            playersInfo.text = "Players Ready !";
            gameCountdown.text = "Game Start";
            yield return new WaitForSeconds(2f);
            playersInfo.text = "";
            gameCountdown.text = "";
            starting = false;
        }


    }

    public IEnumerator reptileSoundEffect()
    {
        warningSound.Play();
        yield return new WaitForSeconds(2f);
        
    }


    public void UpdatePlayerHealth(GameObject[] health, float health_value)
    {
        if (health_value > 0 && health_value <= 30)
        {
            health[0].SetActive(true);
            health[1].SetActive(false);
            health[2].SetActive(false);
        }
        else if (health_value > 30 && health_value <= 60)
        {
            health[0].SetActive(true);
            health[1].SetActive(true);
            health[2].SetActive(false);
        }
        else if (health_value > 60 && health_value <= 100)
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

    public void GotoBadgeScene()
    {
        SceneManager.LoadScene("Badge");
    }



}