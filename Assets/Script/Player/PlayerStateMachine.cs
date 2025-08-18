using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum PlayerState
{
    Idle,
    MoveStart,
    Moving,
    AttackStart,
    Attack_Attacking,
    Attack_MoveStart,
    Attack_Moving,
    AttackMove_MoveStart,
    AttackMove_Moving,
    AttackMove_Attacking
}

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState state;

    public static Func<PlayerState> playerstate;
    public static Func<Animator> animator;
    public static Func<AudioClip> getPlayerWeaponFireSound;
    public static Func<GameObject> getPlayerWeaponProjectile;
    public static Func<float> getPlayerWeaponReloadTime;
    public static Func<float> getPlayerWeaponAttackRange;

    private GameObject currentTarget;
    private GameObject moveMarkPrefab;
    private Coroutine attackingCoroutine;
    private bool isAttacking;

    private GameObject currentMoveMark;
    private Vector3 currentDestination;

    private NavMeshAgent agent;

    private void Awake()
    {
        moveMarkPrefab = Addressables.LoadAssetAsync<GameObject>("MoveMark").WaitForCompletion();


        agent = GetComponent<NavMeshAgent>();
        agent.speed = 0.01f;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.acceleration = 1000f;
        agent.angularSpeed = 0f;
        agent.autoBraking = false;
        agent.stoppingDistance = 0f;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        InputManager.setAttackTarget = go => currentTarget = go;
        InputManager.setDestination = dest => currentDestination = dest;
        InputManager.setPlayerstate = SetPlayerState;
    }

    private void Update()
    {
        switch (state)
        {
            case PlayerState.Idle:
                HandleIdle();
                break;
            case PlayerState.MoveStart:
                HandleMoveStart();
                break;
            case PlayerState.Moving:
                HandleMoving();
                break;
            case PlayerState.AttackStart:
                HandleAttackStart();
                break;
            case PlayerState.Attack_MoveStart:
                HandleAttackMoveStart();
                break;
            case PlayerState.Attack_Moving:
                HandleAttackMoving();
                break;
            case PlayerState.Attack_Attacking:
                HandleAttackAttacking();
                break;
            case PlayerState.AttackMove_MoveStart:
                HandleAttackMoveMoveStart();
                break;
            case PlayerState.AttackMove_Moving:
                HandleAttackMoveMoving();
                break;
            case PlayerState.AttackMove_Attacking:
                HandleAttackMoveAttacking();
                break;
        }
    }

    private void HandleIdle()
    {
        var enemies = DetectEnemies();
        if (enemies.Count > 0)
        {
            currentTarget = GetClosestThreatEnemy(enemies)?.gameObject;
            SetPlayerState(PlayerState.Attack_Attacking);
        }
    }

    private void HandleMoveStart()
    {
        MoveStart(currentDestination);
        SetPlayerState(PlayerState.Moving);
    }

    private void HandleMoving()
    {
        Moving();
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            DestroyCurrentMoveMark();
            SetPlayerState(PlayerState.Idle);
        }
    }

    private void HandleAttackStart()
    {
        DestroyCurrentMoveMark();
        if (currentTarget == null)
        {
            SetPlayerState(PlayerState.Idle);
        }
        else if (IsInAttackRange(currentTarget))
        {
            SetPlayerState(PlayerState.Attack_Attacking);
        }
        else
        {
            SetPlayerState(PlayerState.Attack_MoveStart);
        }
    }

    private void HandleAttackMoveStart()
    {
        if (currentTarget == null)
        {
            SetPlayerState(PlayerState.Idle);
        }
        else
        {
            MoveStart(currentTarget.transform.position);
            SetPlayerState(PlayerState.Attack_Moving);
        }
    }

    private void HandleAttackMoving()
    {
        if (currentTarget == null)
        {
            SetPlayerState(PlayerState.Idle);
            return;
        }

        Moving();

        if (IsInAttackRange(currentTarget))
        {
            StopMoving();
            SetPlayerState(PlayerState.Attack_Attacking);
        }
    }

    private void HandleAttackAttacking()
    {
        if (currentTarget == null)
        {
            SetPlayerState(PlayerState.Idle);
        }
        else if (!IsInAttackRange(currentTarget))
        {
            SetPlayerState(PlayerState.Attack_MoveStart);
        }
        else if (!isAttacking)
        {
            attackingCoroutine = StartCoroutine(Attack());
        }
    }

    private void HandleAttackMoveMoveStart()
    {
        MoveStart(currentDestination);
        SetPlayerState(PlayerState.AttackMove_Moving);
    }

    private void HandleAttackMoveMoving()
    {
        Moving();

        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            DestroyCurrentMoveMark();
            SetPlayerState(PlayerState.Idle);
        }

        var detected = DetectEnemies();
        if (detected.Count > 0)
        {
            currentTarget = GetClosestThreatEnemy(detected)?.gameObject;
            SetPlayerState(PlayerState.AttackMove_Attacking);
        }
    }

    private void HandleAttackMoveAttacking()
    {
        if (currentTarget == null)
        {
            SetPlayerState(PlayerState.AttackMove_MoveStart);
        }
        else if (!isAttacking)
        {
            attackingCoroutine = StartCoroutine(Attack());
        }
    }

    private void SetPlayerState(PlayerState s)
    {
        state = s;

        int animState = (int)s;
        if (s == PlayerState.AttackMove_Moving || s == PlayerState.Attack_Moving)
            animState = (int)PlayerState.Moving;
        if (s == PlayerState.AttackMove_Attacking)
            animState = (int)PlayerState.Attack_Attacking;

        animator?.Invoke()?.SetInteger("State", animState);
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        if (currentTarget != null)
            transform.LookAt(currentTarget.transform.position);

        if (getPlayerWeaponFireSound() != null) ManagerObject.am.PlayAudioClip(getPlayerWeaponFireSound());
        var particle = Instantiate(getPlayerWeaponProjectile(), transform.position + Vector3.up * 1.2f, Quaternion.identity);
        var attackParticle = particle.GetComponent<AttackProjectile>();
        if (attackParticle != null && currentTarget != null)
            attackParticle.SetTarget(currentTarget.transform);

        yield return new WaitForSeconds(getPlayerWeaponReloadTime());

        isAttacking = false;
    }

    public List<EnemyBase> DetectEnemies()
    {
        var detectedEnemies = new List<EnemyBase>();
        var hitColliders = Physics.OverlapSphere(transform.position, 10f, LayerMask.GetMask("EnemyClick"));

        foreach (var collider in hitColliders)
        {
            var enemy = collider.GetComponent<EnemyBase>();
            if (enemy != null)
                detectedEnemies.Add(enemy);
        }

        return detectedEnemies;
    }

    private EnemyBase GetClosestThreatEnemy(List<EnemyBase> enemies)
    {
        EnemyBase closestThreat = null;
        EnemyBase closestAny = null;
        float minThreatSqrDist = float.MaxValue;
        float minAnySqrDist = float.MaxValue;
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


    private void MoveStart(Vector3 dest)
    {
        agent.destination = dest;

        if (moveMarkPrefab != null)
        {
            DestroyCurrentMoveMark();
            currentMoveMark = Instantiate(moveMarkPrefab, dest + Vector3.up * 0.1f, Quaternion.Euler(90, 0, 0));
        }
    }

    private void Moving()
    {
        Vector3 dir = agent.desiredVelocity.normalized;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            transform.position += dir * 10 * Time.deltaTime;
        }
    }

    private void StopMoving()
    {
        agent.destination = transform.position;
        DestroyCurrentMoveMark();
    }

    private bool IsInAttackRange(GameObject target)
    {
        if (target == null) return false;
        return (transform.position - target.transform.position).sqrMagnitude <= Math.Pow(getPlayerWeaponAttackRange(), 2);
    }



    private bool IsSimilarPath(NavMeshPath a, NavMeshPath b)
    {
        if (a == null || b == null) return false;
        var ac = a.corners; var bc = b.corners;
        if (ac.Length != bc.Length) return false;
        for (int i = 0; i < ac.Length; i++)
            if ((ac[i] - bc[i]).sqrMagnitude > 0.04f)
                return false;
        return true;
    }

    private void DestroyCurrentMoveMark()
    {
        if (currentMoveMark != null)
        {
            Destroy(currentMoveMark);
            currentMoveMark = null;
        }
    }
}
