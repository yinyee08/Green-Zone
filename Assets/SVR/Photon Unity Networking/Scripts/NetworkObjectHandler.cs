using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SVR_PHOTON_UNITY_NETWORKING_SDK
using Photon.Pun;
using Photon.Realtime;
#endif

namespace SVR
{
    public class NetworkObjectHandler :
#if SVR_PHOTON_UNITY_NETWORKING_SDK
        MonoBehaviourPunCallbacks
#else
        MonoBehaviour
#endif
    {

        //public UnityEvent spawnEvent;

        public NetworkObject networkObject;

        [Tooltip("Tick to use transform, untick to use raw position and rotation")]
        public bool useTransform = true;

        public Transform trans;

        public Vector3 pos;
        public Vector3 euler;
        public Quaternion rot;

        public Transform parent;

        public void SpawnNetworkObject()
        {
#if SVR_PHOTON_UNITY_NETWORKING_SDK
            if (useTransform)
            {
                GameObject go = PhotonNetwork.Instantiate(networkObject.name, trans.position, trans.rotation) as GameObject;
                go.transform.parent = parent;
            }
            else
            {
                GameObject go = PhotonNetwork.Instantiate(networkObject.name, pos, rot) as GameObject;
                go.transform.parent = parent;
            }
#endif
        }

        // ONLY FOR DEBUG, NOT GOING TO WORK FOR PHOTON NETWORK
        public void SpawnNetworkObjectDebug()
        {
            if (useTransform)
            {
                Vector3 scale = networkObject.gameObject.transform.localScale;
                if (parent)
                {
                    Vector3 parentScale = parent.localScale;
                    scale.x /= parentScale.x;
                    scale.y /= parentScale.y;
                    scale.z /= parentScale.z;

                    //scale *= parentScale.y;
                }

                GameObject go = Instantiate(networkObject.gameObject, trans.position, trans.rotation, parent) as GameObject;
                go.transform.localScale = scale;
            }
            else
            {
                GameObject go = Instantiate(networkObject.gameObject, pos, rot, parent) as GameObject;
            }
        }
    }
}
