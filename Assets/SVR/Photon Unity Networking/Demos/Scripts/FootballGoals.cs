using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SVR_PHOTON_UNITY_NETWORKING_SDK
using Photon.Pun;
using Photon.Realtime;
#endif

namespace SVR
{
    public class FootballGoals : MonoBehaviour
    {
#if SVR_PHOTON_UNITY_NETWORKING_SDK
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter(Collider other)
        {
            if(other.name == "Football")
            {
                Debug.Log("Gooal");
            }
        }
#endif
    }

}
