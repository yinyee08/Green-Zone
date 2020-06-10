using Photon.Pun;
using UnityEngine;
using System.Collections;
using SVR;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Maze mazePrefab;

    //public Player playerPrefab;

    //private Maze mazeInstance;

    //private Player playerInstance;

    public float respawnAirDrop = 5.0f;
    private float spawnAirDropTimer;

    public float respawnZombie = 100.0f;
    private float spawnZombieTimer;

    public NetworkObjectHandler networkObjecthandler1;
    public NetworkObjectHandler networkObjecthandler2;
    public NetworkObjectHandler networkObjecthandler3;
    public NetworkObjectHandler zombieObject;

    public Transform[] zombieSpawnPoints;
    public int zombieNumber;
    public int spawnZombieNumber;

    public GameObject AirDropWeapons;
    private void Start()
    {
        //StartCoroutine(BeginGame());
        BeginGame();
    }

    private void Update()
    {
        if (!PhotonPlayer.starting)
        {
            if (spawnAirDropTimer < respawnAirDrop)
            {
                spawnAirDropTimer += Time.deltaTime;
            }
            else
            {
                SpawnAirDrop();
            }

            if (spawnZombieTimer < respawnZombie)
            {
                spawnZombieTimer += Time.deltaTime;
            }
            else
            {
                SpawnZombie();
            }
        }
    }


    private void BeginGame()
    {

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
                for (int i = 0; i < zombieNumber; i++)
                {
                    int randomPoint = Random.Range(0, 10);
                    zombieObject.pos = zombieSpawnPoints[randomPoint].position;
                    zombieObject.SpawnNetworkObject();
                }
                SpawnZombie();
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

    void SpawnAirDrop()
    {
        if (spawnAirDropTimer < respawnAirDrop) return;

        //networkObjecthandler3.SpawnNetworkObject();
        //BRS_AirDrop.AirDropInstance.transform.position = mazeInstance.GetCell(mazeInstance.RandomCoordinates).transform.position + new Vector3(0f,7f,0f);
        for (int i = 0; i < AirDropWeapons.transform.childCount; i++)
        {
            if (!AirDropWeapons.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                AirDropWeapons.transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }

        spawnAirDropTimer = 0f;
    }

    void SpawnZombie()
    {
        if (spawnZombieTimer < respawnZombie) return;

        for (int i = 0; i < spawnZombieNumber; i++)
        {
            int randomPoint = Random.Range(0, 10);
            zombieObject.pos = zombieSpawnPoints[randomPoint].position;
            zombieObject.SpawnNetworkObject();
        }
        spawnZombieTimer = 0f;
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