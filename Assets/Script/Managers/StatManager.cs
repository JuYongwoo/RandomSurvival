using UnityEngine;


public class StatManager
{

    public WeaponDatabase WeaponStatDB { get; private set; }
    public PlayerDatabase PlayerStatDB { get; private set; }


    public void OnAwake()
    {

        WeaponStatDB = new WeaponDatabase(); //무기 데이터베이스 초기화 //초기화 함수에서 WeaponDataSO를 불러와서 무기 정보를 초기화한다.
        PlayerStatDB = new PlayerDatabase(); //플레이어 데이터베이스 초기화 //초기화 함수에서 PlayerDataSO를 불러와서 플레이어 정보를 초기화한다.

        mapOtherActions();
    }



    private void mapOtherActions()
    {
        // 플레이어의 현재 HP, EXP, 무기 정보 등을 다른 클래스에서 사용할 수 있도록 매핑
        // 더이상 액션에 추가하거나 변경할 필요가 없는 정적 매핑들
        Player.deltaHP = PlayerStatDB.deltaHP;
        EnemyBase.deltaPlayerEXP = PlayerStatDB.deltaEXP;
        StatPanel.getweaponInfo = (WeaponDatabase.Weapons weapon) => WeaponStatDB.GetWeaponInfo(weapon);
        PlayerStateMachine.getPlayerWeaponProjectile = () => WeaponStatDB.GetWeaponInfo(PlayerStatDB.Current.CurrentWeapon).weaponProjectile; //현재 플레이어가 장착한 무기의 투사체를 반환
        PlayerStateMachine.getPlayerWeaponFireSound = () => WeaponStatDB.GetWeaponInfo(PlayerStatDB.Current.CurrentWeapon).weaponFireSound; //현재 플레이어가 장착한 무기의 발사 소리를 반환
        AttackParticle.getCurrentPlayerDamage = () =>
        PlayerStatDB.Current.attackUpgrade * WeaponStatDB.GetWeaponInfo(PlayerStatDB.Current.CurrentWeapon).weaponUpgradeDMGDelta //무기 업그레이드 레벨 * 무기 업그레이드 데미지 델타
        + WeaponStatDB.GetWeaponInfo(PlayerStatDB.Current.CurrentWeapon).weaponDMG; // + 현재 무기 기본 데미지 = 현재 플레이어 공격력

    }
}
