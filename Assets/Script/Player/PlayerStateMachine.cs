using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PlayerStateMachine : MonoBehaviour
{
    public float particleSpeed = 5f; //TODO basestat so ���Ϸ� �̵�
    private GameObject attackParticlePrefab;
    public static Func<PlayerState> playerstate;
    public static Action<PlayerState> setPlayerstate;
    private GameObject currentTarget;
    private Coroutine attackingCoroutine;

    private NavMeshAgent agent;

    private GameObject moveMarkPrefab;
    private GameObject currentMoveMark;

    private Vector3 currendestination;

    void Awake()
    {
        attackParticlePrefab = Resources.Load<GameObject>("Prefabs/AttackParticle");

        moveMarkPrefab = Resources.Load<GameObject>("Prefabs/MoveMark");

        agent = GetComponent<NavMeshAgent>();

        agent.speed = 0.01f; //position�� ���� ó��, ȸ�� �� �ӵ� ���ϸ� ���ֱ� ����
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.acceleration = 1000f;
        agent.angularSpeed = 0f;
        agent.autoBraking = false;
        agent.stoppingDistance = 0f;

        InputManager.setAttackTarget = (go) => {
            currentTarget = go;
        };
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
            moveStart(currendestination);
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
        else if (state == PlayerState.AttackStart) // Ŭ�� ����
        {
            setPlayerstate(PlayerState.Attack_MoveStart);
        }
        else if (state == PlayerState.Attack_MoveStart) // ���� ������� �̵�
        {
            moveStart(currentTarget.transform.position);
            setPlayerstate(PlayerState.Attack_Moving);
        }
        else if (state == PlayerState.Attack_Moving) //���� ������� �̵� ��
        {
            moving();
            if (attackingCoroutine == null && Vector3.Distance(transform.position, currentTarget.transform.position) <= 10f)
            {
                moveStop();
                setPlayerstate(PlayerState.Attack_Attacking); //������ ���߰� ����
            }

        }
        else if (state == PlayerState.Attack_Attacking)
        {

            if (currentTarget == null) setPlayerstate(PlayerState.Idle);
            if (attackingCoroutine == null) attackingCoroutine = StartCoroutine(attack());
            if (attackingCoroutine == null && Vector3.Distance(transform.position, currentTarget.transform.position) > 10f)
            {
                setPlayerstate(PlayerState.Attack_MoveStart); //������ �Ÿ��� �ٽ� ���������� �ٽ� ������ �̵�
            }
        }
        else if (state == PlayerState.AttackMove_MoveStart)
        {
            moveStart(currendestination);
            setPlayerstate(PlayerState.AttackMove_Moving);

        }
        else if (state == PlayerState.AttackMove_Moving)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                if (currentMoveMark != null) Destroy(currentMoveMark);
                setPlayerstate(PlayerState.Idle);
            }

            moving();

            List<GameObject> detectedEnemies = detectEnemies();
            if (detectedEnemies.Count != 0)
            {
                currentTarget = detectedEnemies[0];
                setPlayerstate(PlayerState.AttackMove_Attacking);
            }

        }
        else if (state == PlayerState.AttackMove_Attacking)
        {
            if (currentTarget == null) setPlayerstate(PlayerState.AttackMove_MoveStart);
            if (attackingCoroutine == null) attackingCoroutine = StartCoroutine(attack());
        }
    }

    private IEnumerator attack()
    {

        gameObject.transform.LookAt(currentTarget.transform.position); //��� �ٶ󺻴�
        GameObject particle = Instantiate(attackParticlePrefab, transform.position + Vector3.up * 1.2f, Quaternion.identity);
        particle.GetComponent<AttackParticle>().SetTarget(currentTarget.transform);
        StartCoroutine(moveParticleToTarget(particle, currentTarget.transform.position));
        yield return new WaitForSeconds(1f);
        attackingCoroutine = null;
    }


    public List<GameObject> detectEnemies()
    {
        List<GameObject> detectedEnemies = new List<GameObject>();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f, 1<<6);

        foreach (var collider in hitColliders)
        {
            GameObject enemy = collider.transform.root.gameObject;
            detectedEnemies.Add(enemy);
        }

        return detectedEnemies;
    }

    private IEnumerator moveParticleToTarget(GameObject particle, Vector3 targetPos)
    {
        float threshold = 0.1f;
        while (particle != null && Vector3.Distance(particle.transform.position, targetPos) > threshold)
        {
            Vector3 direction = (targetPos - particle.transform.position).normalized;
            particle.transform.position += direction * particleSpeed * Time.deltaTime;
            yield return null;
        }

        if (particle != null)
            Destroy(particle);
    }

    private void moveStart(Vector3 dest)
    {
        agent.SetDestination(dest);

        if (moveMarkPrefab != null)
        {

            if (currentMoveMark != null)
                Destroy(currentMoveMark); // ���� ��Ŀ ����

            currentMoveMark = Instantiate(moveMarkPrefab, dest + Vector3.up * 0.1f, Quaternion.Euler(90, 0, 0));
        }
    }
    private void moving()
    {

        Vector3 dir = agent.desiredVelocity.normalized;

        transform.rotation = Quaternion.LookRotation(dir);
        transform.position += dir * 10 * Time.deltaTime;
    }
    private void moveStop()
    {
        agent.destination = gameObject.transform.position;
        if (currentMoveMark != null) Destroy(currentMoveMark);
    }
}
