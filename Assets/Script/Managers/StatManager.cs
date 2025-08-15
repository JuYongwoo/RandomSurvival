using UnityEngine;


public class StatManager
{

    public WeaponDatabase WeaponStatDB { get; private set; }
    public PlayerDatabase PlayerStatDB { get; private set; }


    public void OnAwake() // SO���� PlayerData�� �ε��ϰ� ���ӿ� �ʿ��� �÷��̾� ���̽������� �����´�.
    {

        // ���� ������ �ε�
        WeaponStatDB = new WeaponDatabase();
        PlayerStatDB = new PlayerDatabase();

        mapOtherActions();
    }



    private void mapOtherActions()
    {

        Player.deltaHP = PlayerStatDB.Current.deltaHP;
        EnemyBase.deltaPlayerEXP = PlayerStatDB.Current.deltaEXP;
        StatPanel.getweaponInfo = (WeaponDatabase.Weapons weapon) => WeaponStatDB.GetWeaponInfo(weapon);
        AttackParticle.getCurrentPlayerDamage = () =>
        PlayerStatDB.Current.attackUpgrade * WeaponStatDB.GetWeaponInfo(PlayerStatDB.Current.CurrentWeapon).weaponUpgradeDMGDelta //���� ���׷��̵� ���� * ���� ���׷��̵� ������ ��Ÿ
        + WeaponStatDB.GetWeaponInfo(PlayerStatDB.Current.CurrentWeapon).weaponDMG; // + ���� ���� �⺻ ������ = ���� �÷��̾� ���ݷ�

    }
}
