using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Wolf : EnemyBase
{
    private Animator anim;
    private NavMeshAgent agent;
    private Transform target;

    private const float destinationUpdateInterval = 0.5f;
    private Coroutine moveCoroutine;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        GameObject player = GameObject.Find("Player");
        if (player != null)
            target = player.transform;

        hp = 100;
        power = 10;
    }

    private void OnEnable()
    {
        if (moveCoroutine == null)
            moveCoroutine = StartCoroutine(UpdateDestination());
    }

    private void OnDisable()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
        moveCoroutine = null;
    }

    private void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    private IEnumerator UpdateDestination()
    {
        WaitForSeconds wait = new WaitForSeconds(destinationUpdateInterval);

        while (true)
        {
            if (target != null)
                agent.SetDestination(target.position);

            yield return wait;
        }
    }
}
