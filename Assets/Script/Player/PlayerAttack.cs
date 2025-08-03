using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static Action moveStop;
    public float particleSpeed = 5f; //TODO basestat so 파일로 이동
    private Animator animator;
    private GameObject attackParticlePrefab;

    void Awake()
    {
        ManagerObject.input.EnemyMouseAction = TryAttackTarget;
        animator = GetComponentInChildren<Animator>();
        attackParticlePrefab = Resources.Load<GameObject>("Prefabs/AttackParticle");
    }


    private void TryAttackTarget(GameObject hit)
    {
        moveStop();//이동 멈춤
        gameObject.transform.LookAt(hit.transform.position); //상대 바라본다
        animator.SetTrigger("Attack"); //공격 모션
        GameObject particle = Instantiate(attackParticlePrefab, transform.position + Vector3.up * 1.2f, Quaternion.identity);
        particle.GetComponent<AttackParticle>().SetTarget(hit.transform);
        StartCoroutine(MoveParticleToTarget(particle, hit.transform.position));
    }

    private IEnumerator MoveParticleToTarget(GameObject particle, Vector3 targetPos)
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
