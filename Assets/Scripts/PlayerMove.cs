using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMove : MonoBehaviour {

    NavMeshAgent agent;
    Transform target;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start() {

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
        
    }

    public void Move(Vector3 point) {

        agent.SetDestination(point);
    
        
    }

    public void FollowTarget (Interact newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 0.85f;
        agent.updateRotation = false;
        target = newTarget.InteractionTransform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - rb.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        rb.rotation = lookRotation = Quaternion.Slerp(rb.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
