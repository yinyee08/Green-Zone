using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SVR;

public class PhotonPlayer : MonoBehaviour
{
 //   private PhotonView PV;
    public NetworkObjectHandler networkObjecthandler1;
    public NetworkObjectHandler networkObjecthandler2;
    public GameObject zombie;

    // Start is called before the first frame update
    void Start()
    {
        //     PV = GetComponent<PhotonView>();
        //= Random.Range(0, GameSetup.GS.spawnPoints.Length);
        // if (PhotonNetwork.IsMasterClient == true)
       
        if (PlayerPhoton.LocalPlayerInstance == null)
        {
            if (PhotonNetwork.IsMasterClient == true)
            {
                networkObjecthandler1.SpawnNetworkObject();
                networkObjecthandler1.name = "Player1";
                Debug.Log("Master Avatar");
            }
            else
            {
                networkObjecthandler2.SpawnNetworkObject();
                networkObjecthandler1.name = "Player2";
                Debug.Log("Client Avatar");
            }


          //  Debug.Log(GameSetup.GS.spawnPoints[spawnPicker].position + "," + spawnPicker + "SpawnAvatar");
        }
        else
        {
            Debug.Log("Avatar Exist");
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
