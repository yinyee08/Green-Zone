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
    GameObject[] _rooms;

    public void OnRoomListButtonClicked()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.PlayerCount == 2 || info.RemovedFromList || !info.IsOpen || !info.IsVisible || info.PlayerCount == 0)
            {
                _rooms = GameObject.FindGameObjectsWithTag("room");

                foreach (GameObject room in _rooms)
                {
                    if (room.GetComponent<RoomListing>().roomName == info.Name)
                    {
                        Destroy(room);
                    }
                }

            }
            else
            {
                RoomListing listing = Instantiate(_roomlisting, _content);

                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                }
            }
        }
    }


}
