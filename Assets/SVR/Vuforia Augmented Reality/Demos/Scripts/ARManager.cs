using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SVR_PHOTON_UNITY_NETWORKING_SDK
using Photon.Pun;
using Photon.Realtime;
#endif

namespace SVR
{
    public class ARManager : MonoBehaviour
    {
        private bool display = false;
        private TurboFanScript turbofan;

        private void Start()
        {
#if SVR_PHOTON_UNITY_NETWORKING_SDK

            GameObject go = PhotonNetwork.Instantiate("Turbo Fan", new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            go.transform.parent = GameObject.Find("ImageTarget").transform;
            turbofan = go.GetComponent<TurboFanScript>();
            turbofan.isAR = true;   
#endif
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Move the cube if the screen has the finger moving.
                if (touch.phase == TouchPhase.Moved)
                {
                    OnSyncPressed();
                }
            }
        }

        public void OnChangeRed(float value)
        {
            turbofan.OnChangeRed(value);
        }

        public void OnChangeGreen(float value)
        {
            turbofan.OnChangeGreen(value);
        }

        public void OnChangeBlue(float value)
        {
            turbofan.OnChangeBlue(value);
        }

        public void OnChangeMetallic(float value)
        {
            turbofan.OnChangeMetallic(value);
        }

        public void OnChangeSmoothness(float value)
        {
            turbofan.OnChangeSmoothness(value);
        }

        public void OnSyncPressed()
        {
            display = !display;
            turbofan.OnSyncDisplay(display);
        }

        public void OnAdjustAnimation(float value)
        {
            turbofan.OnPlayAnimation(value);
        }
    }
}
