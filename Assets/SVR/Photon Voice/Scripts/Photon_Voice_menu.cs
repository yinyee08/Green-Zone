using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

#if SVR_PHOTON_VOICE_SDK
using Photon.Voice.Unity;
#endif

namespace SVR
{
    public class PhotonVoice_Menu : EditorWindow
    {
#if SVR_PHOTON_VOICE_SDK
        [MenuItem("SVR/Photon Voice/Setup Settings", false, 150)]
        private static void VoiceSetup()
        {
            Selection.activeObject = Resources.Load("PhotonServerSettings");
            EditorUtility.DisplayDialog("Setup Photon Unity Networking", "Make sure you setup the APP ID for Photon Unity Networking", "OK");
        }

        [MenuItem("SVR/Photon Voice/Voice Connection", false, 151)]
        private static void VoiceConnection()
        {
            if (Selection.assetGUIDs.Length > 0)
            {
                EditorUtility.DisplayDialog("Voice Connection", "You must select a game object in the scene hierarchy to be Voice Connection.", "OK");
                return;
            }

            GameObject target = Selection.activeGameObject;

            try
            {
                if (!target.GetComponent<VoiceConnection>())
                {
                    target.AddComponent<VoiceConnection>();
                }

                target.name = "Voice Connection";
            }
            catch (System.NullReferenceException)
            {
                EditorUtility.DisplayDialog("Voice Connection", "You must select a game object in the scene hierarchy to be Voice Connection.", "OK");
                return;
            }
        }

        [MenuItem("SVR/Photon Voice/Voice Recorder", false, 152)]
        private static void VoiceRecorder()
        {
            if (Selection.assetGUIDs.Length > 0)
            {
                EditorUtility.DisplayDialog("Voice Recorder", "You must select a game object in the scene hierarchy to be Voice Recorder.", "OK");
                return;
            }

            GameObject target = Selection.activeGameObject;

            try
            {
                if (!target.GetComponent<Recorder>())
                {
                    target.AddComponent<Recorder>();
                }

                target.name = "Voice Recorder";
            }
            catch (System.NullReferenceException)
            {
                EditorUtility.DisplayDialog("Voice Recorder", "You must select a game object in the scene hierarchy to be Voice Recorder.", "OK");
                return;
            }
        }

        [MenuItem("SVR/Photon Voice/Voice Speaker", false, 153)]
        private static void VoiceSpeaker()
        {
            if (Selection.assetGUIDs.Length > 0)
            {
                EditorUtility.DisplayDialog("Voice Speaker", "You must select a game object in the scene hierarchy to be Voice Speaker.", "OK");
                return;
            }

            GameObject target = Selection.activeGameObject;

            try
            {
                if (!target.GetComponent<Speaker>())
                {
                    target.AddComponent<Speaker>();
                }

                target.name = "Voice Speaker";

                string localPath = "Assets/SVR/Photon Voice/Demos/" + target.name + ".prefab";

                // Make sure the file name is unique, in case an existing Prefab has the same name.
                localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

                // Create the new Prefab.
                PrefabUtility.SaveAsPrefabAssetAndConnect(target, localPath, InteractionMode.UserAction);

                FindObjectOfType<VoiceConnection>().SpeakerPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject));

                DestroyImmediate(target);

                Selection.activeObject = (GameObject)AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject));
            }
            catch (System.NullReferenceException)
            {
                EditorUtility.DisplayDialog("Voice Speaker", "You must select a game object in the scene hierarchy to be Voice Speaker.", "OK");
                return;
            }
        }
#endif
    }

}
#endif