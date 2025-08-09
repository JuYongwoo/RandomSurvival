using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected float hp;
    [HideInInspector]
    public float power;
    public static Action<float> hitplayer;
    protected Transform player;
    private float cullDistance = 30f;
    private Renderer[] renderers;
    static public Action<int> deltaEnemyCount;

    protected virtual void Awake()
    {
        if (GetType() != typeof(Gate))
            deltaEnemyCount(+1);
    }

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(player.name);
        renderers = GetComponentsInChildren<Renderer>();

    }

    protected virtual void Update()
    {
        float distSqr = (player.position - transform.position).sqrMagnitude;
        bool visible = distSqr < cullDistance * cullDistance;

        foreach (var r in renderers)
            r.enabled = visible;
    }


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

    protected virtual void OnDestroy()
    {
        if(GetType() != typeof(Gate))
        deltaEnemyCount(-1);
    }
}
