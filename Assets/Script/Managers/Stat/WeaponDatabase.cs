using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase
{
    private readonly Dictionary<Weapons, WeaponInfo> weaponInfoMap;

    public enum Weapons
    {
        Hand,
        Bow,
        Magic
    }
    public struct WeaponInfo
    {
        public string weaponName;
        public Sprite weaponImg;
        public float weaponDMG;
        public float weaponUpgradeDMGDelta;
        public AudioClip weaponFireSound;
        public GameObject weaponProjectile;

        public WeaponInfo(string weaponName, WeaponDataSO weaponData, Sprite weaponImg, AudioClip weaponFireSound, GameObject weaponProjectile)
        {
            this.weaponName = weaponName;
            this.weaponImg = weaponImg;
            this.weaponDMG = weaponData.BaseDamage;
            this.weaponUpgradeDMGDelta = weaponData.UpgradeDamageDelta;
            this.weaponFireSound = weaponFireSound;
            this.weaponProjectile = weaponProjectile;
        }
    }

    public WeaponDatabase()
    {
        Dictionary<Weapons, WeaponDataSO> weaponTypeDataSOMap = Util.mapDictionaryWithLoad<Weapons, WeaponDataSO>("GameData/Weapon/Type"); //���� ���� �ε�
        Dictionary<Weapons, Sprite> weaponIconMap = Util.mapDictionaryWithLoad<Weapons, Sprite>("GameData/Weapon/Icon"); //���� �ؽ��� �ε�
        Dictionary<Weapons, AudioClip> weaponFireSoundMap = Util.mapDictionaryWithLoad<Weapons, AudioClip>("GameData/Weapon/FireSound"); //���� �Ҹ� �ε�
        Dictionary<Weapons, GameObject> weaponProjectileMap = Util.mapDictionaryWithLoad<Weapons, GameObject>("GameData/Weapon/Projectile"); //���� ����ü �ε�

        weaponInfoMap = new Dictionary<Weapons, WeaponInfo> //�������� �ϳ��� ����
        {
            { Weapons.Hand,  new WeaponInfo("�Ǽ�", weaponTypeDataSOMap[Weapons.Hand], weaponIconMap[Weapons.Hand], weaponFireSoundMap[Weapons.Hand], weaponProjectileMap[Weapons.Hand])},
            { Weapons.Bow,   new WeaponInfo("Ȱ",   weaponTypeDataSOMap[Weapons.Bow], weaponIconMap[Weapons.Bow], weaponFireSoundMap[Weapons.Bow], weaponProjectileMap[Weapons.Bow]) },
            { Weapons.Magic, new WeaponInfo("����", weaponTypeDataSOMap[Weapons.Magic], weaponIconMap[Weapons.Magic], weaponFireSoundMap[Weapons.Magic], weaponProjectileMap[Weapons.Magic]) }
        };
    }

    public WeaponInfo GetWeaponInfo(Weapons weapon) //map���ν� �پ��� ���Ⱑ �ֱ� ������ ���� ������ �޴� �Լ�
    {
        if (weaponInfoMap.TryGetValue(weapon, out var info))
            return info;

        Debug.LogWarning($"Weapon info not found for {weapon}");
        return default;
    }
}