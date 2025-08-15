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
        Player.deltaHP = PlayerStatDB.Current.deltaHP;
        EnemyBase.deltaPlayerEXP = PlayerStatDB.Current.deltaEXP;
        StatPanel.getweaponInfo = (WeaponDatabase.Weapons weapon) => WeaponStatDB.GetWeaponInfo(weapon);
        AttackParticle.getCurrentPlayerDamage = () =>
        PlayerStatDB.Current.attackUpgrade * WeaponStatDB.GetWeaponInfo(PlayerStatDB.Current.CurrentWeapon).weaponUpgradeDMGDelta //���� ���׷��̵� ���� * ���� ���׷��̵� ������ ��Ÿ
        + WeaponStatDB.GetWeaponInfo(PlayerStatDB.Current.CurrentWeapon).weaponDMG; // + ���� ���� �⺻ ������ = ���� �÷��̾� ���ݷ�

    }
}
