using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    const float motionSmoothTime = 0.1f;

    NavMeshAgent agent;
    Animator animator;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float speedPer = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPer", speedPer, motionSmoothTime, Time.deltaTime);
    }
}
