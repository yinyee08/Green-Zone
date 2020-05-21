using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

namespace SVR
{
#if UNITY_EDITOR
    public class ARObjectEditorWindow : EditorWindow
    {
        static bool randomColor = false;
        static bool setColor = false;

        static bool playAnimation = false;
        static bool playAudio = false;
        static bool playParticle = false;
        static bool toggleHideShow = false;

        static bool rotate = false;
        static Axis rotationAxis = Axis.y;
        static float rotateMagnitude = 5f;

        static BasicObjectInteraction aRObjectInteraction;
        static ParticleSystem particle;
        static Color color = new Color(255, 255, 0, 255);

        static Animation animation;
        static AudioClip audioClip;

        bool handleRepaintErrors = false;
        public void OnGUI()
        {
            if (Event.current.type == EventType.Repaint && !handleRepaintErrors)
            {
                handleRepaintErrors = true;
                return;
            }

            GameObject target = Selection.activeGameObject;

            //EditorStyles.label.wordWrap = true;
            GUILayout.Label("Setup Interaction type and events for AR Object", EditorStyles.boldLabel);

            aRObjectInteraction = (BasicObjectInteraction)EditorGUILayout.EnumPopup("Interaction type", aRObjectInteraction);
            if (aRObjectInteraction == BasicObjectInteraction.LeftMousePressed)
            {
                EditorGUILayout.HelpBox("Event triggered by left mouse button", MessageType.Info);
            }
            else if (aRObjectInteraction == BasicObjectInteraction.RightMousePressed)
            {
                EditorGUILayout.HelpBox("Event triggered by right mouse button", MessageType.Info);
            } 
            else if (aRObjectInteraction == BasicObjectInteraction.EnterKeyPressed)
            {
                EditorGUILayout.HelpBox("Event triggered by Enter/Return key pressed", MessageType.Info);
            }
            else if (aRObjectInteraction == BasicObjectInteraction.SpaceKeyPressed)
            {
                EditorGUILayout.HelpBox("Event triggered by Space key pressed", MessageType.Info);
            }
            else if (aRObjectInteraction == BasicObjectInteraction.TouchScreen)
            {
                EditorGUILayout.HelpBox("Event triggered by Touch screen input", MessageType.Info);
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Create AR Object"))
            {
                if (!target.GetComponent<ARObject>())
                {
                    target.AddComponent<ARObject>();
                }

                ARObject obj = target.GetComponent<ARObject>();
                obj.interactionType = aRObjectInteraction;
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Events link", EditorStyles.boldLabel);

            randomColor = EditorGUILayout.Toggle("Random Color", randomColor);
            setColor = EditorGUILayout.Toggle("Set Color", setColor);

            if (setColor)
            {
                color = EditorGUILayout.ColorField("Color to set", color);
            }

            EditorGUILayout.HelpBox("Set color will override random color if both are used", MessageType.Info);

            rotate = EditorGUILayout.Toggle("Rotate", rotate);
            if (rotate)
            {
                rotationAxis = (Axis)EditorGUILayout.EnumPopup("Rotation Axis", rotationAxis);
                rotateMagnitude = EditorGUILayout.FloatField("Rotation Magnitude", rotateMagnitude);
                EditorGUILayout.Space();
            }

            playAnimation = EditorGUILayout.Toggle("Play Animation", playAnimation);
            if (playAnimation)
            {
                animation = EditorGUILayout.ObjectField(animation, typeof(Animation), false) as Animation;
                EditorGUILayout.HelpBox("An animation component will be added if absence", MessageType.Info);
            }

            playAudio = EditorGUILayout.Toggle("Play Audio", playAudio);
            if (playAudio)
            {
                audioClip = EditorGUILayout.ObjectField(audioClip, typeof(AudioClip), false) as AudioClip;
                EditorGUILayout.HelpBox("An audio source component will be added if absence", MessageType.Info);
            }

            playParticle = EditorGUILayout.Toggle("Play Particle System", playParticle);
            if (playParticle)
            {
                particle = (ParticleSystem)EditorGUILayout.ObjectField(particle, typeof(ParticleSystem), false);
            }

            toggleHideShow = EditorGUILayout.Toggle("Toggle Hide and Show", toggleHideShow);

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("You may also add other events afterward", MessageType.Info);

            if (GUILayout.Button("Link Event"))
            {
                if (EditorUtility.DisplayDialog("AR Object", "Make " + target.name + " as an AR Object?", "OK", "Cancel"))
                {
                    ARObject aRObject = target.GetComponent<ARObject>();
                    try
                    {
                        aRObject.interactionType = aRObjectInteraction;

                        for (int i = 0; i < aRObject.interactionEvent.GetPersistentEventCount(); i++)
                        {
                            UnityEventTools.RemovePersistentListener(aRObject.interactionEvent, i);
                        }

                        if (rotate)
                        {
                            aRObject.rotationAxis = rotationAxis;
                            UnityEventTools.AddPersistentListener(aRObject.interactionEvent, aRObject.Rotate);
                        }

                        if (randomColor)
                        {
                            UnityEventTools.AddPersistentListener(aRObject.interactionEvent, aRObject.RandomColor);
                        }

                        if (setColor)
                        {
                            aRObject.changeColor = color;
                            UnityEventTools.AddPersistentListener(aRObject.interactionEvent, aRObject.ApplyColor);
                        }

                        if (playAnimation)
                        {
                            UnityEventTools.AddPersistentListener(aRObject.interactionEvent, aRObject.PlayAnimation);
                            if (!target.GetComponent<Animation>())
                            {
                                target.AddComponent<Animation>();
                            }

                            if (animation)
                            {
                                target.GetComponent<Animation>().clip = animation.clip;
                            }
                        }

                        if (playAudio)
                        {
                            UnityEventTools.AddPersistentListener(aRObject.interactionEvent, aRObject.PlayAudio);
                            if (!target.GetComponent<AudioSource>())
                            {
                                target.AddComponent<AudioSource>();
                            }

                            if (audioClip)
                            {
                                target.GetComponent<AudioSource>().clip = audioClip;
                            }
                        }

                        if (playParticle)
                        {
                            aRObject.particle = particle;
                            UnityEventTools.AddPersistentListener(aRObject.interactionEvent, aRObject.PlayParticle);
                        }

                        if (toggleHideShow)
                        {
                            UnityEventTools.AddPersistentListener(aRObject.interactionEvent, aRObject.ToggleHideShow);
                        }

                        EditorUtility.DisplayDialog("AR interactable object", "Make sure that the object is under the hierarchy of AR Marker.", "OK");
                    }
                    catch (System.NullReferenceException)
                    {
                        EditorUtility.DisplayDialog("AR interactable object", "Failed to make AR interactable object.", "OK");
                        return;
                    }

                    Close();
                }
            }
        }
    }
#endif
}
