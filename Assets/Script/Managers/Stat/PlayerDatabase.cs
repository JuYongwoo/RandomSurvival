using System;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDatabase
{

    public PlayerCurrentStat Current; //계속 수치가 변해야하므로 readonly 사용 X

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


        public float MaxHP;
        public float HP;
        public int EXP;
        public WeaponDatabase.Weapons CurrentWeapon; // 현재 장착된 무기
        public int attackUpgrade; // 업그레이드 레벨
        public int HPUpgrade; // 업그레이드 레벨
        public float moveSpeed;

    }


    public static Action<float, float> OnRefreshHPBar;
    public static Action<int> OnRefreshEXPUI;
    public static Action<WeaponDatabase.Weapons> OnChangeWeapon;

    public PlayerDatabase()
    {
        PlayerDataSO playerData = Util.LoadOneResource<PlayerDataSO>("Player");
        Current = new PlayerCurrentStat(playerData.MaxHP, playerData.BaseMoveSpeed); //player가 여러개가 되면 weapon처럼 맵으로 바꿔야
    }

    public void deltaHP(float delta)
    {
        Current.HP += delta;
        OnRefreshHPBar?.Invoke(Current.HP, Current.MaxHP);
        if (Current.HP <= 0f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }
    }
    public void deltaEXP(int delta)
    {
        Current.EXP += delta;
        OnRefreshEXPUI?.Invoke(Current.EXP);
    }
    public void setCurrentWeapon(WeaponDatabase.Weapons weapon)
    {
        Current.CurrentWeapon = weapon;
        OnChangeWeapon(Current.CurrentWeapon);
    }

}