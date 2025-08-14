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

    public WeaponDatabase WeaponDB { get; private set; }
    public PlayerStats PlayerStats { get; private set; }


    public void OnAwake() // SO파일 PlayerData를 로드하고 게임에 필요한 플레이어 베이스스탯을 가져온다.
    {

        // 무기 데이터 로드
        var weaponImgMap = Util.mapDictionaryWithLoad<Weapons, Sprite>("Graphics/Textures");
        WeaponDB = new WeaponDatabase(weaponImgMap);

        // 플레이어 데이터 로드
        var playerData = loadPlayerData();
        if (playerData != null)
        {
            PlayerStats = new PlayerStats(playerData);
        }
        mapOtherActions();
    }
    public void OnStart()
    {
    }


    public PlayerDataSO loadPlayerData()
    {
        PlayerDataSO[] playerDatas = Resources.LoadAll<PlayerDataSO>("GameData/Player/");
        if (playerDatas.Length != 1)
        {
            Debug.LogError("PlayerDataSO not found in Resources/GameData/Player");
            return null;
        }

        PlayerDataSO playerData = playerDatas[0];

        return playerData;
    }

    private void mapOtherActions()
    {

        Player.deltaHP = PlayerStats.Current.deltaHP;
        EnemyBase.deltaPlayerEXP = PlayerStats.Current.deltaEXP;
        StatPanel.getweaponInfo = (Weapons weapon) => WeaponDB.GetWeaponInfo(weapon);

    }
}
