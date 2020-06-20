using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    //public Text ntext;

    //[SerializeField]
    // public static string roomNameText;

    public Text _text;
    public Button JoinRoomButton;

    public string roomName;

    public void Start()
    {
        JoinRoomButton.onClick.AddListener(() =>
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
            }

            PhotonNetwork.JoinRoom(roomName);
            Debug.Log("JoinRoom2 : " + roomName);
        });
    }

    public void SetRoomInfo(RoomInfo roominfo)
    {
        // teamname = LoginGame.teamName;
        roomName = roominfo.Name;
        _text.text = roominfo.Name + ", " + roominfo.PlayerCount + " Player Available";
    }

}
