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
        Dictionary<Weapons, WeaponDataSO> weaponTypeDataSOMap = Util.mapDictionaryWithLoad<Weapons, WeaponDataSO>("GameData/Weapon/Type"); //무기 스탯 로드
        Dictionary<Weapons, Sprite> weaponIconMap = Util.mapDictionaryWithLoad<Weapons, Sprite>("GameData/Weapon/Icon"); //무기 텍스쳐 로드
        Dictionary<Weapons, AudioClip> weaponFireSoundMap = Util.mapDictionaryWithLoad<Weapons, AudioClip>("GameData/Weapon/FireSound"); //무기 소리 로드
        Dictionary<Weapons, GameObject> weaponProjectileMap = Util.mapDictionaryWithLoad<Weapons, GameObject>("GameData/Weapon/Projectile"); //무기 투사체 로드

        weaponInfoMap = new Dictionary<Weapons, WeaponInfo> //정보들을 하나로 통일
        {
            { Weapons.Hand,  new WeaponInfo("맨손", weaponTypeDataSOMap[Weapons.Hand], weaponIconMap[Weapons.Hand], weaponFireSoundMap[Weapons.Hand], weaponProjectileMap[Weapons.Hand])},
            { Weapons.Bow,   new WeaponInfo("활",   weaponTypeDataSOMap[Weapons.Bow], weaponIconMap[Weapons.Bow], weaponFireSoundMap[Weapons.Bow], weaponProjectileMap[Weapons.Bow]) },
            { Weapons.Magic, new WeaponInfo("마법", weaponTypeDataSOMap[Weapons.Magic], weaponIconMap[Weapons.Magic], weaponFireSoundMap[Weapons.Magic], weaponProjectileMap[Weapons.Magic]) }
        };
    }

    public WeaponInfo GetWeaponInfo(Weapons weapon) //map으로써 다양한 무기가 있기 때문에 무기 정보를 받는 함수
    {
        if (weaponInfoMap.TryGetValue(weapon, out var info))
            return info;

        Debug.LogWarning($"Weapon info not found for {weapon}");
        return default;
    }
}