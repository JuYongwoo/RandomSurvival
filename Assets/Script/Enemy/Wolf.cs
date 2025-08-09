using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Wolf : EnemyBase
{
    private NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();


        hp = 100;
        power = 0;
    }

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("UpdateDestination", 0.5f, 0.5f);
    }

    private void UpdateDestination()
    {

        if (player != null)
            agent.SetDestination(player.position);
    }
}
