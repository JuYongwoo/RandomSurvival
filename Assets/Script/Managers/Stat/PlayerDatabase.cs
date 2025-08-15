using System;
using System.Collections.Generic;
public class PlayerDatabase
{


    public struct PlayerCurrentStat
    {
        public PlayerCurrentStat(float maxHP, float moveSp)
        {
            MaxHP = maxHP;
            moveSpeed = moveSp;
            HP = MaxHP;
            EXP = 0;
            attackUpgrade = 0;
            HPUpgrade = 0;
            CurrentWeapon = WeaponDatabase.Weapons.Hand; // 기본 무기는 맨손으로 설정
        }
        public void deltaHP(float delta)
        {
            HP += delta;
            OnRefreshHPBar?.Invoke(HP, MaxHP);
            if (HP <= 0f)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
            }
        }
        public void deltaEXP(int delta)
        {
            EXP += delta;
            OnRefreshEXPUI?.Invoke(EXP);
        }
        public void setCurrentWeapon(WeaponDatabase.Weapons weapon)
        {
            CurrentWeapon = weapon;
            OnChangeWeapon(CurrentWeapon);
        }

        public float MaxHP;
        public float HP;
        public int EXP;
        public WeaponDatabase.Weapons CurrentWeapon; // 현재 장착된 무기
        public int attackUpgrade; // 업그레이드 레벨
        public int HPUpgrade; // 업그레이드 레벨
        public float moveSpeed;

        public static Action<float, float> OnRefreshHPBar;
        public static Action<int> OnRefreshEXPUI;
        public static Action<WeaponDatabase.Weapons> OnChangeWeapon;
    }

    public PlayerCurrentStat Current;

    public PlayerDatabase()
    {
        PlayerDataSO playerData = Util.LoadOneResource<PlayerDataSO>("GameData/Player/");
        Current = new PlayerCurrentStat(playerData.MaxHP, playerData.BaseMoveSpeed); //player가 여러개가 되면 weapon처럼 맵으로 바꿔야
    }

}