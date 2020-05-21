using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SVR_PHOTON_UNITY_NETWORKING_SDK
using Photon.Pun;
using Photon.Realtime;
#endif


namespace SVR
{
    public class Sphere : MonoBehaviour
    {
#if SVR_PHOTON_UNITY_NETWORKING_SDK

        public float speed = 2f;

        private PhotonView photonView;

        private void Start()
        {
            photonView = GetComponent<PhotonView>();
        }

        public void OnMouseEnter()
        {
            if (photonView.IsMine)
            {
                float r = Random.Range(0.0f, 1.0f);
                float g = Random.Range(0.0f, 1.0f);
                float b = Random.Range(0.0f, 1.0f);
                Color newColor = new Color(r, g, b);
                GetComponent<MeshRenderer>().material.color = newColor;
                GetComponent<NetworkObject>().UpdateColor(r, g, b);
            }
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(Vector3.up * Time.deltaTime * speed);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(Vector3.down * Time.deltaTime * speed);
                }
            }
        }

#endif
    }
}

