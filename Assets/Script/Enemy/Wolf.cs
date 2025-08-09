using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Wolf : EnemyBase
{
    private NavMeshAgent agent;
    private Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            target = player.transform;

        hp = 100;
        power = 10;
    }

    private void Start()
    {
        InvokeRepeating("UpdateDestination", 0.5f, 0.5f);
    }


    private void UpdateDestination()
    {

        if (target != null)
            agent.SetDestination(target.position);
    }
}
