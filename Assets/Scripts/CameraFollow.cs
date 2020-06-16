using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject playerObj1;
    GameObject playerObj2;

    public float smoothSpeed = 0.125f;
    public Vector3 cameraOffSet;

    public Space offsetPositionSpace = Space.World;
    public bool lookAt = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (PhotonNetwork.IsMasterClient == true && GameManager.clientname=="player1")
        {
            playerObj1 = GameObject.FindGameObjectWithTag("player1");
        }
        else
        {
            playerObj1 = GameObject.FindGameObjectWithTag("player2");
        }

        if (playerObj1)
        {
            // compute position
            if (offsetPositionSpace == Space.Self)
            {

                Vector3 desiredPosition = playerObj1.transform.TransformPoint(cameraOffSet);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
                transform.rotation = playerObj1.transform.rotation;
            }
            else
            {
                //  transform.position = playerObj1.transform.position + cameraOffSet;
                Vector3 desiredPosition = playerObj1.transform.position + cameraOffSet;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
                transform.rotation = playerObj1.transform.rotation;
            }

            // compute rotation
            if (lookAt)
            {
                transform.LookAt(playerObj1.transform);
            }
            else
            {
                transform.rotation = playerObj1.transform.rotation;
            }
        }
        else
        {
            return;
        }
    }

}
