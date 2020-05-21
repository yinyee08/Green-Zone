using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SVR
{
    public class ARObject : MonoBehaviour
    {

        public BasicObjectInteraction interactionType = BasicObjectInteraction.LeftMousePressed;
        public UnityEvent interactionEvent;

        [Tooltip("Color that will be applied, only if Apply Color event is set")]
        public Color changeColor = new Color(0, 255, 255, 255);

        private Material defaultMaterial;
        private Color defaultColor;

        [Tooltip("Particle System that will be played, only if Play Particle System event is set")]
        public ParticleSystem particle;

        public Axis rotationAxis = Axis.y;
        public float rotationMagnitude = 5f;

#if SVR_VUFORIA_AR_SDK
        private void Start()
        {
            defaultMaterial = GetComponent<MeshRenderer>().material;
            defaultColor = defaultMaterial.color;
        }

        private void Update()
        {
            switch (interactionType)
            {
                case BasicObjectInteraction.LeftMousePressed:
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            interactionEvent.Invoke();
                        }
                        break;
                    }
                case BasicObjectInteraction.RightMousePressed:
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            interactionEvent.Invoke();
                        }
                        break;
                    }
                case BasicObjectInteraction.LeftMousePressedHold:
                    {
                        if (Input.GetMouseButton(0))
                        {
                            interactionEvent.Invoke();
                        }
                        break;
                    }
                case BasicObjectInteraction.RightMousePressedHold:
                    {
                        if (Input.GetMouseButton(1))
                        {
                            interactionEvent.Invoke();
                        }
                        break;
                    }
                case BasicObjectInteraction.LeftMouseSelect:
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit))
                            {
                                if(hit.transform == this.transform)
                                {
                                    interactionEvent.Invoke();
                                }
                            }
                        }
                        break;
                    }
                case BasicObjectInteraction.RightMouseSelect:
                    {
                        if (Input.GetMouseButtonDown(1))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit))
                            {
                                if (hit.transform == this.transform)
                                {
                                    interactionEvent.Invoke();
                                }
                            }
                        }
                        break;
                    }
                case BasicObjectInteraction.LeftMouseSelectHold:
                    {
                        if (Input.GetMouseButton(0))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit))
                            {
                                if (hit.transform == this.transform)
                                {
                                    interactionEvent.Invoke();
                                }
                            }
                        }
                        break;
                    }
                case BasicObjectInteraction.RightMouseSelectHold:
                    {
                        if (Input.GetMouseButton(1))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit))
                            {
                                if (hit.transform == this.transform)
                                {
                                    interactionEvent.Invoke();
                                }
                            }
                        }
                        break;
                    }
                case BasicObjectInteraction.EnterKeyPressed:
                    {
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            interactionEvent.Invoke();
                        }
                        break;
                    }
                case BasicObjectInteraction.SpaceKeyPressed:
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            interactionEvent.Invoke();
                        }
                        break;
                    }
                case BasicObjectInteraction.TouchScreen:
                    {
                        if (Input.touchCount > 0)
                        {
                            Touch touch = Input.GetTouch(0);
                            if (touch.phase == TouchPhase.Began)
                            {
                                interactionEvent.Invoke();
                            }
                        }
                        break;
                    }
                case BasicObjectInteraction.TouchScreenHold:
                    {
                        if (Input.touchCount > 0)
                        {
                            Touch touch = Input.GetTouch(0);
                            if (touch.phase == TouchPhase.Stationary)
                            {
                                interactionEvent.Invoke();
                            }
                        }
                        break;
                    }
                case BasicObjectInteraction.TouchScreenSelect:
                    {
                        if (Input.touchCount > 0)
                        {
                            Touch touch = Input.GetTouch(0);
                            if (touch.phase == TouchPhase.Began)
                            {
                                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                                RaycastHit hit;

                                if (Physics.Raycast(ray, out hit))
                                {
                                    if (hit.transform == this.transform)
                                    {
                                        interactionEvent.Invoke();
                                    }
                                }
                            }
                        }
                        break;
                    }
                case BasicObjectInteraction.TouchScreenSelectHold:
                    {
                        if (Input.touchCount > 0)
                        {
                            Touch touch = Input.GetTouch(0);
                            if (touch.phase == TouchPhase.Stationary)
                            {
                                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                                RaycastHit hit;

                                if (Physics.Raycast(ray, out hit))
                                {
                                    if (hit.transform == this.transform)
                                    {
                                        interactionEvent.Invoke();
                                    }
                                }
                            }
                        }
                        break;
                    }
                case BasicObjectInteraction.TouchScreenMove:
                    {
                        if (Input.touchCount > 0)
                        {
                            Touch touch = Input.GetTouch(0);
                            if (touch.phase == TouchPhase.Moved)
                            {
                                interactionEvent.Invoke();
                            }
                        }
                        break;
                    }
            }
        }
#endif

        public void RandomColor()
        {
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);

            Color color = new Color(r, g, b);
            GetComponent<MeshRenderer>().material.color = color;
        }

        // WILL TOGGLE BETWEEN DEFAULT AND SET COLOR
        public void ApplyColor()
        {
            if (defaultMaterial.color != changeColor)
                defaultMaterial.color = changeColor;
            else
                defaultMaterial.color = defaultColor;
        }

        public void PlayAnimation()
        {
            if (!GetComponent<Animation>())
            {
                return;
            }

            Animation animation = GetComponent<Animation>();
            if(!animation.isPlaying)
                animation.Play();
        }

        public void PlayAudio()
        {
            if (!GetComponent<AudioSource>())
            {
                return;
            }

            AudioSource audio = GetComponent<AudioSource>();

            if (!audio.isPlaying && audio.clip)
                audio.Play();
        }

        public void PlayParticle()
        {
            if (particle)
                particle.Play();
        }

        public void ToggleHideShow()
        {
            bool toggle = true;
            if (GetComponent<MeshRenderer>().enabled)
            {
                toggle = false;
            }
            foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
            {
                mr.enabled = toggle;
            }
        }

        // ALONG Y-AXIS
        public void Rotate()
        {
            switch (rotationAxis)
            {
                case Axis.x:
                    {
                        transform.Rotate(new Vector3(rotationMagnitude, 0, 0));
                        break;
                    }
                case Axis.y:
                    {
                        transform.Rotate(new Vector3(0, rotationMagnitude, 0));
                        break;
                    }
                case Axis.z:
                    {
                        transform.Rotate(new Vector3(0, 0, rotationMagnitude));
                        break;
                    }
            }
        }

    }
}

