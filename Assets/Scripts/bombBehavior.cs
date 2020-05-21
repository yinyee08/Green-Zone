using UnityEngine;
using System.Collections;

public class bombBehavior : MonoBehaviour {

    public GameObject instantExplodeAnimation;
    public GameObject explosionObject;
    Collider coll;
    private bool blockedUp = false;
    private bool blockedDown = false;
    private bool blockedLeft = false;
    private bool blockedRight = false;

    private int originalRange = 0;
    private string bombType = "normal";

    private bool move = false;
    private string direction = "";

    public bool bombMoving = false;

    Quaternion horizRotation;
    Quaternion vertRotation;
    
    // Use this for initialization
    void Start () {
        horizRotation.eulerAngles = new Vector3(0, 90, 0);
        vertRotation.eulerAngles = new Vector3(0, 0, 0);

        coll = GetComponent<Collider>();
        StartCoroutine(explode());

        if (this.tag == "player1bomb")
        {
            playerController player1Script = GameObject.FindGameObjectWithTag("player1").GetComponent<playerController>();
            originalRange = player1Script.bombRange;
            bombType = player1Script.bombType;
        }

        if (this.tag == "player2bomb")
        {
            playerController player2Script = GameObject.FindGameObjectWithTag("player2").GetComponent<playerController>();
            originalRange = player2Script.bombRange;
            bombType = player2Script.bombType;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (move == true)
        {
            float speed = 0.15f;
            switch (direction)
            {
                case "up":
                    if (noWallandBreakableandPlayer(new Vector3(transform.position.x, 0.5f, transform.position.z + speed)))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
                        bombMoving = true;
                        break;
                    }
                    else
                    {
                        move = false;
                        bombMoving = false;
                    }
                    break;

                case "down":
                    if (noWallandBreakableandPlayer(new Vector3(transform.position.x, 0.5f, transform.position.z - speed)))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
                        bombMoving = true;
                        break;
                    }
                    else
                    {
                        move = false;
                        bombMoving = false;
                    }
                    break;

                case "left":
                    if (noWallandBreakableandPlayer(new Vector3(transform.position.x - speed, 0.5f, transform.position.z)))
                    {
                        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
                        bombMoving = true;
                        break;
                    }
                    else
                    {
                        move = false;
                        bombMoving = false;
                    }
                    break;

                case "right":
                    if (noWallandBreakableandPlayer(new Vector3(transform.position.x + speed, 0.5f, transform.position.z)))
                    {
                        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
                        bombMoving = true;
                        break;
                    }
                    else
                    {
                        move = false;
                        bombMoving = false;
                    }
                    break;

                default:
                    break;
            }            
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (move == true)
        {
            move = false;
            var roundedPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
            transform.position = roundedPosition;
        }
    }

    void OnTriggerExit()
    {
        if (noWallandBreakableandPlayer(transform.position))
        {
            (coll as BoxCollider).size = new Vector3(1.5f, 1.5f, 1.5f);
            (coll as BoxCollider).center = new Vector3(0f, 0.75f, 0f);
            coll.isTrigger = false;
        }
    }

    IEnumerator explode()
    {
        yield return new WaitForSeconds(2.8f);
        instantExplode();
    }

    public void instantExplode()
    {
        var roundedPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
        transform.position = roundedPosition;
                
        Destroy(this.gameObject);

        Instantiate(instantExplodeAnimation, transform.position, transform.rotation);

        if (this.tag == "player1bomb")
        {
            if (GameObject.FindGameObjectWithTag("player1"))      
                GameObject.FindGameObjectWithTag("player1").GetComponent<playerController>().bombPlanted -=1;
            blast(originalRange);
        }

        if (this.tag == "player2bomb")
        {
            if (GameObject.FindGameObjectWithTag("player2"))
                GameObject.FindGameObjectWithTag("player2").GetComponent<playerController>().bombPlanted -= 1;
            blast(originalRange);
        }
    }

    void blast(int range)
    {
        var x = transform.position.x;
        var y = 0.5f;
        var z = transform.position.z;

        Instantiate(explosionObject, new Vector3(x, y, z), horizRotation);
        Instantiate(explosionObject, new Vector3(x, y, z), vertRotation);

        for (int i = 1; i <= range; i++)
            plantExplosions(i, x, y, z, bombType);
        
        blockedUp = false;
        blockedDown = false;
        blockedLeft = false;
        blockedRight = false;
    }

