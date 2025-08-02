using UnityEngine;

public class AttackParticle : MonoBehaviour
{
    public float damage = 100f;
    public float speed = 5f;
    private Transform target;

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            IAttackable attackable = target.GetComponentInParent<IAttackable>();
            if (attackable != null)
                attackable.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
