using System.Threading.Tasks;
using UnityEngine;
using static PlayerDatabase;

public class StatManager
{
    public WeaponDatabase WeaponStatDB { get; private set; }
    public PlayerDatabase PlayerStatDB { get; private set; }
    public bool IsReady { get; private set; }

    // 호출부에서 await 필수
    public void OnAwake()
    {
        WeaponStatDB = new WeaponDatabase();
        PlayerStatDB = new PlayerDatabase();

        MapOtherActions();   // 모든 DB 로드 완료 후 바인딩
        IsReady = true;
    }

    private void MapOtherActions()
    {
        // 플레이어/적 상호작용
        Player.deltaHP = PlayerStatDB.deltaHP;
        EnemyBase.deltaPlayerEXP = PlayerStatDB.deltaEXP;


        // 현재 무기 정보 접근(중복 조회 방지용 로컬 함수)
        WeaponDatabase.WeaponInfo Curr() => WeaponStatDB.GetInfo(PlayerStatDB.Current.currentWeapon);


        // 투사체 데미지 및 투사체 속도 설정
        AttackProjectile.getCurrentPlayerDamage = () =>
            PlayerStatDB.Current.currentWeaponUpgrade * Curr().UpgradeDMGDelta + Curr().BaseDMG;

        AttackProjectile.getProjectileSpeed = () => Curr().ProjectileSpeed;
    }
}
