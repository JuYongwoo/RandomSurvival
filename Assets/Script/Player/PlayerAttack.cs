using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static Action moveStop;
    public float particleSpeed = 5f; //TODO basestat so 파일로 이동
    private GameObject attackParticlePrefab;
    public static Func<PlayerState> playerstate;
    public static Action<PlayerState> setPlayerstate;
    private GameObject currentTarget;

    void Awake()
    {
        attackParticlePrefab = Resources.Load<GameObject>("Prefabs/AttackParticle");
        InputManager.setAttackTarget = (go) => {
            currentTarget = go;
        };
    }

    private void Update()
    {
        var state = playerstate();

        if (state == PlayerState.AttackStart)
        {
            if(currentTarget == null)
            {
                setPlayerstate(PlayerState.Idle);
                return;
            }
            else
            {
                moveStop();
                attack();
                setPlayerstate(PlayerState.Attacking);
            }
        }
        else if(state == PlayerState.Attacking)
        {
            if(currentTarget == null)
            {
                setPlayerstate(PlayerState.Idle);
            }
        }
        else if(state == PlayerState.AttackMove_Moving)
        {
            List<GameObject> detectedEnemies = detectEnemies();
            if (detectedEnemies.Count != 0)
            {
                currentTarget = detectedEnemies[0];
                setPlayerstate(PlayerState.AttackMove_AttackStart);
            }

        }

        else if (state == PlayerState.AttackMove_AttackStart)
        {
            if (currentTarget == null)
            {
                setPlayerstate(PlayerState.AttackMove_MoveStart);
                return;
            }
            else
            {
                attack();
                setPlayerstate(PlayerState.AttackMove_Attacking);
            }
        }
        else if (state == PlayerState.AttackMove_Attacking)
        {
            if (currentTarget == null)
            {
                setPlayerstate(PlayerState.AttackMove_MoveStart);
            }
        }
    }

    private void attack()
    {

        gameObject.transform.LookAt(currentTarget.transform.position); //상대 바라본다
        GameObject particle = Instantiate(attackParticlePrefab, transform.position + Vector3.up * 1.2f, Quaternion.identity);
        particle.GetComponent<AttackParticle>().SetTarget(currentTarget.transform);
        StartCoroutine(moveParticleToTarget(particle, currentTarget.transform.position));
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
}
