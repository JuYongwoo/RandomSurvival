using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent agent;

    private GameObject moveMarkPrefab;
    private GameObject currentMoveMark;

    public static Action<float> OnRefreshStaminaBar;

    public static Func<PlayerState> playerstate;
    public static Action<PlayerState> setPlayerstate;
    private Vector3 currendestination;

    private void Awake()
    {
        PlayerAttack.moveStop = () => {
            agent.destination = gameObject.transform.position;
            if (currentMoveMark != null) Destroy(currentMoveMark);
        };

        moveMarkPrefab = Resources.Load<GameObject>("Prefabs/MoveMark");

        agent = GetComponent<NavMeshAgent>();

        agent.speed = 0.01f; //position은 직접 처리, 회전 시 속도 저하를 없애기 위해
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.acceleration = 1000f;
        agent.angularSpeed = 0f;
        agent.autoBraking = false;
        agent.stoppingDistance = 0f;

        InputManager.setDestination = (dest) =>
        {
            currendestination = dest;
        };
    }

    private void Update()
    {
        var state = playerstate();

        if (state == PlayerState.MoveStart)
        {
            moveStart();
            setPlayerstate(PlayerState.Moving);

        }
        else if (state == PlayerState.Moving)
        {
            moving();
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                if (currentMoveMark != null) Destroy(currentMoveMark);
                setPlayerstate(PlayerState.Idle);
            }

        }
        else if (state == PlayerState.AttackMove_MoveStart)
        {
            moveStart();
            setPlayerstate(PlayerState.AttackMove_Moving);

        }
        else if (state == PlayerState.AttackMove_Moving)
        {
            moving();
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                if (currentMoveMark != null) Destroy(currentMoveMark);
                setPlayerstate(PlayerState.Idle);
            }
        }

    }
    private void moveStart()
    {
        agent.SetDestination(currendestination);

        if (moveMarkPrefab != null)
        {

            if (currentMoveMark != null)
                Destroy(currentMoveMark); // 기존 마커 제거

            currentMoveMark = Instantiate(moveMarkPrefab, currendestination + Vector3.up * 0.1f, Quaternion.Euler(90, 0, 0));
        }
    }
    private void moving()
    {

        Vector3 dir = agent.desiredVelocity.normalized;

        transform.rotation = Quaternion.LookRotation(dir);
        transform.position += dir * 10 * Time.deltaTime;
    }


}
