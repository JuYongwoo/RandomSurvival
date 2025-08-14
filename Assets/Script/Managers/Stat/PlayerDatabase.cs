using System;
public class PlayerDatabase
{
    public struct PlayerBaseStat
    {
        public PlayerBaseStat(PlayerDataSO playerdataSO)
        {
            BaseMoveSpeed = playerdataSO.BaseMoveSpeed;
            SprintSpeed = playerdataSO.SprintSpeed;
            RunSpeed = playerdataSO.RunSpeed;
            RotationSpeed = playerdataSO.RotationSpeed;
            MaxStamina = playerdataSO.MaxStamina;
            StaminaRegenRate = playerdataSO.StaminaRegenRate;
            StaminaDrainRate = playerdataSO.StaminaDrainRate;
            SprintStaminaCost = playerdataSO.SprintStaminaCost;
            SprintDuration = playerdataSO.SprintDuration;
            SprintCooldown = playerdataSO.SprintCooldown;
            MaxHP = playerdataSO.MaxHP;
        }
        public float BaseMoveSpeed;
        public float SprintSpeed;
        public float RunSpeed;
        public float RotationSpeed;
        public float MaxStamina;
        public float StaminaRegenRate;
        public float StaminaDrainRate;
        public float SprintStaminaCost;
        public float SprintDuration;
        public float SprintCooldown;
        public float MaxHP;
    }


    public struct PlayerCurrentStat
    {
        public PlayerCurrentStat(float maxHP, float moveSp)
        {
            MaxHP = maxHP;
            HP = MaxHP;
            EXP = 0;
            attackUpgrade = 0;
            HPUpgrade = 0;
            CurrentWeapon = WeaponDatabase.Weapons.Hand; // 기본 무기는 맨손으로 설정
            moveSpeed = moveSp;
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

    public PlayerBaseStat Base;
    public PlayerCurrentStat Current;

    public PlayerDatabase(PlayerDataSO data)
    {
        Base = new PlayerBaseStat(data);
        Current = new PlayerCurrentStat(Base.MaxHP, Base.BaseMoveSpeed);
    }

}