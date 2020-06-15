using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

[System.Serializable]
public class TeamPlayer
{
    public string message;
    public int status;
}

[System.Serializable]
public class Score
{
    public string metric_id;
    public string metric_name;
    public string metric_type;
    public int value;

}
[System.Serializable]
public class MessageData
{
    public string alias;
    public string id;
    public string first_name;
    public string last_name;
    public string created;
    public Score[] score;
}
[System.Serializable]
public class Data
{
    public MessageData message;
    public int status;
}


public class LoginGame : MonoBehaviour
{
    public GameObject SignInPanel;
    public GameObject SignUpPanel;
    public GameObject LoginPanel;

    public GameObject LoggedIn;
    public Text signInTeam;
    public Text signUpTeam;
    public Text logTeam;
    public Text placeholderSignIn;
    public Text placeholderSignUp;

    public static string teamName;
    public static string teamalias;

    void Start()
    {
        LoginPanel.SetActive(true);

    }

    public void showSignIn()
    {
        SignInPanel.SetActive(true);
        SignUpPanel.SetActive(false);
        LoginPanel.SetActive(false);

    }

    public void showSignUp()
    {
        SignUpPanel.SetActive(true);
        SignInPanel.SetActive(false);
        LoginPanel.SetActive(false);
    }

    public void showLoggedIn()
    {
        SignUpPanel.SetActive(false);
        SignInPanel.SetActive(false);
        LoginPanel.SetActive(false);
        LoggedIn.SetActive(true);
    }

    public void setNameSignIn()
    {
        if (string.IsNullOrEmpty(signInTeam.text))
        {
            placeholderSignIn.text = "Please Type Team Name !";
           
        }
        else
        {
            teamName = signInTeam.text;
            StartCoroutine(GetPlayerData(teamName));
            logTeam.text = "Hi, " + signInTeam.text;
            showLoggedIn();
        }
    }

    public void setNameSignUp()
    {
        if (string.IsNullOrEmpty(signUpTeam.text))
        {
            placeholderSignUp.text = "Please Type Team Name !";
        }
        else
        {
            teamName = signUpTeam.text;
            StartCoroutine(GetPlayerData(teamName));
            logTeam.text = "Hi, " + signUpTeam.text;
            showLoggedIn();
        }
    }

    IEnumerator SignUp(string team_alias, string team_name)
    {
        string team_id = team_name.Substring(0, 1) + "01";

        using (UnityWebRequest www = UnityWebRequest.Get("http://api.tenenet.net/createPlayer?token=7bfe7669d12cb3f64681d71d4bb54a22&alias=" + team_alias + "&id=" + team_id + "&fname=" + team_name + "&lname=" + team_name))
        {
            yield return www.SendWebRequest();
            var data = www.downloadHandler.text;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                TeamPlayer result = JsonUtility.FromJson<TeamPlayer>(data);
                Debug.Log(data);
                if (result.status == 1)
                {
                    teamalias = team_alias;
                    Debug.Log("Player created!");
                }
                else
                {
                    Debug.Log("Player Fail created!");
                }




            }
        }
    }

    IEnumerator GetPlayerData(string team_alias)
    {

        string team_name = team_alias;
        team_alias = team_alias + "001";

        using (UnityWebRequest www = UnityWebRequest.Get("http://api.tenenet.net/getPlayer?token=7bfe7669d12cb3f64681d71d4bb54a22&alias=" + team_alias))
        {
            yield return www.SendWebRequest();
            var data = www.downloadHandler.text;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

                Data players = JsonUtility.FromJson<Data>(data);

                if (players.status == 0)
                {

                    StartCoroutine(SignUp(team_alias, team_name));
                    Debug.Log("Prepare to create user");
                }
                else
                {
                    teamalias = team_alias;
                    Debug.Log("Player Existed");

                }

            }
        }
    }

}

/*
 {"message":
    {"first_name":"supercell\u200b",
     "last_name":"supercell\u200b",
     "alias":"supercell\u200b001",
     "id":"s01",
     "created":"2020-06-01 07:55:06",
     "score":[
        {"metric_id":"easy_score","metric_name":"easy_score","metric_type":"point","value":"0"},
        {"metric_id":"medium_score","metric_name":"medium_score","metric_type":"point","value":"0"},
        {"metric_id":"hard_score","metric_name":"hard_score","metric_type":"point","value":"0"},
        {"metric_id":"bonus_score","metric_name":"bonus_score","metric_type":"point","value":"0"}
        ]},
      "status":"1"}
 */
