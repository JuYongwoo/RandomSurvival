using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class WeaponDatabase
{
    public enum Weapons { Hand, Bow, Magic }

    private Dictionary<Weapons, WeaponInfo> _map = new Dictionary<Weapons, WeaponInfo>();

    public readonly struct WeaponInfo
    {
        public string Name { get; }
        public float BaseDMG { get; }
        public float UpgradeDMGDelta { get; }
        public float ProjectileSpeed { get; }
        public float ReloadTime { get; }
        public float AttackRange { get; }
        public Sprite Icon { get; }
        public AudioClip FireSfx { get; }
        public GameObject Projectile { get; }

        public WeaponInfo(string name, WeaponDataSO so, Sprite icon, AudioClip sfx, GameObject proj)
        {
            Name = name;
            BaseDMG = so.BaseDamage;
            UpgradeDMGDelta = so.UpgradeDamageDelta;
            ProjectileSpeed = so.ProjectileSpeed;
            ReloadTime = so.ReloadTime;
            AttackRange = so.Range;
            Icon = icon;
            FireSfx = sfx;
            Projectile = proj;
        }
    }

    public WeaponDatabase()
    {
        var soMap = Util.MapEnumToAddressables<Weapons, WeaponDataSO>("WeaponDataSO"); //라벨 통해서 맵 등록
        var iconMap = Util.MapEnumToAddressables<Weapons, Sprite>("WeaponIcon");
        var sfxMap = Util.MapEnumToAddressables<Weapons, AudioClip>("WeaponFireSound");
        var projMap = Util.MapEnumToAddressables<Weapons, GameObject>("WeaponProjectile");


        _map = new Dictionary<Weapons, WeaponInfo>
        {
            { Weapons.Hand,  new WeaponInfo("맨손", soMap[Weapons.Hand],  iconMap[Weapons.Hand],  sfxMap[Weapons.Hand],  projMap[Weapons.Hand]) },
            { Weapons.Bow,   new WeaponInfo("활",   soMap[Weapons.Bow],   iconMap[Weapons.Bow],   sfxMap[Weapons.Bow],   projMap[Weapons.Bow]) },
            { Weapons.Magic, new WeaponInfo("마법", soMap[Weapons.Magic], iconMap[Weapons.Magic], sfxMap[Weapons.Magic], projMap[Weapons.Magic]) },
        };
    }

    private Dictionary<TEnum, TObject> BuildDict<TEnum, TObject>(IList<TObject> list)
    where TEnum : Enum
    where TObject : UnityEngine.Object
    {
        Dictionary<TEnum, TObject> dict = new Dictionary<TEnum, TObject>();
        foreach (TEnum e in Enum.GetValues(typeof(TEnum)))
        {
            string enumName = e.ToString();
            foreach (var obj in list)
            {
                if (obj != null && obj.name.Equals(enumName, StringComparison.OrdinalIgnoreCase))
                {
                    dict[e] = obj;
                    break;
                }
            }
        }
        return dict;
    }
    public bool TryGetInfo(Weapons w, out WeaponInfo info) => _map.TryGetValue(w, out info);
    public WeaponInfo GetInfo(Weapons w) => _map[w];
}
