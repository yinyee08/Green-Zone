using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SVR;

public class MasterGameInform : MonoBehaviourPun, IPunObservable
{
  //  public NetworkObjectHandler networkObjecthandler;
   // public float maxX = 10, maxY = 10, maxZ = 10;
   //public float minX = 1, minY = 1, minZ = 1;
    // Start is called before the first frame update
    public Text team;

    public void Awake()
    {
        PhotonNetwork.SerializationRate = 5;
        PhotonNetwork.SendRate = 20;
        team.text = LoginGame.teamName;   
    }

    public void Update()
    {
     /*   if (Input.GetKeyDown(KeyCode.Return))
        {
            networkObjecthandler.SpawnNetworkObject();
            networkObjecthandler.pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

        }*/
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
    // Update is called once per frame
    /* void Update()
     {

        /* if (Input.GetKeyDown(KeyCode.Return))
         {
             networkObjecthandler.SpawnNetworkObject();
             networkObjecthandler.pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

         }*/
    //   }
}
