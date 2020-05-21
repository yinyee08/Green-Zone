using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangeColor : MonoBehaviour
{
    public GameObject capsule;
    // Start is called before the first frame update
    public void GetName() 
    {
      //  Debug.Log("RoomName : "+RoomListing.roomNameText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomChange()
    {
      //  Debug.Log(RoomListing.roomNameText);
        capsule.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f,1f),Random.Range(0F,1f), Random.Range(0F, 1f));
    }
}
