using UnityEngine.AI;

public class Wolf : EnemyBase
{

    protected override void Awake()
    {
        base.Awake();


        hp = 100;
        power = 0;
    }
}
