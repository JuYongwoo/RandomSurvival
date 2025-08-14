using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase
{
    private readonly Dictionary<Weapons, WeaponInfo> weaponInfoMap;

    public struct WeaponInfo
    {
        public string weaponName;
        public Sprite weaponImg;
        public int weaponDMG;

        public WeaponInfo(string weaponName, Sprite weaponImg, int weaponDMG)
        {
            this.weaponName = weaponName;
            this.weaponImg = weaponImg;
            this.weaponDMG = weaponDMG;
        }
    }

    public WeaponDatabase(Dictionary<Weapons, Sprite> weaponImgMap)
    {
        weaponInfoMap = new Dictionary<Weapons, WeaponInfo>
        {
            { Weapons.Hand,  new WeaponInfo("맨손", weaponImgMap[Weapons.Hand],  10) },
            { Weapons.Bow,   new WeaponInfo("활",   weaponImgMap[Weapons.Bow],   15) },
            { Weapons.Magic, new WeaponInfo("마법", weaponImgMap[Weapons.Magic], 20) }
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