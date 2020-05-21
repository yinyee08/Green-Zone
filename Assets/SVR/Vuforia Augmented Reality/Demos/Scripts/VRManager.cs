using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SVR_PHOTON_UNITY_NETWORKING_SDK
using Photon.Pun;
using Photon.Realtime;
#endif

namespace SVR
{
    public class VRManager : MonoBehaviour
    {

        private TurboFanScript turbofan;

        private void Start()
        {
#if SVR_PHOTON_UNITY_NETWORKING_SDK
            //GameObject go = PhotonNetwork.Instantiate("Turbo Fan", new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            GameObject go = GameObject.Find("Turbo Fan");
            turbofan = go.GetComponent<TurboFanScript>();
#endif
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RandomColor()
        {
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);

            turbofan.OnChangeRed(r);
            turbofan.OnChangeGreen(g);
            turbofan.OnChangeBlue(b);
        }
    }
}
