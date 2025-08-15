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
            CurrentWeapon = WeaponDatabase.Weapons.Hand; // �⺻ ����� �Ǽ����� ����
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
        public WeaponDatabase.Weapons CurrentWeapon; // ���� ������ ����
        public int attackUpgrade; // ���׷��̵� ����
        public int HPUpgrade; // ���׷��̵� ����
        public float moveSpeed;

        public static Action<float, float> OnRefreshHPBar;
        public static Action<int> OnRefreshEXPUI;
        public static Action<WeaponDatabase.Weapons> OnChangeWeapon;
    }

    public PlayerCurrentStat Current;

    public PlayerDatabase()
    {
        PlayerDataSO playerData = Util.LoadOneResource<PlayerDataSO>("GameData/Player/");
        Current = new PlayerCurrentStat(playerData.MaxHP, playerData.BaseMoveSpeed); //player�� �������� �Ǹ� weaponó�� ������ �ٲ��
    }

}