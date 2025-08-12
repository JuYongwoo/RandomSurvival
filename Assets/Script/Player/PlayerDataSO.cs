using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Game/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public float BaseMoveSpeed;
    public float SprintSpeed;
    public float RotationSpeed;
    public float RunSpeed;
    public float MaxStamina;
    public float StaminaRegenRate;
    public float StaminaDrainRate;
    public float SprintStaminaCost;
    public float SprintDuration;
    public float SprintCooldown;
    public float MaxHP;
    public float HitDamage;

}