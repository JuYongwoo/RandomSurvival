using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Game/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public float BaseDamage;
    public float UpgradeDamageDelta;
    public float FireRate;
    public float ReloadTime;
    public float Range;
    public float BulletSpeed;
    public AudioClip FireSound;
    public GameObject EffectPrefab;
    // Add any additional weapon properties as needed

}