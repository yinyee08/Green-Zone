using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginGame : MonoBehaviour
{
    public GameObject SignInPanel;
    public GameObject SignUpPanel;
    public GameObject LoginPanel;

    public GameObject LoggedIn;
    public TextMeshProUGUI signInTeam;
    public TextMeshProUGUI signUpTeam;
    public Text logTeam;

    public static string teamName;

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
        if (!string.IsNullOrEmpty(signInTeam.text))
        {
            teamName = signInTeam.text;
            logTeam.text = "Hi, " + signInTeam.text;
            showLoggedIn();
        }
    }

    public void setNameSignUp()
    {
        if (!string.IsNullOrEmpty(signUpTeam.text))
        {
            teamName = signUpTeam.text;
            logTeam.text = "Hi, "+ signUpTeam.text;
            showLoggedIn();
        }
    }
}
