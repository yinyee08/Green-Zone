using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SVR_PHOTON_UNITY_NETWORKING_SDK
using Photon.Pun;
using Photon.Realtime;
#endif

namespace SVR
{
    public class TurboFanScript : MonoBehaviour
    {
        Material mat;

#if SVR_PHOTON_UNITY_NETWORKING_SDK
        PhotonView photonView;
#endif

        public bool isAR = false;

        private void Awake()
        {
#if SVR_PHOTON_UNITY_NETWORKING_SDK
            photonView = GetComponent<PhotonView>();
            mat = GetComponentInChildren<MeshRenderer>().sharedMaterial;
            if (isAR)
            {
                Transform[] allChildren = GetComponentsInChildren<Transform>();
                foreach (Transform child in allChildren)
                {
                    if (child.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        child.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }
#endif
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnSyncDisplay(bool on)
        {
#if SVR_PHOTON_UNITY_NETWORKING_SDK
            photonView.RPC("SyncDisplay", RpcTarget.OthersBuffered, on);
#endif
        }

#if SVR_PHOTON_UNITY_NETWORKING_SDK
        [PunRPC]
        private void SyncDisplay(bool on)
        {
            if (!isAR)
            {
                Transform[] allChildren = GetComponentsInChildren<Transform>();
                foreach (Transform child in allChildren)
                {
                    if (child.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        child.gameObject.GetComponent<MeshRenderer>().enabled = on;
                    }
                }
            }
        }
#endif

        public void OnChangeRed(float value)
        {
#if SVR_PHOTON_UNITY_NETWORKING_SDK
            photonView.RPC("OnSynchronize", RpcTarget.AllBuffered, value, mat.color.g, mat.color.b, mat.GetFloat("_Metallic"), mat.GetFloat("_Glossiness"));
#endif
        }

        public void OnChangeGreen(float value)
        {
#if SVR_PHOTON_UNITY_NETWORKING_SDK
            photonView.RPC("OnSynchronize", RpcTarget.AllBuffered, mat.color.r, value, mat.color.b, mat.GetFloat("_Metallic"), mat.GetFloat("_Glossiness"));
#endif
        }

        public void OnChangeBlue(float value)
        {
#if SVR_PHOTON_UNITY_NETWORKING_SDK
            photonView.RPC("OnSynchronize", RpcTarget.AllBuffered, mat.color.r, mat.color.g, value, mat.GetFloat("_Metallic"), mat.GetFloat("_Glossiness"));
#endif
        }

        public void OnChangeMetallic(float value)
        {
#if SVR_PHOTON_UNITY_NETWORKING_SDK
            photonView.RPC("OnSynchronize", RpcTarget.AllBuffered, mat.color.r, mat.color.g, mat.color.b, value, mat.GetFloat("_Glossiness"));
#endif
        }

        public void OnChangeSmoothness(float value)
        {
#if SVR_PHOTON_UNITY_NETWORKING_SDK
            photonView.RPC("OnSynchronize", RpcTarget.AllBuffered, mat.color.r, mat.color.g, mat.color.b, mat.GetFloat("_Metallic"), value);
#endif
        }

#if SVR_PHOTON_UNITY_NETWORKING_SDK
        [PunRPC]
        private void OnSynchronize(float red, float green, float blue, float metallic, float smoothness)
        {
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.gameObject.GetComponent<MeshRenderer>() != null)
                {
                    mat = child.gameObject.GetComponent<MeshRenderer>().material;
                    mat.color = new Color(red, green, blue);
                    mat.SetFloat("_Metallic", metallic);
                    mat.SetFloat("_Glossiness", smoothness);
                }
            }
        }
#endif

        public void OnPlayAnimation(float value)
        {
#if SVR_PHOTON_UNITY_NETWORKING_SDK
            photonView.RPC("SyncAnimation", RpcTarget.AllBuffered, value);
#endif
        }

#if SVR_PHOTON_UNITY_NETWORKING_SDK
        [PunRPC]
        private void SyncAnimation(float value)
        {
            //float timer = (1 / GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length) / 60;
            GetComponent<Animator>().Play("TurboFan_Assemble", 0, value);
        }
#endif
    }

}