using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if SVR_PHOTON_UNITY_NETWORKING_SDK
using Photon.Pun;
using Photon.Realtime;
#endif

namespace SVR
{
    public class SampleManager : 
#if SVR_PHOTON_UNITY_NETWORKING_SDK
        MonoBehaviourPunCallbacks
#else
        MonoBehaviour
#endif
    {

        public UnityEvent OnStart;
#if SVR_PHOTON_UNITY_NETWORKING_SDK
        // Start is called before the first frame update
        void Start()
        {
            OnStart.Invoke();
        }

        // Update is called once per frame
        void Update()
        {

        }
#endif
    }
}