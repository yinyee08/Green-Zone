using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using SVR;

public class DestructBox : MonoBehaviour
{

    public float cubeSize = 0.1f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    public Texture wood_texture;
    public GameObject destructPieces;
    private PhotonView pview;

    private bool hasWeapon = false;

    // Use this for initialization
    void Start()
    {
        pview = GetComponent<PhotonView>();
        //calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector)
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

    }
   // [PunRPC]
    public void explode()
    {
        //make object disappear
        //gameObject.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        if (gameObject.transform.childCount > 0)
        {
            Debug.Log("Box has weapon");
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            hasWeapon = true;
        }
        else
        {
            Debug.Log("No weapon in box");
        }

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    // pview.RPC("createPiece", RpcTarget.All, x, y, z);
                    createPiece(x, y, z);
                }
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }

        Invoke("DestroyPieces", 2);

    }
   // [PunRPC]
    void createPiece(int x, int y, int z)
    {
        //create piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;

        Renderer mRenderer = piece.GetComponent<Renderer>();
        mRenderer.material.SetTexture("_MainTex", wood_texture);


        piece.GetComponent<Transform>().SetParent(destructPieces.transform);

    }

    void DestroyPieces()
    {

        foreach (Transform child in destructPieces.transform)
        {
            Destroy(child.gameObject);
        }

        if (!hasWeapon) Destroy(gameObject);
    }

    void Update()
    {

        if (hasWeapon)
        {

            if (gameObject.transform.GetChild(0).gameObject.name == "s_mask" && gameObject.transform.GetChild(0).gameObject.GetComponent<Mask>().checkMaskCollision())
            {
                //Add script mask +1
                Destroy(gameObject);
                hasWeapon = false;

            }
            else if (gameObject.transform.GetChild(0).gameObject.name == "s_sanitizer" && gameObject.transform.GetChild(0).gameObject.GetComponent<Disinfectant>().checkDisinfectCollision())
            {
                //Add script disinfectant +1
                Destroy(gameObject);
                hasWeapon = false;
            }

        }

    }


}