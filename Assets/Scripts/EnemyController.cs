using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;
    public float damage;
    public float damageRate;

    Transform target;
    NavMeshAgent agent;
    Animator anim;


    private bool inPunchRadius;
    float nextDamage;

    // Start is called before the first frame update
    void Start()
    {
        nextDamage = 0f;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            anim.SetBool("isRunning", true);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
            if (inPunchRadius)
            {               
                anim.SetBool("isRunning", false);
            }
            /*else
            {
                anim.SetBool("isRunning", true);
            }*/

        }

        else
        {
            anim.SetBool("isRunning", false);
            agent.ResetPath();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPunchRadius = true;
            anim.SetBool("isRunning", false);
            anim.SetBool("isPunching", true);
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player") && nextDamage < Time.time)
        {
            playerHealth thePlayerHealth = other.gameObject.GetComponent<playerHealth>();
            thePlayerHealth.addDamage(damage);
            nextDamage = Time.time + damageRate;
            Debug.Log(other.name);

        }
    }


    void OnTriggerExit(Collider other)
    {
        inPunchRadius = false;
        anim.SetBool("isPunching", false);
        anim.SetBool("isRunning", true);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
