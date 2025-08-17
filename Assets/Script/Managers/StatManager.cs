using UnityEngine;


public class StatManager
{

    public WeaponDatabase WeaponStatDB { get; private set; }
    public PlayerDatabase PlayerStatDB { get; private set; }


    public void OnAwake()
    {

        WeaponStatDB = new WeaponDatabase(); //���� �����ͺ��̽� �ʱ�ȭ //�ʱ�ȭ �Լ����� WeaponDataSO�� �ҷ��ͼ� ���� ������ �ʱ�ȭ�Ѵ�.
        PlayerStatDB = new PlayerDatabase(); //�÷��̾� �����ͺ��̽� �ʱ�ȭ //�ʱ�ȭ �Լ����� PlayerDataSO�� �ҷ��ͼ� �÷��̾� ������ �ʱ�ȭ�Ѵ�.

        mapOtherActions();
    }



    private void mapOtherActions()
    {
        // �÷��̾��� ���� HP, EXP, ���� ���� ���� �ٸ� Ŭ�������� ����� �� �ֵ��� ����
        // ���̻� �׼ǿ� �߰��ϰų� ������ �ʿ䰡 ���� ���� ���ε�
        Player.deltaHP = PlayerStatDB.deltaHP;
        EnemyBase.deltaPlayerEXP = PlayerStatDB.deltaEXP;
        StatPanel.getweaponInfo = (WeaponDatabase.Weapons weapon) => WeaponStatDB.GetInfo(weapon);
        PlayerStateMachine.getPlayerWeaponProjectile = () => WeaponStatDB.GetInfo(PlayerStatDB.Current.CurrentWeapon).Projectile; //���� �÷��̾ ������ ������ ����ü�� ��ȯ
        PlayerStateMachine.getPlayerWeaponFireSound = () => WeaponStatDB.GetInfo(PlayerStatDB.Current.CurrentWeapon).FireSfx; //���� �÷��̾ ������ ������ �߻� �Ҹ��� ��ȯ
        AttackParticle.getCurrentPlayerDamage = () =>
        PlayerStatDB.Current.attackUpgrade * WeaponStatDB.GetInfo(PlayerStatDB.Current.CurrentWeapon).UpgradeDMGDelta //���� ���׷��̵� ���� * ���� ���׷��̵� ������ ��Ÿ
        + WeaponStatDB.GetInfo(PlayerStatDB.Current.CurrentWeapon).BaseDMG; // + ���� ���� �⺻ ������ = ���� �÷��̾� ���ݷ�

    }
}
