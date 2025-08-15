using UnityEngine;


public class StatManager
{

    public WeaponDatabase WeaponStatDB { get; private set; }
    public PlayerDatabase PlayerStatDB { get; private set; }


    public void OnAwake() // SO파일 PlayerData를 로드하고 게임에 필요한 플레이어 베이스스탯을 가져온다.
    {

        // 무기 데이터 로드
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
        PlayerStatDB.Current.attackUpgrade * WeaponStatDB.GetWeaponInfo(PlayerStatDB.Current.CurrentWeapon).weaponUpgradeDMGDelta //무기 업그레이드 레벨 * 무기 업그레이드 데미지 델타
        + WeaponStatDB.GetWeaponInfo(PlayerStatDB.Current.CurrentWeapon).weaponDMG; // + 현재 무기 기본 데미지 = 현재 플레이어 공격력

    }
}
