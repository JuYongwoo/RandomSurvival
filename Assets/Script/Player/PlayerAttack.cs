using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격 설정")]
    public float particleSpeed = 5f;

    private Animator animator;

    private GameObject attackParticlePrefab;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        attackParticlePrefab = Resources.Load<GameObject>("Prefabs/AttackParticle");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            TryAttackTarget();
        }
    }

    private void TryAttackTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            IAttackable target = hit.collider.GetComponentInParent<IAttackable>();
            if (target != null)
            {
                animator.SetTrigger("Attack");

                GameObject particle = Instantiate(attackParticlePrefab, transform.position + Vector3.up * 1.2f, Quaternion.identity);
                particle.GetComponent<AttackParticle>().SetTarget(hit.collider.transform);
                StartCoroutine(MoveParticleToTarget(particle, hit.collider.transform.position));
            }
        }
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
