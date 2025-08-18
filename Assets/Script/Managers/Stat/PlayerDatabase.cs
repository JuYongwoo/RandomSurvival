using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static AudioManager;
using static WeaponDatabase;
public class PlayerDatabase
{

    public PlayerCurrentStat Current; //��� ��ġ�� ���ؾ��ϹǷ� readonly ��� X

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
            CurrentWeapon = WeaponDatabase.Weapons.Hand;
        }


        public float MaxHP;
        public float HP;
        public int EXP;
        public WeaponDatabase.Weapons CurrentWeapon; // ���� ������ ����
        public int attackUpgrade; // ���׷��̵� ����
        public int HPUpgrade; // ���׷��̵� ����
        public float moveSpeed;

    }


    public static Action<float, float> OnRefreshHPBar;
    public static Action<int> OnRefreshEXPUI;
    public static Action<WeaponDatabase.Weapons> OnChangeWeapon;

    public PlayerDatabase()
    {
        PlayerDataSO playerData = new PlayerDataSO();
        playerData = Addressables.LoadAssetAsync<PlayerDataSO>("PlayerDataSO").WaitForCompletion();

        Current = new PlayerCurrentStat(playerData.MaxHP, playerData.BaseMoveSpeed); //player�� �������� �Ǹ� weaponó�� ������ �ٲ��

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