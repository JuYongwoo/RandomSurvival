using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStateMachine : MonoBehaviour
{
    public float particleSpeed = 5f;
    private GameObject attackParticlePrefab;
    public static Func<PlayerState> playerstate;
    public static Action<PlayerState> setPlayerstate;
    private GameObject currentTarget;
    private Coroutine attackingCoroutine;
    private bool isAttacking = false;

    private NavMeshAgent agent;

    private GameObject moveMarkPrefab;
    private GameObject currentMoveMark;

    private Vector3 currendestination;

    void Awake()
    {
        attackParticlePrefab = Resources.Load<GameObject>("Prefabs/AttackParticle");
        moveMarkPrefab = Resources.Load<GameObject>("Prefabs/MoveMark");

        agent = GetComponent<NavMeshAgent>();
        agent.speed = 0.01f;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.acceleration = 1000f;
        agent.angularSpeed = 0f;
        agent.autoBraking = false;
        agent.stoppingDistance = 0f;

        InputManager.setAttackTarget = go => currentTarget = go;
        InputManager.setDestination = dest => currendestination = dest;
    }

    private void Update()
    {
        var state = playerstate();

        switch (state)
        {
            case PlayerState.Idle:
                var enemies = detectEnemies();
                if (enemies.Count > 0)
                {
                    currentTarget = GetClosestThreatEnemy(enemies).gameObject;
                    setPlayerstate(PlayerState.Attack_Attacking);
                }
                break;

            case PlayerState.MoveStart:
                moveStart(currendestination);
                setPlayerstate(PlayerState.Moving);
                break;

            case PlayerState.Moving:
                moving();
                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    Destroy(currentMoveMark);
                    setPlayerstate(PlayerState.Idle);
                }
                break;

            case PlayerState.AttackStart:
                Destroy(currentMoveMark);
                if (currentTarget == null)
                {
                    setPlayerstate(PlayerState.Idle);
                }
                else if (isInAttackRange(currentTarget))
                {
                    setPlayerstate(PlayerState.Attack_Attacking);
                }
                else
                {
                    setPlayerstate(PlayerState.Attack_MoveStart);
                }
                break;

            case PlayerState.Attack_MoveStart:
                if (currentTarget == null)
                {
                    setPlayerstate(PlayerState.Idle);
                }
                else
                {
                    moveStart(currentTarget.transform.position);
                    setPlayerstate(PlayerState.Attack_Moving);
                }
                break;

            case PlayerState.Attack_Moving:
                if (currentTarget == null)
                {
                    setPlayerstate(PlayerState.Idle);
                    break;
                }

                moving();

                if (isInAttackRange(currentTarget))
                {
                    stopMoving();
                    setPlayerstate(PlayerState.Attack_Attacking);
                }
                break;

            case PlayerState.Attack_Attacking:
                if (currentTarget == null)
                {
                    setPlayerstate(PlayerState.Idle);
                }
                else if (!isInAttackRange(currentTarget))
                {
                    setPlayerstate(PlayerState.Attack_MoveStart);
                }
                else if (!isAttacking)
                {
                    attackingCoroutine = StartCoroutine(attack());
                }
                break;

            case PlayerState.AttackMove_MoveStart:
                moveStart(currendestination);
                setPlayerstate(PlayerState.AttackMove_Moving);
                break;

            case PlayerState.AttackMove_Moving:
                moving();

                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    Destroy(currentMoveMark);
                    setPlayerstate(PlayerState.Idle);
                }

                var detected = detectEnemies();
                if (detected.Count > 0)
                {
                    currentTarget = GetClosestThreatEnemy(detected).gameObject;
                    setPlayerstate(PlayerState.AttackMove_Attacking);
                }
                break;

            case PlayerState.AttackMove_Attacking:
                if (currentTarget == null)
                {
                    setPlayerstate(PlayerState.AttackMove_MoveStart);
                }
                else if (!isAttacking)
                {
                    attackingCoroutine = StartCoroutine(attack());
                }
                break;
        }
    }

    private IEnumerator attack()
    {
        isAttacking = true;

        transform.LookAt(currentTarget.transform.position);

        GameObject particle = Instantiate(attackParticlePrefab, transform.position + Vector3.up * 1.2f, Quaternion.identity);
        particle.GetComponent<AttackParticle>().SetTarget(currentTarget.transform);
        StartCoroutine(moveParticleToTarget(particle, currentTarget.transform.position));

        yield return new WaitForSeconds(1f);

        isAttacking = false;
    }

    public List<EnemyBase> detectEnemies()
    {
        List<EnemyBase> detectedEnemies = new List<EnemyBase>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f, LayerMask.GetMask("EnemyClick"));

        foreach (var collider in hitColliders)
        {
            detectedEnemies.Add(collider.gameObject.GetComponent<EnemyBase>());
        }

        return detectedEnemies;
    }

    private EnemyBase GetClosestThreatEnemy(List<EnemyBase> enemies)
    {
        EnemyBase closestThreat = null;
        EnemyBase closestAny = null;
        float minThreatSqrDist = Mathf.Infinity;
        float minAnySqrDist = Mathf.Infinity;
        Vector3 origin = transform.position;

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            float sqrDist = (enemy.transform.position - origin).sqrMagnitude;

            if (enemy.power > 0f && sqrDist < minThreatSqrDist)
            {
                minThreatSqrDist = sqrDist;
                closestThreat = enemy;
            }

            if (sqrDist < minAnySqrDist)
            {
                minAnySqrDist = sqrDist;
                closestAny = enemy;
            }
        }

        return closestThreat ?? closestAny;
    }


    private IEnumerator moveParticleToTarget(GameObject particle, Vector3 targetPos)
    {
        float threshold = 0.1f;

        while (particle != null && Vector3.Distance(particle.transform.position, targetPos) > threshold)
        {
            Vector3 dir = (targetPos - particle.transform.position).normalized;
            particle.transform.position += dir * particleSpeed * Time.deltaTime;
            yield return null;
        }

        if (particle != null)
            Destroy(particle);
    }

    private void moveStart(Vector3 dest)
    {
        // 유효 y 높이 강제 적용
        Vector3 navPos = new Vector3(dest.x, transform.position.y, dest.z);
        agent.SetDestination(navPos);

        if (moveMarkPrefab != null)
        {
            Destroy(currentMoveMark);
            currentMoveMark = Instantiate(moveMarkPrefab, navPos + Vector3.up * 0.1f, Quaternion.Euler(90, 0, 0));
        }
    }

    private void moving()
    {
        Vector3 dir = agent.desiredVelocity.normalized;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            transform.position += dir * 10 * Time.deltaTime;
        }
    }

    private void stopMoving()
    {
        agent.destination = transform.position;
        Destroy(currentMoveMark);
    }

    private bool isInAttackRange(GameObject target)
    {
        if (target == null) return false;
        return (transform.position - target.transform.position).sqrMagnitude <= 100f;
    }
}
