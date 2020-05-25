using Photon.Pun;
using UnityEngine;
using System.Collections;
using SVR;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;

	//public Player playerPrefab;

	private Maze mazeInstance;

	//private Player playerInstance;

	private void Start () {
		StartCoroutine(BeginGame());
	}
    public NetworkObjectHandler networkObjecthandler1;
    public NetworkObjectHandler networkObjecthandler2;

    /*private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}*/

    private IEnumerator BeginGame () {
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
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
                PlayerPhoton.LocalPlayerInstance.transform.position = mazeInstance.GetCell(mazeInstance.RandomCoordinates).transform.position;
                Debug.Log("Master Avatar1");
            }
            else
            {
                networkObjecthandler2.SpawnNetworkObject();
                PlayerPhoton.LocalPlayerInstance.transform.position = mazeInstance.GetCell(mazeInstance.RandomCoordinates).transform.position;
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

	/*private void RestartGame () {
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
		StartCoroutine(BeginGame());
	}*/
}