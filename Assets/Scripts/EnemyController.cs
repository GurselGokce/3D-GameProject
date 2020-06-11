using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float lookRadius = 100f;
    public float rotationSpeed = 4f;
    Transform target;
    NavMeshAgent agent;
    public Rigidbody rb;
    Animator animator;

    public float speed = 2f;
    Vector3 playerDirection;
    float distance = 15f;
    public float attackRange = 0.80f;

    //Vector3 movement = new Vector3(1, 0, 0);
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("Attacking", false);
        animator.SetBool("Moving", false);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);

        if (distance <= lookRadius)
        {
            playerDirection = target.position - transform.position;
            Vector3 targetPosition = target.transform.position;
            Quaternion direction = Quaternion.LookRotation(targetPosition - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, direction, rotationSpeed * Time.deltaTime);

            direction.z = 0;

            direction.x = 0;
        }


    }

    void FixedUpdate()
    {

        if (distance <= lookRadius && distance>attackRange)
        {
            moveEnemy(playerDirection);
            animator.SetBool("Attacking", false);
            animator.SetBool("Moving", true);
        }


        if (distance > lookRadius)
        {
            animator.SetBool("Moving", false);
        }

        if (distance < 1.5f)
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Attacking", true);
        }


    }

    void OnDrawGizmosSelected(Vector3 direction)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);


    }

    void moveEnemy(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }

}
