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
      //  photonView = GetComponent<PhotonView>();
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instanciation when levels are synchronized
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
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
