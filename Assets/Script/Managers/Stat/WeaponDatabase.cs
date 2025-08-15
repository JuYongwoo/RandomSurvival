using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase
{
    private readonly Dictionary<Weapons, WeaponInfo> weaponInfoMap;
    private readonly Dictionary<Weapons, WeaponDataSO> weaponSOMap;
    private readonly Dictionary<Weapons, Sprite> weaponImgMap;


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

        public WeaponInfo(string weaponName, Sprite weaponImg, float weaponDMG, float weaponUpgradeDMGDelta)
        {
            this.weaponName = weaponName;
            this.weaponImg = weaponImg;
            this.weaponDMG = weaponDMG;
            this.weaponUpgradeDMGDelta = weaponUpgradeDMGDelta;
        }
    }

    public WeaponDatabase()
    {
        weaponImgMap = Util.mapDictionaryWithLoad<WeaponDatabase.Weapons, Sprite>("Graphics/Textures"); //무기 텍스쳐 로드
        weaponSOMap = Util.mapDictionaryWithLoad<Weapons, WeaponDataSO>("GameData/Weapon"); //무기 스탯 로드
        weaponInfoMap = new Dictionary<Weapons, WeaponInfo> //정보들을 하나로 통일
        {
            { Weapons.Hand,  new WeaponInfo("맨손", weaponImgMap[Weapons.Hand],  weaponSOMap[Weapons.Hand].BaseDamage, weaponSOMap[Weapons.Hand].UpgradeDamageDelta) },
            { Weapons.Bow,   new WeaponInfo("활",   weaponImgMap[Weapons.Bow],   weaponSOMap[Weapons.Bow].BaseDamage, weaponSOMap[Weapons.Bow].UpgradeDamageDelta) },
            { Weapons.Magic, new WeaponInfo("마법", weaponImgMap[Weapons.Magic], weaponSOMap[Weapons.Magic].BaseDamage, weaponSOMap[Weapons.Magic].UpgradeDamageDelta) }
        };
    }

    public WeaponInfo GetWeaponInfo(Weapons weapon)
    {
        if (weaponInfoMap.TryGetValue(weapon, out var info))
            return info;

        Debug.LogWarning($"Weapon info not found for {weapon}");
        return default;
    }
}