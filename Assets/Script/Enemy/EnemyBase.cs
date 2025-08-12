using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected float hp;
    [HideInInspector]
    public float power;
    public int EXP;
    protected Transform player;
    public static Action<float> hitplayer;
    static public Action<int> deltaEnemyCount;
    public static Action<int> deltaPlayerEXP;

    // NavMeshAgent 관련
    protected NavMeshAgent agent;
    private Vector3 lastDestination;
    private float destinationThreshold = 1.0f; // 0.5m 이상 움직였을 때만 경로 갱신

    protected virtual void Awake()
    {
        if (GetType() != typeof(Gate))
            deltaEnemyCount?.Invoke(+1);

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.autoBraking = false;
            agent.stoppingDistance = 0f;
        }
    }

    protected virtual void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        InvokeRepeating("UpdateDest", 0.0f, 0.5f);
    }

    protected virtual void UpdateDest()
    {
        float distSqr = (player.position - transform.position).sqrMagnitude;

        // NavMeshAgent 최적화
        if (agent != null)
        {
            // 플레이어가 일정 거리 이내일 때만 NavMeshAgent 활성화
            if (distSqr < 3000f)
            {
                if (!agent.enabled)
                    agent.enabled = true;

                if ((lastDestination - player.position).sqrMagnitude > destinationThreshold * destinationThreshold)
                {
                    agent.SetDestination(player.position);
                    lastDestination = player.position;
                }
            }
            else
            {
                if (agent.enabled)
                    agent.enabled = false;
            }
        }
    }

    public virtual void TakeDamage(float getDamage)
    {
        hp -= getDamage;
        if (hp <= 0) Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitplayer?.Invoke(power);
        }
    }

    protected virtual void OnDestroy()
    {
        if (GetType() != typeof(Gate))
        {
            deltaEnemyCount?.Invoke(-1);
            deltaPlayerEXP?.Invoke(EXP);
        }
    }
}
