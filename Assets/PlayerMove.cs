using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMove : MonoBehaviour {

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start() {

        agent = GetComponent<NavMeshAgent>();
    


    }


    public void Move(Vector3 point) {

        agent.SetDestination(point);
    
        
    }
}
