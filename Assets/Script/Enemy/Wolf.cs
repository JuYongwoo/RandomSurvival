public class Wolf : EnemyBase
{

    protected override void Awake()
    {
        base.Awake();


        hp = 10;
        power = 1;
        EXP= 10;
    }
}
