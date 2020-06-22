﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTeamAlias : MonoBehaviourPun, IPunObservable
{

    public static string team;
    public static string curlevel;

    public void Awake()
    {
        PhotonNetwork.SerializationRate = 5;
        PhotonNetwork.SendRate = 20;
        team = LoginGame.teamalias;
        curlevel = SceneUI.levelUI;
    }

    public void Update()
    {
       // Debug.Log("Team Alias : "+team);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
        if (stream.IsWriting)
        {
            stream.SendNext(LoginGame.teamalias);
            stream.SendNext(SceneUI.levelUI);
        }
        else
        {
            team = (string)stream.ReceiveNext();
            curlevel = (string)stream.ReceiveNext();
        }
    }

}
