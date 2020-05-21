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
    [RequireComponent(typeof(Image))]
    public class ProgressBar : MonoBehaviour
    {
#if SVR_PHOTON_UNITY_NETWORKING_SDK

        public NetworkManager manager;
        public NetworkRoom room;

        private Image targetImage;

        private float fill = 0;

        // Start is called before the first frame update
        void Start()
        {
            if(!manager)
                manager = FindObjectOfType<NetworkManager>();

            if(!room)
                room = FindObjectOfType<NetworkRoom>();

            targetImage = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!manager.isConnectedToServer())
            {
                if (fill < 0.33f)
                {
                    fill += Time.deltaTime / 2;
                    targetImage.fillAmount = fill;
                }
            }
            else
            {
                if (!room.isConnectedToRoom())
                {
                    if (fill < 0.75f)
                    {
                        fill += Time.deltaTime / 2;
                        targetImage.fillAmount = fill;
                    }
                }
                else
                {
                    if (fill < 1f)
                    {
                        fill += Time.deltaTime / 2;
                        targetImage.fillAmount = fill;
                    }
                }
            }
        }
#endif
    }
}
