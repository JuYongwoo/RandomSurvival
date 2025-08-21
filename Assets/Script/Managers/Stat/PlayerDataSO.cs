using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Game/PlayerData")]
public class PlayerDataSO : ScriptableObject //기획에 따라 플레이어 시작 스탯 수정 필요
{
    public float CurrentMoveSpeed;
    public float CurrentMaxHP;
    public float CurrentHP;
    public int CurrentEXP;
    public int CurrentMaxEXP;
    public int CurrentHPUpgrade;
    public int CurrentWeaponUpgrade;
    public WeaponDatabase.Weapons CurrentWeapon;

}