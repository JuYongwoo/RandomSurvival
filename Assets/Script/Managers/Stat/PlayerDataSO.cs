using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Game/PlayerData")]
public class PlayerDataSO : ScriptableObject //기획에 따라 플레이어 시작 스탯 수정 필요 //deprecated: Sprint, Stamina //위 관련 스탯은 제거 예정
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

}