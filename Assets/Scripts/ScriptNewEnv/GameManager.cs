using Photon.Pun;
using UnityEngine;
using System.Collections;
using SVR;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;
	
	//public Player playerPrefab;

	//private Maze mazeInstance;
	
	//private Player playerInstance;

	public float respawnAirDrop = 5.0f;
	private float spawnAirDropTimer;

	
    public NetworkObjectHandler networkObjecthandler1;
    public NetworkObjectHandler networkObjecthandler2;
    public NetworkObjectHandler networkObjecthandler3;
    
	private void Start () {
		//StartCoroutine(BeginGame());
		BeginGame ();
	}
	 
    private void Update () {
		if(spawnAirDropTimer < respawnAirDrop) {
			spawnAirDropTimer += Time.deltaTime;
		}
		else {
			SpawnAirDrop();
		}
	}


    private void BeginGame () {
		
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
		//mazeInstance = Instantiate(mazePrefab) as Maze;
		//StartCoroutine(mazeInstance.Generate());
        
		/*playerInstance = Instantiate(playerPrefab) as Player;
		//playerInstance.TeleportToCell(
		//	mazeInstance.GetCell(mazeInstance.RandomCoordinates));
		playerInstance.TeleportToCell(
			mazeInstance.GetCell(mazeInstance.CenterCoordinates));*/
        if (PlayerPhoton.LocalPlayerInstance == null)
        {
            if (PhotonNetwork.IsMasterClient == true)
            {
				
				networkObjecthandler1.SpawnNetworkObject();
                //PlayerPhoton.LocalPlayerInstance.transform.position = mazePrefab.GetCell(mazePrefab.RandomCoordinates).transform.position;
                Debug.Log("Master Avatar1");
            }
            else
            {
                networkObjecthandler2.SpawnNetworkObject();
				//PlayerPhoton.LocalPlayerInstance.transform.position = mazePrefab.GetCell(mazePrefab.RandomCoordinates).transform.position;
                Debug.Log("Client Avatar1");
            }

        }
        else
        {
            Debug.Log("Avatar Exist");
        }
        Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
		Color ambientColor = new Color();
		ambientColor.r = 0.5f;
		ambientColor.g = 0.5f;
		ambientColor.b = 0.5f;
		RenderSettings.ambientLight = ambientColor;

	}

	void SpawnAirDrop() {
		if(spawnAirDropTimer < respawnAirDrop) return;
		
		//networkObjecthandler3.SpawnNetworkObject();
        //BRS_AirDrop.AirDropInstance.transform.position = mazeInstance.GetCell(mazeInstance.RandomCoordinates).transform.position + new Vector3(0f,7f,0f);

		spawnAirDropTimer = 0f;
	}

	/*private void RestartGame () {
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
		StartCoroutine(BeginGame());
	}*/
}