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
        weaponImgMap = Util.mapDictionaryWithLoad<WeaponDatabase.Weapons, Sprite>("Graphics/Textures"); //���� �ؽ��� �ε�
        weaponSOMap = Util.mapDictionaryWithLoad<Weapons, WeaponDataSO>("GameData/Weapon"); //���� ���� �ε�
        weaponInfoMap = new Dictionary<Weapons, WeaponInfo> //�������� �ϳ��� ����
        {
            { Weapons.Hand,  new WeaponInfo("�Ǽ�", weaponImgMap[Weapons.Hand],  weaponSOMap[Weapons.Hand].BaseDamage, weaponSOMap[Weapons.Hand].UpgradeDamageDelta) },
            { Weapons.Bow,   new WeaponInfo("Ȱ",   weaponImgMap[Weapons.Bow],   weaponSOMap[Weapons.Bow].BaseDamage, weaponSOMap[Weapons.Bow].UpgradeDamageDelta) },
            { Weapons.Magic, new WeaponInfo("����", weaponImgMap[Weapons.Magic], weaponSOMap[Weapons.Magic].BaseDamage, weaponSOMap[Weapons.Magic].UpgradeDamageDelta) }
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