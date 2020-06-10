using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTeamAlias : MonoBehaviourPun, IPunObservable
{
  //  public NetworkObjectHandler networkObjecthandler;
   // public float maxX = 10, maxY = 10, maxZ = 10;
   //public float minX = 1, minY = 1, minZ = 1;
    // Start is called before the first frame update
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
