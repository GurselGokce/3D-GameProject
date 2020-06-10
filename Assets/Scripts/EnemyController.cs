using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float lookRadius = 0.1f;
    public float rotationSpeed = 4f;
    Transform target;
    NavMeshAgent agent;
    public Rigidbody rb;

    public float speed = 2f;
    Vector3 playerDirection;
    float distance;

    //Vector3 movement = new Vector3(1, 0, 0);
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            playerDirection = target.position - transform.position;
            Vector3 targetPosition = target.transform.position;
            Quaternion direction = Quaternion.LookRotation(target.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, direction, rotationSpeed * Time.deltaTime);
            // Setting Rotation along z axis to zero
            direction.z = 0;

            // Setting Rotation along x axis to zero
            direction.x = 0;
        }
        else
        {

        }

    }

    void FixedUpdate()
    {

        if (distance <= lookRadius)
        {
            moveEnemy(playerDirection);
        }
        else
        {

        }
       
    }

    void OnDrawGizmosSelected(Vector3 direction)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
       

    }

    void moveEnemy(Vector3 direction)
    {
        rb.MovePosition(transform.position + (playerDirection * speed * Time.deltaTime));
    }

}
