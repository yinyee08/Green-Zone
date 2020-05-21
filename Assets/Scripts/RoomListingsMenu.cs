using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    public RoomListing _roomlisting;

    public void OnRoomListButtonClicked()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
            Debug.Log("Yes Lobby");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("hi2");
        foreach (RoomInfo info in roomList)
        {
            RoomListing listing = Instantiate(_roomlisting, _content);

            if (listing != null)
            {
                listing.SetRoomInfo(info);
            }
        }
    }


        /*    public void OnRoomList()
        {
            Debug.Log("hi2");

                RoomListing listing = Instantiate(_roomlisting, _content);
                listing.SetRoomInfo(roomText.text);

        }*/



    }
