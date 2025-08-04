using System;
using UnityEngine;
using UnityEngine.AI;

public interface IAttackable
{
    void TakeDamage(float damage);
}
public class Enemy : MonoBehaviour, IAttackable
{

    public static Action hitplayer;
    private GameObject target;
    private NavMeshAgent agent; //NavMeshAgent 컴포넌트 선언
    private Animator anim;
    private float hp = 100;

    private void Awake()
    {
        target = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        //TODO 가까워지면 공격모션, 멀면 이동
    }

    private void Update()
    {
        agent.destination = target.transform.position;
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player"){
            hitplayer?.Invoke();
        }
    }
}
