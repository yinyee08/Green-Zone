using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using SVR;

public class Disinfectant : MonoBehaviour {

    private bool collideWithPlayer = false;

    void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.tag == "player1" || other.gameObject.tag == "player2") {
            collideWithPlayer = true;
            Debug.Log("player collides disinfectant");
        }
       
    }

    public bool checkMaskCollision() {
        return collideWithPlayer;
    }
}
