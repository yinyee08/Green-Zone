using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GetLevelName : MonoBehaviourPun, IPunObservable
{
    public static string levelname;
    // Start is called before the first frame update
    public void Awake()
    {
        PhotonNetwork.SerializationRate = 5;
        PhotonNetwork.SendRate = 20;
        levelname = SceneUI.levelUI;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            stream.SendNext(SceneUI.levelUI);
        }
        else
        {
            levelname = (string)stream.ReceiveNext();
        }
    }
}
