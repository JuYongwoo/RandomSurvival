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
    }
public class PlayerStatObject
{

    public PlayerBaseStat playerBaseStat = new PlayerBaseStat();


    public PlayerCurrentStat playerCurrentStat = new PlayerCurrentStat();


    public void OnAwake() // SO파일 PlayerData를 로드하고 게임에 필요한 플레이어 베이스스탯을 가져온다.
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
    }
}
