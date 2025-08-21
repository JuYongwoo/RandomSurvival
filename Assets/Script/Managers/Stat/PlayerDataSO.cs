using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Game/PlayerData")]
public class PlayerDataSO : ScriptableObject //��ȹ�� ���� �÷��̾� ���� ���� ���� �ʿ�
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