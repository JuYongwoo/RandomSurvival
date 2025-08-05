using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected float hp;
    protected float power;
    public static Action<float> hitplayer;


    public virtual void TakeDamage(float getDamage)
    {
        hp -= getDamage;
        if(hp <= 0) Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            hitplayer?.Invoke(power);
        }
    }

}
