using System;
using UnityEngine;
public class PlayerBaseStat
{
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
    public float HitDamage;
}

    public class PlayerCurrentStat
    {
        public float HP;
        public float Stamina;
        public int EXP;
}
public class PlayerStatObject
{

    public PlayerBaseStat playerBaseStat = new PlayerBaseStat();


    public PlayerCurrentStat playerCurrentStat = new PlayerCurrentStat();

    public static Action<float> OnRefreshHPBar;
    public static Action<int> OnRefreshEXPUI;

    public void OnAwake() // SO���� PlayerData�� �ε��ϰ� ���ӿ� �ʿ��� �÷��̾� ���̽������� �����´�.
    {
        PlayerData pd = Resources.Load<PlayerData>("GameData/PlayerData");

        playerBaseStat.BaseMoveSpeed = pd.BaseMoveSpeed;
        playerBaseStat.SprintSpeed = pd.SprintSpeed;
        playerBaseStat.RotationSpeed = pd.RotationSpeed;
        playerBaseStat.RunSpeed = pd.RunSpeed;
        playerBaseStat.MaxStamina = pd.MaxStamina;
        playerBaseStat.StaminaRegenRate = pd.StaminaRegenRate;
        playerBaseStat.StaminaDrainRate = pd.StaminaDrainRate;
        playerBaseStat.SprintStaminaCost = pd.SprintStaminaCost;
        playerBaseStat.SprintDuration = pd.SprintDuration;
        playerBaseStat.SprintCooldown = pd.SprintCooldown;
        playerBaseStat.MaxHP = pd.MaxHP;
        playerBaseStat.HitDamage = pd.HitDamage;

        playerCurrentStat.HP = playerBaseStat.MaxHP;
        playerCurrentStat.Stamina = playerBaseStat.MaxStamina;


        Player.deltaHP = deltaHP;
        EnemyBase.deltaPlayerEXP = deltaEXP;
    }

    public void deltaHP(float delta)
    {
        playerCurrentStat.HP += delta;
        OnRefreshHPBar?.Invoke(playerCurrentStat.HP);
        if (playerCurrentStat.HP <= 0f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
        }
    }
    public void deltaEXP(int delta)
    {
        playerCurrentStat.EXP += delta;
        OnRefreshEXPUI?.Invoke(playerCurrentStat.EXP);
    }
}
