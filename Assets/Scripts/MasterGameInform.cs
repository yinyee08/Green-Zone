using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SVR;

public class MasterGameInform : MonoBehaviourPun, IPunObservable
{

    public Text team;

    public void Awake()
    {
        PhotonNetwork.SerializationRate = 5;
        PhotonNetwork.SendRate = 20;
        team.text = LoginGame.teamName;   
    }

    public void Update()
    {
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
        if (stream.IsWriting)
        {
            stream.SendNext(LoginGame.teamName);
        }
        else
        {
            team.text = (string)stream.ReceiveNext();
        }
    }


    [PunRPC]
    public void UpdateText(string text)
    {
        text = "";
    }

}
