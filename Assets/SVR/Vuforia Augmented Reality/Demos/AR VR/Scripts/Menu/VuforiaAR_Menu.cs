using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;

namespace SVR
{
    public class VuforiaAR_Menu : EditorWindow
    {
#if SVR_VUFORIA_AR_SDK

        [MenuItem("SVR/Vuforia AR/Enable \uff06 Setup", false, 150)]
        private static void EnableVuforia()
        {
#if UNITY_2019_2_OR_NEWER

            if (!PlayerSettings.vuforiaEnabled)
            {
                PlayerSettings.vuforiaEnabled = true;
            }
            Debug.Log("Vuforia Augmented Reality support enabled");

#else

#if UNITY_ANDROID
            if (!PlayerSettings.GetPlatformVuforiaEnabled(BuildTargetGroup.Android))
            {
                PlayerSettings.SetPlatformVuforiaEnabled(BuildTargetGroup.Android, true);
            }
            Debug.Log("Vuforia Augmented Reality support enabled");
#endif

#if UNITY_IPHONE
            if (!PlayerSettings.GetPlatformVuforiaEnabled(BuildTargetGroup.iOS))
            {
                PlayerSettings.SetPlatformVuforiaEnabled(BuildTargetGroup.iOS, true);
            }
            Debug.Log("Vuforia Augmented Reality support enabled");
#endif


#if UNITY_STANDALONE
            if (!PlayerSettings.GetPlatformVuforiaEnabled(BuildTargetGroup.Standalone))
            {
                PlayerSettings.SetPlatformVuforiaEnabled(BuildTargetGroup.Standalone, true);
            }
            Debug.Log("Vuforia Augmented Reality support enabled");
#endif

#endif
            Selection.activeObject = Resources.Load("VuforiaConfiguration");

        }

        [MenuItem("SVR/Vuforia AR/AR Camera", false, 151)]
        private static void VuforiaARCamera()
        {
            EditorUtility.DisplayDialog("AR Camera", "Go to GameObject > Vuforia Engine > AR Camera to add AR Camera into the scene.\nRemove any other camera in the scene as well.", "OK");
        }

        [MenuItem("SVR/Vuforia AR/Image Marker", false, 152)]
        private static void VuforiaARImageMarker()
        {
            EditorUtility.DisplayDialog("Image Marker", "Go to GameObject > Vuforia Engine > Image to add Image Marker into the scene.\nUse imported or default database for tracking.", "OK");
        }

        [MenuItem("SVR/Vuforia AR/AR Object Interaction", false, 153)]
        private static void VuforiaARObject()
        {
            if (Selection.assetGUIDs.Length > 0)
            {
                EditorUtility.DisplayDialog("AR Object", "You must select a game object in the scene hierarchy to create interaction on it.", "OK");
                return;
            }

            GetWindow(typeof(ARObjectEditorWindow), false, "AR Object");
        }

        [MenuItem("SVR/Vuforia AR/Open Sample Scene/Simple", false, 160)]
        private static void VuforiaSampleSceneSimple()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/SVR/Vuforia Augmented Reality/Demos/Simple AR/Scene/Simple AR Sample.unity");

            Debug.Log("[SVR] Please check our documentation for setting up the sample scene");
        }

        [MenuItem("SVR/Vuforia AR/Open Sample Scene/AR VR/Lobby", false, 161)]
        private static void VuforiaSampleSceneLobby()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/SVR/Vuforia Augmented Reality/Demos/AR VR/Scene/Lobby.unity");
        }

        [MenuItem("SVR/Vuforia AR/Open Sample Scene/AR VR/AR", false, 162)]
        private static void VuforiaSampleSceneAR()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/SVR/Vuforia Augmented Reality/Demos/AR VR/Scene/AR.unity");
        }

        [MenuItem("SVR/Vuforia AR/Open Sample Scene/AR VR/VR", false, 163)]
        private static void VuforiaSampleSceneVR()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/SVR/Vuforia Augmented Reality/Demos/AR VR/Scene/VR.unity");
        }

        [MenuItem("SVR/Vuforia AR/Vuforia Documentation/Learn Vuforia Object", false, 200)]
        private static void VuforiaDocumentationObject()
        {
            Application.OpenURL("https://library.vuforia.com/articles/Training/getting-started-with-vuforia-in-unity.html#game-objects");
        }

        [MenuItem("SVR/Vuforia AR/Vuforia Documentation/Full Training", false, 201)]
        private static void VuforiaDocumentation()
        {
            Application.OpenURL("https://library.vuforia.com/articles/Training/getting-started-with-vuforia-in-unity.html");
        }
#endif
    }
}
#endif