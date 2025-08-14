using System;
using System.Collections.Generic;
using UnityEngine;

public enum Weapons
{
    Hand,
    Bow,
    Magic
}
public class StatObject
{

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
    public struct PlayerBaseStat
    {
        public float BaseMoveSpeed;
        public float SprintSpeed;
        public float RunSpeed;
        public float RotationSpeed;
        public float MaxStamina;
        public float StaminaRegenRate;
        public float StaminaDrainRate;
        public float SprintStaminaCost;
        public float SprintDuration;
        public float SprintCooldown;
        public float MaxHP;
        public float HitDamage;
    }

    public PlayerBaseStat playerBaseStat = new PlayerBaseStat();
    public struct PlayerCurrentStat
    {
        public void Reset()
        {
            MaxHP = 100f;
            HP = 100f;
            EXP = 0;
            attackUpgrade = 0;
            HPUpgrade = 0;
            CurrentWeapon = Weapons.Hand; // �⺻ ����� �Ǽ����� ����
            OnRefreshHPBar?.Invoke(HP);
            OnRefreshEXPUI?.Invoke(EXP);
        }
        public void deltaHP(float delta)
        {
            HP += delta;
            OnRefreshHPBar?.Invoke(HP);
            if (HP <= 0f)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
            }
        }
        public void deltaEXP(int delta)
        {
            EXP += delta;
            OnRefreshEXPUI?.Invoke(EXP);
        }
        public void setCurrentWeapon(Weapons weapon)
        {
            CurrentWeapon = weapon;
        }

        public float MaxHP;
        public float HP;
        public int EXP;
        public Weapons CurrentWeapon; // ���� ������ ����
        public int attackUpgrade; // ���׷��̵� ����
        public int HPUpgrade; // ���׷��̵� ����

        public static Action<float> OnRefreshHPBar;
        public static Action<int> OnRefreshEXPUI;
    }

    public PlayerCurrentStat playerCurrentStat = new PlayerCurrentStat();

    private Dictionary<Weapons, WeaponInfo> weaponInfoMap;
    private Dictionary<Weapons, Sprite> weaponImgMap;


    public void OnAwake() // SO���� PlayerData�� �ε��ϰ� ���ӿ� �ʿ��� �÷��̾� ���̽������� �����´�.
    {
        weaponImgMap = Util.mapDictionaryWithLoad<Weapons, Sprite>("Graphics/Textures");
        weaponInfoMap = new Dictionary<Weapons, WeaponInfo>
        {
            { Weapons.Hand,  new WeaponInfo( "�Ǽ�", weaponImgMap[Weapons.Hand],  10) },
            { Weapons.Bow,   new WeaponInfo("Ȱ",   weaponImgMap[Weapons.Bow],   15) },
            { Weapons.Magic, new WeaponInfo("����", weaponImgMap[Weapons.Magic], 20) },
        };


        loadPlayerData();
        playerCurrentStat.Reset();
        mapOtherActions();
    }


    public void loadPlayerData()
    {
        PlayerDataSO[] playerDatas = Resources.LoadAll<PlayerDataSO>("GameData/Player/");
        if (playerDatas.Length != 1)
        {
            Debug.LogError("PlayerDataSO not found in Resources/GameData/Player");
            return;
        }

        PlayerDataSO playerData = playerDatas[0];

        playerBaseStat.BaseMoveSpeed = playerData.BaseMoveSpeed;
        playerBaseStat.SprintSpeed = playerData.SprintSpeed;
        playerBaseStat.RotationSpeed = playerData.RotationSpeed;
        playerBaseStat.RunSpeed = playerData.RunSpeed;
        playerBaseStat.MaxStamina = playerData.MaxStamina;
        playerBaseStat.StaminaRegenRate = playerData.StaminaRegenRate;
        playerBaseStat.StaminaDrainRate = playerData.StaminaDrainRate;
        playerBaseStat.SprintStaminaCost = playerData.SprintStaminaCost;
        playerBaseStat.SprintDuration = playerData.SprintDuration;
        playerBaseStat.SprintCooldown = playerData.SprintCooldown;
        playerBaseStat.MaxHP = playerData.MaxHP;
        playerBaseStat.HitDamage = playerData.HitDamage;
    }

    private void mapOtherActions()
    {

        Player.deltaHP = playerCurrentStat.deltaHP;
        EnemyBase.deltaPlayerEXP = playerCurrentStat.deltaEXP;
        StatPanel.getweaponInfo = (Weapons weapon) => weaponInfoMap[weapon];

    }
}
