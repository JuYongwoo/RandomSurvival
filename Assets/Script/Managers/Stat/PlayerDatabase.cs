using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static AudioManager;
using static WeaponDatabase;
public class PlayerDatabase
{

    public PlayerCurrentStat Current; //계속 수치가 변해야하므로 readonly 사용 X

    public struct PlayerCurrentStat
    {
        public PlayerCurrentStat(float currentMaxHP, float currentHP, float currentMoveSpeed, int currentEXP, int currentMaxEXP, int currentHPUpgrade, int currentWeaponUpgrade, WeaponDatabase.Weapons currentWeapon)
        {
            this.currentMaxHP = currentMaxHP;
            this.currentHP = currentHP;
            this.currentMoveSpeed = currentMoveSpeed;
            this.currentEXP = currentEXP;
            this.currentMaxEXP = currentMaxEXP;
            this.currentHPUpgrade = currentHPUpgrade;
            this.currentWeaponUpgrade = currentWeaponUpgrade;
            this.currentWeapon = currentWeapon;
        }


        public float currentMaxHP;
        public float currentHP;
        public float currentMoveSpeed;
        public int currentEXP;
        public int currentMaxEXP;
        public WeaponDatabase.Weapons currentWeapon; // 현재 장착된 무기
        public int currentWeaponUpgrade; // 업그레이드 레벨
        public int currentHPUpgrade; // 업그레이드 레벨

    }


    public static Action<float, float> OnRefreshHPBar;
    public static Action<int, int> OnRefreshEXPUI;
    public static Action<WeaponDatabase.Weapons> OnChangeWeapon;

    public PlayerDatabase()
    {
        PlayerDataSO playerData = new PlayerDataSO();
        playerData = Addressables.LoadAssetAsync<PlayerDataSO>("PlayerDataSO").WaitForCompletion();

        Current = new PlayerCurrentStat(
            playerData.CurrentMaxHP,
            playerData.CurrentHP,
            playerData.CurrentMoveSpeed,
            playerData.CurrentEXP,
            playerData.CurrentMaxEXP,
            playerData.CurrentHPUpgrade,
            playerData.CurrentWeaponUpgrade,
            playerData.CurrentWeapon
            ); //player가 여러개가 되면 weapon처럼 맵으로 바꿔야

    }

    public void deltaHP(float delta)
    {
        Current.currentHP += delta;
        OnRefreshHPBar?.Invoke(Current.currentHP, Current.currentMaxHP);
        if (Current.currentHP <= 0f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }
    }
    public void deltaEXP(int delta)
    {
        Current.currentEXP += delta;
        OnRefreshEXPUI?.Invoke(Current.currentEXP, Current.currentMaxEXP);
    }
    public void setCurrentWeapon(WeaponDatabase.Weapons weapon)
    {
        Current.currentWeapon = weapon;
        OnChangeWeapon(Current.currentWeapon);
    }

}