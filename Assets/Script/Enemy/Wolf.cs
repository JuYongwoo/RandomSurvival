using UnityEngine;
using UnityEngine.AI;

public class Wolf : EnemyBase
{
    private Animator anim;
    private NavMeshAgent agent; //NavMeshAgent 컴포넌트 선언
    private GameObject target;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        hp = 100;
        power = 10;

    }

    private void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);

        if(target != null) agent.destination = target.transform.position;


    }



}
