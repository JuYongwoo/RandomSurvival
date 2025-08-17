using System;
using UnityEngine;

public class AttackProjectile : MonoBehaviour
{
    public static Func<float> getCurrentPlayerDamage;
    public static Func<float> getProjectileSpeed;
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
        transform.position += direction * getProjectileSpeed() * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            EnemyBase attackable = target.GetComponentInParent<EnemyBase>();
            if (attackable != null)
                attackable.TakeDamage(getCurrentPlayerDamage());

            Destroy(gameObject);
        }
    }
}
