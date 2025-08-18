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

        // 무기 조회
        StatPanel.getweaponInfo = w => WeaponStatDB.GetInfo(w);

        // 현재 무기 정보 접근(중복 조회 방지용 로컬 함수)
        WeaponDatabase.WeaponInfo Curr() => WeaponStatDB.GetInfo(PlayerStatDB.Current.CurrentWeapon);

        // PlayerStateMachine 의존성
        PlayerStateMachine.getPlayerWeaponProjectile = () => Curr().Projectile;
        PlayerStateMachine.getPlayerWeaponFireSound = () => Curr().FireSfx;
        PlayerStateMachine.getPlayerWeaponReloadTime = () => Curr().ReloadTime;
        PlayerStateMachine.getPlayerWeaponAttackRange = () => Curr().AttackRange;

        // 투사체/데미지
        AttackProjectile.getCurrentPlayerDamage = () =>
            PlayerStatDB.Current.attackUpgrade * Curr().UpgradeDMGDelta + Curr().BaseDMG;

        AttackProjectile.getProjectileSpeed = () => Curr().ProjectileSpeed;
    }
}
