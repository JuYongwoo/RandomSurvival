using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Game/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public float BaseDamage;
    public float UpgradeDamageDelta;
    public float ProjectileSpeed;
    public float ReloadTime;
    public float Range;

}