    /// <summary>
    /// Plants an explosion at the selected range (automatically checks for wall)
    /// 
    /// TRY IMPLEMENTING SETS OR MAPS HERE, so the code wont lag up
    /// </summary>
    /// <param name="range"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    private void plantExplosions(float range, float x, float y, float z, string type)
    {
        if (bombType == "normal")
        {
            if (blockedLeft == false)
            {
                if (noWall(new Vector3(x - range, y, z)))
                {
                    if (checkIfExistOn(new Vector3(x - range, y, z), "breakable"))
                        blockedLeft = true;
                    Instantiate(explosionObject, new Vector3(x - range, y, z), horizRotation);
                }
                else blockedLeft = true;
            }

            if (blockedRight == false)
            {
                if (noWall(new Vector3(x + range, y, z)))
                {
                    if (checkIfExistOn(new Vector3(x + range, y, z), "breakable"))
                        blockedRight = true;
                    Instantiate(explosionObject, new Vector3(x + range, y, z), horizRotation);
                }
                else blockedRight = true;
            }

            if (blockedDown == false)
            {
                if (noWall(new Vector3(x, y, z - range)))
                {
                    if (checkIfExistOn(new Vector3(x, y, z - range), "breakable"))
                        blockedDown = true;
                    Instantiate(explosionObject, new Vector3(x, y, z - range), vertRotation);
                }
                else blockedDown = true;
            }

            if (blockedUp == false)
            {
                if (noWall(new Vector3(x, y, z + range)))
                {
                    if (checkIfExistOn(new Vector3(x, y, z + range), "breakable"))
                        blockedUp = true;
                    Instantiate(explosionObject, new Vector3(x, y, z + range), vertRotation);
                }
                else blockedUp = true;
            }
        }
        else if (bombType == "spike")
        {
            if (blockedLeft == false)
            {
                if (noWall(new Vector3(x - range, y, z)))
                    Instantiate(explosionObject, new Vector3(x - range, y, z), horizRotation);
                else blockedLeft = true;
            }

            if (blockedRight == false)
            {
                if (noWall(new Vector3(x + range, y, z)))
                    Instantiate(explosionObject, new Vector3(x + range, y, z), horizRotation);
                else blockedRight = true;
            }
            
            if (blockedDown == false)
            {
                if (noWall(new Vector3(x, y, z - range)))
                    Instantiate(explosionObject, new Vector3(x, y, z - range), vertRotation);
                else blockedDown = true;
            }

            if (blockedUp == false)
            {
                if (noWall(new Vector3(x, y, z + range)))
                    Instantiate(explosionObject, new Vector3(x, y, z + range), vertRotation);
                else blockedUp = true;
            }
        }
    }

    private bool noWall(Vector3 location)
    {
        Collider[] objects = Physics.OverlapSphere(location, 0.1f);
        foreach (Collider coll in objects)
        {
            if (coll.gameObject.CompareTag("wall"))
            {
                return false;
            }
        }
        return true;
    }

    private bool noWallandBreakableandPlayer(Vector3 location)
    {
        Collider[] objects = Physics.OverlapSphere(location, 0.1f);
        foreach (Collider coll in objects)
        {
            if (coll.gameObject.CompareTag("wall") || coll.gameObject.CompareTag("breakable") || coll.gameObject.CompareTag("player1") || coll.gameObject.CompareTag("player2"))
            {
                return false;
            }
        }
        return true;
    }

    private bool checkIfExistOn(Vector3 location, string data)
    {
        Collider[] objects = Physics.OverlapSphere(location, 0.2f);
        foreach (Collider coll in objects)
        {
            if (coll.gameObject.CompareTag(data))
            {
                return true;
            }
        }
        return false;
    }

    public void Move(string Direction)
    {
        if (bombMoving == false)
        {
            move = true;
            direction = Direction;
        }
    }

    public void StopMoving()
    {
        move = false;
        bombMoving = false;
        direction = "";
    }
}
