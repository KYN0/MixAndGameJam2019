using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("properties")]
    public float life;
    public float shurikenDMG;
    public float atkDMG;

    public Animator anim;

    [Header("patrol and Chasing")]
    public float DetectRange;
    public float atkRange;
    public Transform player;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public bool isChasing;
    public bool enableAtk;
    public bool getDmg;
    public bool getAtk;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isChasing = false;
        agent.autoBraking = false;
        enableAtk = false;
        getDmg = false;
        getAtk = false;
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        float tempdis = Vector3.Distance(player.position, transform.position);
       
        if (Vector3.Distance(player.position, transform.position) <= DetectRange)
        {
            isChasing = true;
            //go to player
        }else{
            isChasing = false;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f && isChasing.Equals(false)){
            GotoNextPoint();

        }
        if(isChasing.Equals(true)){
            GetComponent<NavMeshAgent>().destination = player.transform.position;
            if (Vector3.Distance(player.position, transform.position) <= atkRange){
                enableAtk = true;
            }
        }

        if(enableAtk.Equals(true)){
            atk();
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
               
                anim.SetBool("atk", false);
                anim.SetBool("idle", true);
            }

            if(life<=0){
                Destroy(gameObject);
            }

    }

    public void atk(){
        anim.SetBool("atk", true);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("shuriken") && getDmg.Equals(false)){
            getDmg = true;
            life -= shurikenDMG;
            StartCoroutine(reset());
        }
        if(other.CompareTag("Player") && getAtk.Equals(false)){
            getAtk = true;
            other.GetComponent<Player>().takeDmg(atkDMG);
            StartCoroutine(resetDmg());
        }
    }

    IEnumerator reset(){

        yield return new WaitForSeconds(0.3f);
        getDmg = false;
    }

     IEnumerator resetDmg(){

        yield return new WaitForSeconds(0.3f);
        getAtk = false;
    }
}
