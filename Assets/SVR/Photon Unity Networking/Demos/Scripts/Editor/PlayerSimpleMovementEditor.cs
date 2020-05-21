using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace SVR
{
    [CustomEditor(typeof(PlayerSimpleMovement))]
    public class PlayerSimpleMovementEditor : Editor
    {
        SerializedProperty speed;

        public void OnEnable()
        {
            speed = serializedObject.FindProperty("speed");
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((PlayerSimpleMovement)target), typeof(PlayerSimpleMovement), false);
            GUI.enabled = true;

            EditorGUILayout.HelpBox("Use arrow key to move this player object", MessageType.Info);

            EditorGUILayout.PropertyField(speed);

            serializedObject.ApplyModifiedProperties();

        }
    }
}

#endif
