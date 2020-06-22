using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SVR;

public class ReptileMovement : MonoBehaviourPun
{
    GameObject player1;
    GameObject player2;
    private Animator anim;
    private PhotonView pv;

    Vector3[] directions = { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
    Vector3 currentDir;
    int reptilehealth = 100;
    Vector3 direction1;
    Vector3 direction2;

    public AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        currentDir = directions[0];
        pv = this.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        player1 = GameObject.FindGameObjectWithTag("player1");
        player2 = GameObject.FindGameObjectWithTag("player2");

        if (!PhotonPlayer.starting)
        {

            if (player1 != null)
            {
                Vector3 direction1 = player1.transform.position - this.transform.position;
                float angle1 = Vector3.Angle(direction1, this.transform.forward);

                if (Vector2.Distance(player1.transform.position, this.transform.position) < 1.0 && player1.GetComponent<NetworkObject>().GetHealth()>0)
                {
                    anim.SetBool("isIdle", false);
                    EnemyController(player1.transform, direction1);
                }
                else
                {
                    anim.SetBool("isIdle", false);
                    this.transform.Translate(0, 0, 0.01f);
                    anim.SetBool("isWalk", true);
                    anim.SetBool("isRun", false);
                    anim.SetBool("isAttack", false);
                    anim.SetBool("isHit", false);
                }
            }

            if (player2 != null)
            {
                Vector3 direction2 = player2.transform.position - this.transform.position;
                float angle2 = Vector3.Angle(direction2, this.transform.forward);

                if (Vector2.Distance(player2.transform.position, this.transform.position) < 1.0 && player2.GetComponent<NetworkObject>().GetHealth() > 0)

                {
                    anim.SetBool("isIdle", false);
                    EnemyController(player2.transform, direction2);
                }
                else
                {
                    anim.SetBool("isIdle", false);
                    this.transform.Translate(0, 0, 0.01f);
                    anim.SetBool("isWalk", true);
                    anim.SetBool("isRun", false);
                    anim.SetBool("isAttack", false);
                    anim.SetBool("isHit", false);
                }
            }

        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalk", false);
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("isHit", false);

        }

    }

    void EnemyController(Transform playerPosition, Vector3 direction)
    {


        direction.y = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        this.transform.Translate(0, 0, 0.01f);
        anim.SetBool("isWalk", false);

        if (direction.magnitude > 0.9)
        {
            this.transform.Translate(0, 0, 0.02f);
            anim.SetBool("isRun", true);
            anim.SetBool("isAttack", false);
            anim.SetBool("isHit", false);
        }
        else
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", true);
            anim.SetBool("isHit", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DestructableObj" || collision.gameObject.tag == "wall")
        {
            currentDir = directions[Random.Range(0, 4)];
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(currentDir), 45f);
        }

    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Power")
        {
             
            this.reptilehealth -= 10;
            this.gameObject.GetComponent<NetworkObject>().SetHealth(reptilehealth);

            anim.SetBool("isHit", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalk", false);
            anim.SetBool("isRun", false);
            anim.SetBool("isAttack", false);
            if (this.gameObject.GetComponent<NetworkObject>().GetHealth() == 0f)
            {
                
                StartCoroutine(EnemyLose());
                deathSound.Play();
                PhotonPlayer.score_earn += 15f;
            }
        }
    }

    [PunRPC]
    public IEnumerator EnemyLose()
    {
        anim.SetBool("isDead", true);
        anim.SetBool("isHit", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalk", false);
        anim.SetBool("isRun", false);
        anim.SetBool("isAttack", false);
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

    }


}