using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class MultiplayerVRMenu : MonoBehaviour
{
#if UNITY_EDITOR
#if SVR_PHOTON_UNITY_NETWORKING_SDK
    [MenuItem("SVR/Multiplayer VR/VR Player")]
    public static void MultiplayerVRPlayer()
    {
        if (GameObject.FindObjectOfType<Camera>())
        {
            DestroyImmediate(GameObject.FindObjectOfType<Camera>().gameObject);
        }

        GameObject go = Instantiate(Resources.Load<GameObject>("SVR Multiplayer Spawner")) as GameObject;
        go.name = "SVR Multiplayer Spawner";
    }

    [MenuItem("SVR/Multiplayer VR/Sample Scene/Lobby")]
    public static void MultiplayerVRLobbyScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/SVR/Multiplayer VR/Scenes/MVR Lobby Scene.unity");
    }

    [MenuItem("SVR/Multiplayer VR/Sample Scene/Game")]
    public static void MultiplayerVRGameScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/SVR/Multiplayer VR/Scenes/MVR Game Scene.unity");
    }
#endif
#endif
}
