using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatNavigation : MonoBehaviour
{
    [SerializeField] Transform player;
    NavMeshAgent navMeshAgent;

    Vector3 target = new Vector3(0f, 0f, 0f);

    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        target = player.position;
    }

    void Update() {
        navMeshAgent.SetDestination(target);
    }

    public void SetTarget(Vector3 waitPointPos) {
        target = waitPointPos;
    }
}
