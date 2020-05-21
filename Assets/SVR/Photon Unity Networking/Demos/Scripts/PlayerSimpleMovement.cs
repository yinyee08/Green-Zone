using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if SVR_PHOTON_UNITY_NETWORKING_SDK
using Photon.Pun;
using Photon.Realtime;
#endif


namespace SVR
{
    public class PlayerSimpleMovement : MonoBehaviour
    {

        public float speed = 5f;

        private Text nameText;

#if SVR_PHOTON_UNITY_NETWORKING_SDK
        private PhotonView photonView;

        private void Start()
        {
            photonView = GetComponent<PhotonView>();

            if (photonView.IsMine)
            {
                photonView.RPC("SyncPlayerName", RpcTarget.All, photonView.ViewID, PhotonNetwork.NickName);
            }

            CameraFollow cameraFollow = this.gameObject.GetComponent<CameraFollow>();

            if (cameraFollow != null)
            {
                if (photonView.IsMine)
                {
                    cameraFollow.OnStartFollowing();
                }
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (!photonView.IsMine)
                return;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed * -1);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                //transform.Translate(transform.right * Time.deltaTime * 2);
                transform.Rotate(Vector3.up * Time.deltaTime * speed * 10);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //transform.Translate(transform.right * Time.deltaTime * 2 * -1);
                transform.Rotate(Vector3.up * Time.deltaTime * speed * 10 * -1);
            }
        }

        [PunRPC]
        private void SyncPlayerName(int ID, string name)
        {
            PhotonView pv = PhotonView.Find(ID);
            Transform parent = pv.transform;
            while (parent.parent)
            {
                parent = parent.parent;
            }

            nameText = parent.GetComponentInChildren<Text>();
            nameText.text = name;
        }

#endif
    }
}
