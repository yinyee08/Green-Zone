using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRS_AirDrop : MonoBehaviour
{
	public static GameObject AirDropInstance;

	public GameObject GroundDetection;
	public GameObject Canopy;
	public GameObject Parcel;
	public GameObject disinfectantPowerUp;
	//public GameObject respawnEffect;
	public GameObject PointLight;
	public GameObject Smoke;
	private Rigidbody AirDropRB;
	private bool Landed = false;

	public void Awake()
    {
		AirDropInstance = this.gameObject;
		DontDestroyOnLoad(this.gameObject);
	}
	// Use this for initialization
	void Start ()
	{
		AirDropRB = transform.GetComponent<Rigidbody> ();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit objectHit;

		if (Physics.Raycast (transform.position, Vector3.down, out objectHit, 1))
		{
			if (objectHit.collider.gameObject.tag == "ground")
			{
				Landed = true;
			}
		}

		if (Landed)
		{
			DropHasLanded ();
			Landed = false;
		}
		if(disinfectantPowerUp.GetComponent<Disinfectant>().checkDisinfectCollision()){
			Destroy(gameObject);
		}
	}

	void DropHasLanded()
	{
		//DropLight.gameObject.SetActive (true);
		//Smoke.gameObject.SetActive (true);
		//AirDropRB.drag = 0;
		//AirDropRB.mass = 5000;
		//Destroy (GroundDetection);
		Destroy (Canopy);
		
	}

	void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.tag == "player1" || other.gameObject.tag == "player2") {
			PointLight.SetActive(true);
			Smoke.SetActive(true);
            Invoke("ShowPowerUp", 4);			
        }
       
    }

	void ShowPowerUp() {
		Destroy(PointLight);
		Destroy(Smoke);
		Destroy(Parcel);
		Destroy(GroundDetection);
		AirDropRB.useGravity = false;
		disinfectantPowerUp.SetActive(true);
		//respawnEffect.SetActive(true);
	}
}
