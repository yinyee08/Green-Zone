using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhoton : MonoBehaviourPun
{
    public static GameObject LocalPlayerInstance;
//   private PhotonView photonView;

    public void Awake()
    {

        if (photonView.IsMine)
        {
            LocalPlayerInstance = this.gameObject;
            if (PhotonNetwork.IsMasterClient == true)
            {
                this.gameObject.name = "Player1";
            }
            else
            {
                this.gameObject.name = "Player2";
            }
            
        }
        DontDestroyOnLoad(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
