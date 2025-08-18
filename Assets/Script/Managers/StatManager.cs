using System.Threading.Tasks;
using UnityEngine;
using static PlayerDatabase;

public class StatManager
{
    public WeaponDatabase WeaponStatDB { get; private set; }
    public PlayerDatabase PlayerStatDB { get; private set; }
    public bool IsReady { get; private set; }

    // ȣ��ο��� await �ʼ�
    public void OnAwake()
    {
        WeaponStatDB = new WeaponDatabase();
        PlayerStatDB = new PlayerDatabase();

        MapOtherActions();   // ��� DB �ε� �Ϸ� �� ���ε�
        IsReady = true;
    }

    private void MapOtherActions()
    {
        // �÷��̾�/�� ��ȣ�ۿ�
        Player.deltaHP = PlayerStatDB.deltaHP;
        EnemyBase.deltaPlayerEXP = PlayerStatDB.deltaEXP;

        // ���� ��ȸ
        StatPanel.getweaponInfo = w => WeaponStatDB.GetInfo(w);

        // ���� ���� ���� ����(�ߺ� ��ȸ ������ ���� �Լ�)
        WeaponDatabase.WeaponInfo Curr() => WeaponStatDB.GetInfo(PlayerStatDB.Current.CurrentWeapon);

        // PlayerStateMachine ������
        PlayerStateMachine.getPlayerWeaponProjectile = () => Curr().Projectile;
        PlayerStateMachine.getPlayerWeaponFireSound = () => Curr().FireSfx;
        PlayerStateMachine.getPlayerWeaponReloadTime = () => Curr().ReloadTime;
        PlayerStateMachine.getPlayerWeaponAttackRange = () => Curr().AttackRange;

        // ����ü/������
        AttackProjectile.getCurrentPlayerDamage = () =>
            PlayerStatDB.Current.attackUpgrade * Curr().UpgradeDMGDelta + Curr().BaseDMG;

        AttackProjectile.getProjectileSpeed = () => Curr().ProjectileSpeed;
    }
}
