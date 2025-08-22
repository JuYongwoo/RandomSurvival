public class Gate : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        hp = 20;
        power = 0;
        EXP = 0; // 게이트는 경험치를 주지 않음
    }
}
