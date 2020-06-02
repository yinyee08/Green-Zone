using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using SVR;

public class Explosion : MonoBehaviour {

	public GameObject bomb;
	public float power = 10.0f;
	public float radius = 5.0f;
	public float upforce = 1.0f;
    public GameObject bigExplosionPrefab;
   // public NetworkObjectHandler bigExplosionPrefab;
    private PhotonView pv;

    void Start()
    {
        pv = GetComponent<PhotonView>();   
    }

    void FixedUpdate()
    {
        InvokeEXplosion();
    }

    void InvokeEXplosion()
    {
        pv.RPC("ExplodeStart", RpcTarget.All);
    }

    [PunRPC]
    void ExplodeStart() { 
		if(bomb == enabled) {
          Invoke("Detonate",5); //5 seconds
		}
	}

    
    void InvokeDetonate() { 
}

	void Detonate() {

        Instantiate(bigExplosionPrefab, transform.position, transform.rotation);
        //bigExplosionPrefab.SpawnNetworkObject();
		Vector3 explosionPosition = bomb.transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

		foreach(Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) {
				rb.AddExplosionForce(power, explosionPosition, radius, upforce, ForceMode.Impulse);
				if(rb.gameObject.tag == "DestructableObj") {
					rb.gameObject.GetComponent<DestructBox>().GetComponent<PhotonView>().RPC("explode", RpcTarget.All);

				}
			}
		}

		Destroy(gameObject);
		
	}

}
