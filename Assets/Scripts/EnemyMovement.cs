using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using SVR;

public class EnemyMovement : MonoBehaviourPun, IPunObservable
{
    GameObject player1;
    GameObject player2;
    static Animator anim;

    Vector3[] directions = { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
    Vector3 currentDir;

    public void Awake()
    {
        PhotonNetwork.SerializationRate = 5;
        PhotonNetwork.SendRate = 20;
      
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentDir = directions[0];
    }

    // Update is called once per frame
    void Update()
    {
        player1 = GameObject.Find("/Player1");
        player2 = GameObject.Find("/Player2");



        if (player1 != null)
        {
            Vector3 direction1 = player1.transform.position - this.transform.position;
            float angle1 = Vector3.Angle(direction1, this.transform.forward);

            if (Vector2.Distance(player1.transform.position, this.transform.position) < 10 && angle1 <30)
            {
                EnemyController(player1.transform,direction1);
            }

            else if (player2 != null)
            {
                Vector3 direction2 = player2.transform.position - this.transform.position;
                float angle2 = Vector3.Angle(direction2, this.transform.forward);

                if (Vector2.Distance(player2.transform.position, this.transform.position) < 10 && angle2 <30)
                {
                    EnemyController(player2.transform,direction2);
                }
            }
            else
            {
                this.transform.Translate(0, 0, 0.01f);
                anim.SetBool("isWalking", true);
                anim.SetBool("isRun", false);
                anim.SetBool("isAttack", false);
            }

        }
        else
        {
            this.transform.Translate(0, 0, 0.01f);
            anim.SetBool("isWalking", true);
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", false);
        }
    }

    void EnemyController(Transform playerPosition, Vector3 direction)
    {


        direction.y = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        this.transform.Translate(0, 0, 0.01f);
        anim.SetBool("isWalking", false);

        if (direction.magnitude >= 3)

        {
            this.transform.Translate(0, 0, 0.02f);
            anim.SetBool("isRun", true);
            anim.SetBool("isAttack", false);
        }
        else
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        currentDir = directions[Random.Range(0, 4)];
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(currentDir), 45f); ;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.rotation);
            stream.SendNext(anim.GetBool("isWalking"));
            stream.SendNext(anim.GetBool("isRun"));
            stream.SendNext(anim.GetBool("isAttack"));
            Debug.Log("Sending");
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            anim.SetBool("isWalking", (bool)stream.ReceiveNext());
            anim.SetBool("isRun", (bool)stream.ReceiveNext());
            anim.SetBool("isAttack", (bool)stream.ReceiveNext());
            Debug.Log("Receiving");
        }
    }

}
