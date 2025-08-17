using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase
{
    public enum Weapons { Hand, Bow, Magic }

    private readonly Dictionary<Weapons, WeaponInfo> _map;

    public readonly struct WeaponInfo
    {
        public string Name { get; }
        public Sprite Icon { get; }
        public float BaseDMG { get; }
        public float UpgradeDMGDelta { get; }
        public AudioClip FireSfx { get; }
        public GameObject Projectile { get; }

        public WeaponInfo(string name, WeaponDataSO so, Sprite icon, AudioClip sfx, GameObject proj)
        {
            Name = name;
            Icon = icon;
            BaseDMG = so.BaseDamage;
            UpgradeDMGDelta = so.UpgradeDamageDelta;
            FireSfx = sfx;
            Projectile = proj;
        }
    }

    public WeaponDatabase()
    {
        var soMap = Util.MapEnumToChildFile<Weapons, WeaponDataSO>("Weapon", "Data");
        var iconMap = Util.MapEnumToChildFile<Weapons, Sprite>("Weapon", "Icon");
        var sfxMap = Util.MapEnumToChildFile<Weapons, AudioClip>("Weapon", "FireSound");
        var projMap = Util.MapEnumToChildFile<Weapons, GameObject>("Weapon", "Projectile");

        _map = new Dictionary<Weapons, WeaponInfo>
        {
            { Weapons.Hand,  new WeaponInfo("맨손", soMap[Weapons.Hand],  iconMap[Weapons.Hand],  sfxMap[Weapons.Hand],  projMap[Weapons.Hand]) },
            { Weapons.Bow,   new WeaponInfo("활",   soMap[Weapons.Bow],   iconMap[Weapons.Bow],   sfxMap[Weapons.Bow],   projMap[Weapons.Bow]) },
            { Weapons.Magic, new WeaponInfo("마법", soMap[Weapons.Magic], iconMap[Weapons.Magic], sfxMap[Weapons.Magic], projMap[Weapons.Magic]) },
        };
    }

    public bool TryGetInfo(Weapons w, out WeaponInfo info) => _map.TryGetValue(w, out info);
    public WeaponInfo GetInfo(Weapons w) => _map[w];
}
