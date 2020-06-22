using UnityEngine;
using System.Collections;
using Photon.Pun;
using SVR;

public class playerController : MonoBehaviour
{

    Animator anim;
    public GameObject normalBomb;
    public GameObject spikeBomb;

    public int bombCount = 1;
    public int bombPlanted = 0;
    public int bombRange = 1;
    public string bombType = "normal";
    public bool kickBomb = false;
    public float moveSpeed = 0.01f;
    float offsetForOverlapSphere = 0.3f;

    public string currentFacingDirection = "up";

    private string player;
    private string playerName;

    bool moveup = true;
    bool movedown = true;
    bool moveleft = true;
    bool moveright = true;

    private PhotonView pv;
    public NetworkObjectHandler networkBomb;
    public NetworkObjectHandler networkSpray;
    public NetworkObjectHandler networkHighSpray;
    private int health = 3;
    public string disinfectant;
    public string disinfectantPower;
    public string mask;
    float timeMask = 0f;
    float timeDisinfectant = 0f;
    float timeDisinfectantPower = 0f;

    GameObject[] door;
    public AudioSource bombSound;
    public AudioSource spraySound;
    public AudioSource attackSound;
    public AudioSource collectSound;

    private float speed = 2f;
    private Rigidbody rb;
    public float jumpForce = 3.5f;
    private bool playerIsOnGrounded = true;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        player = this.tag;
        playerName = this.name;
        pv = this.GetComponent<PhotonView>();
        mask = "0.00";
        disinfectant = "0.00";
        disinfectantPower = "0.00";
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        if (pv.IsMine == true && this.gameObject.GetComponent<NetworkObject>().GetHealth()>0)
        {
            rotatin();
            movin();
            plantbom();
            Spray();
            jumping();

            pv.RPC("timerMask", RpcTarget.All);
            pv.RPC("timerDisinfectant", RpcTarget.All);
            pv.RPC("timerDisinfectantPower", RpcTarget.All);

        }
    }

    void plantbom()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            plant();
        }
    }

    void plant()
    {
        var roundedPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));

        if (noBomb(roundedPosition))
        {

            networkBomb.SpawnNetworkObject();
            bombSound.Play();
        }
    }

    void Spray()
    {
        if (this.gameObject.transform.Find("Hands/sanitizer").gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                networkSpray.SpawnNetworkObject();
                spraySound.Play();
            }
        }

        if (this.gameObject.transform.Find("Hands/sanitizerPowerUp").gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                networkHighSpray.SpawnNetworkObject();
                spraySound.Play();
            }
        }
    }

    private bool noBomb(Vector3 location)
    {
        Collider[] objects = Physics.OverlapSphere(location, 0.1f);
        foreach (Collider coll in objects)
            if (coll.gameObject.CompareTag("bomb"))
                return false;
        return true;
    }

    void rotatin()
    {
        var rotationVector = transform.rotation.eulerAngles;

        if (player == "player1")
        {
            if (Input.GetAxis("Vertical") < 0) { rotationVector.y = 180; }
            if (Input.GetAxis("Vertical") > 0) { rotationVector.y = 0; }
            if (Input.GetAxis("Horizontal") < 0) { rotationVector.y = 270; }
            if (Input.GetAxis("Horizontal") > 0) { rotationVector.y = 90; }
        }
        else if (player == "player2")
        {
            if (Input.GetAxis("Vertical") < 0) { rotationVector.y = 180; }
            if (Input.GetAxis("Vertical") > 0) { rotationVector.y = 0; }
            if (Input.GetAxis("Horizontal") < 0) { rotationVector.y = 270; }
            if (Input.GetAxis("Horizontal") > 0) { rotationVector.y = 90; }
        }
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    private bool checkIfExistOn(Vector3 location, string data)
    {
        Collider[] objects = Physics.OverlapSphere(location, 0.2f);

        if (data == "all")
        {
            foreach (Collider coll in objects)
            {
                if (coll.gameObject.CompareTag("wall") || coll.gameObject.CompareTag("DestructableObj") || coll.gameObject.CompareTag("tree") || coll.gameObject.CompareTag("stairs"))
                {
                    return true;
                }

            }
        }
        else
        {
            foreach (Collider coll in objects)
            {
                if (coll.gameObject.CompareTag(data))
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool idleCoroutineStarted = false;

    /// <summary>
    /// Automatically moves the player sideways to provide smooth transition on blocks
    /// </summary>
    /// <param name="type"></param>
    void autoMoveSideways(string type, float inputMoveSpeed)
    {
        if (type == "moveVertical")
        {
            bool up = false;
            bool down = false;

            if (checkIfExistOn(new Vector3(transform.position.x, transform.position.y, transform.position.z + offsetForOverlapSphere), "all") == false)
                up = true;
            if (checkIfExistOn(new Vector3(transform.position.x, transform.position.y, transform.position.z - offsetForOverlapSphere), "all") == false)
                down = true;

            if (up == true)
            {
                transform.Translate(inputMoveSpeed * -moveSpeed, 0, 0);
            }
        }

    }

    void movin()
    {
        float moveVert = 0;
        float moveHoriz = 0;

        if (player == "player1")
        {
            moveVert = Input.GetAxis("Vertical");
            moveHoriz = Input.GetAxis("Horizontal");
        }
        else if (player == "player2")
        {
            moveVert = Input.GetAxis("Vertical");
            moveHoriz = Input.GetAxis("Horizontal");
        }

        var roundedPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));

        bool tempskip = false;

        if (transform.rotation.eulerAngles.y < 181 && transform.rotation.eulerAngles.y > 179 || transform.rotation.eulerAngles.y == 0)
        {
            if (Mathf.Abs(moveVert) > 0)
            {
                anim.SetBool("allowIdle", false);
                StopCoroutine(allowIdleState());
                idleCoroutineStarted = false;
            }

            if (moveVert > 0 && moveup) // up
            {
                if (checkIfExistOn(new Vector3(transform.position.x, transform.position.y, transform.position.z + offsetForOverlapSphere), "all") == false)
                {
                    transform.Translate(0, 0, moveVert * moveSpeed);
                }
                else
                {
                    anim.SetFloat("speed", 0);
                    tempskip = true;
                }
                movedown = true; moveleft = true; moveright = true;
            }
            if (moveVert < 0 && movedown) // down
            {
                if (checkIfExistOn(new Vector3(transform.position.x, transform.position.y, transform.position.z - offsetForOverlapSphere), "all") == false)
                {
                    transform.Translate(0, 0, -moveVert * moveSpeed);
                }
                else
                {
                    anim.SetFloat("speed", 0);
                    tempskip = true;
                }
                moveup = true; moveleft = true; moveright = true;
            }

            if (tempskip == false)
                anim.SetFloat("speed", Mathf.Abs(moveVert));
        }

        if (transform.rotation.eulerAngles.y == 90 || transform.rotation.eulerAngles.y == 270)
        {
            if (Mathf.Abs(moveHoriz) > 0)
            {
                anim.SetBool("allowIdle", false);
                StopCoroutine(allowIdleState());
                idleCoroutineStarted = false;
            }

            if (moveHoriz > 0 && moveright)
            {

                if (checkIfExistOn(new Vector3(transform.position.x + offsetForOverlapSphere, transform.position.y, transform.position.z), "all") == false)
                {
                    transform.Translate(0, 0, moveHoriz * moveSpeed);
                }
                else
                {
                    anim.SetFloat("speed", 0);
                    tempskip = true;
                }
                movedown = true; moveleft = true; moveup = true;
            }
            if (moveHoriz < 0 && moveleft)
            {
                if (checkIfExistOn(new Vector3(transform.position.x - offsetForOverlapSphere, transform.position.y, transform.position.z), "all") == false)
                {
                    transform.Translate(0, 0, -moveHoriz * moveSpeed);
                }
                else
                {
                    anim.SetFloat("speed", 0);
                    tempskip = true;
                }
                movedown = true; moveup = true; moveright = true;
            }
            if (tempskip == false)
                anim.SetFloat("speed", Mathf.Abs(moveHoriz));
        }

        if (moveVert == 0 && moveHoriz == 0)
        {
            if (idleCoroutineStarted == false)
            {
                StartCoroutine(allowIdleState());
                idleCoroutineStarted = true;
            }

        }
    }

    IEnumerator allowIdleState()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("allowIdle", true);
        idleCoroutineStarted = false;
        StopCoroutine(allowIdleState());
    }

    void OnCollisionEnter(Collision other)
    {

        if (kickBomb == true)
        {
            if (other.gameObject.CompareTag("player1bomb") || other.gameObject.CompareTag("player2bomb"))
            {
                anim.SetBool("kick", true);

                switch (currentFacingDirection)
                {
                    case "up":
                        moveup = false;
                        break;

                    case "down":
                        movedown = false;
                        break;

                    case "left":
                        moveleft = false;
                        break;

                    case "right":
                        moveright = false;
                        break;
                }

                StartCoroutine(stopKick(other));
            }
        }

        if (other.gameObject.CompareTag("player1") || other.gameObject.CompareTag("player2"))
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), transform.GetComponent<Collider>());
        }

        if (other.gameObject.CompareTag("enemy"))
        {
            if (!this.gameObject.transform.Find("Head/mask").gameObject.activeSelf)
            {
                health -= 1;
                attackSound.Play();
                gameObject.GetComponent<NetworkObject>().SetHealth(health);
            }


        }

        if (other.gameObject.tag == "disinfectant")
        {
            timeDisinfectant += 30f;
            collectSound.Play();
            other.gameObject.SetActive(false);
            this.gameObject.transform.Find("Hands/sanitizer").gameObject.SetActive(true);

        }

        if (other.gameObject.tag == "disinfectantPower")
        {
            timeDisinfectantPower += 30f;
            collectSound.Play();
            other.gameObject.SetActive(false);
            this.gameObject.transform.Find("Hands/sanitizerPowerUp").gameObject.SetActive(true);

        }

        if (other.gameObject.tag == "mask")
        {
            timeMask += 30f;
            collectSound.Play();
            other.gameObject.SetActive(false);
            this.gameObject.transform.Find("Head/mask").gameObject.SetActive(true);
        }

        playerIsOnGrounded = true;

    }

    IEnumerator stopKick(Collision other)
    {
        string dir = currentFacingDirection;
        yield return new WaitForSeconds(0.1f);
        other.gameObject.GetComponent<bombBehavior>().StopMoving();
        other.gameObject.GetComponent<bombBehavior>().Move(dir);
        anim.SetBool("kick", false);
        moveup = true;
        movedown = true;
        moveleft = true;
        moveright = true;
        StopCoroutine(stopKick(other));
    }

    public string GetMask()
    {
        return this.mask;
    }

    public string GetDisinfectant()
    {
        return this.disinfectant;
    }

    public string GetDisinfectantPower()
    {
        return this.disinfectantPower;
    }

    [PunRPC]
    public void timerMask()
    {
        if (this.gameObject.transform.Find("Head/mask").gameObject.activeSelf)
        {

            timeMask -= Time.deltaTime;
            string minutes = Mathf.Floor(timeMask / 60).ToString("0");
            string seconds = (timeMask % 60).ToString("00");

            this.mask = minutes + ":" + seconds + "s";

            if (timeMask < 0)
            {
                this.mask = "0.00";
                timeMask = 0f;
                this.gameObject.transform.Find("Head/mask").gameObject.SetActive(false);
            }
        }
        else
        {
            timeMask = 0f;
        }
    }

    [PunRPC]
    public void timerDisinfectant()
    {
        if (this.gameObject.transform.Find("Hands/sanitizer").gameObject.activeSelf)
        {

            timeDisinfectant -= Time.deltaTime;
            string minutes = Mathf.Floor(timeDisinfectant / 60).ToString("0");
            string seconds = (timeDisinfectant % 60).ToString("00");

            this.disinfectant = minutes + ":" + seconds + "s";

            if (timeDisinfectant < 0)
            {
                this.disinfectant = "0.00";
                timeDisinfectant = 0f;
                this.gameObject.transform.Find("Hands/sanitizer").gameObject.SetActive(false);
            }
        }
        else
        {
            timeDisinfectant = 0f;
        }
    }

    [PunRPC]
    public void timerDisinfectantPower()
    {

        if (this.gameObject.transform.Find("Hands/sanitizerPowerUp").gameObject.activeSelf)
        {

            timeDisinfectantPower -= Time.deltaTime;
            string minutes = Mathf.Floor(timeDisinfectantPower / 60).ToString("0");
            string seconds = (timeDisinfectantPower % 60).ToString("00");

            this.disinfectantPower = minutes + ":" + seconds + "s";

            if (timeDisinfectantPower < 0)
            {
                this.disinfectantPower = "0.00";
                timeDisinfectantPower = 0f;
                this.gameObject.transform.Find("Hands/sanitizerPowerUp").gameObject.SetActive(false);
            }
        }
        else
        {
            timeDisinfectantPower = 0f;
        }
    }

    void jumping()
    {
        if (Input.GetKeyDown(KeyCode.C) && playerIsOnGrounded)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            playerIsOnGrounded = false;
        }

    }
    

}