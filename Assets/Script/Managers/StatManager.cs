using UnityEngine;


public class StatManager
{

    public WeaponDatabase WeaponDB { get; private set; }
    public PlayerDatabase PlayerStats { get; private set; }


    public void OnAwake() // SO파일 PlayerData를 로드하고 게임에 필요한 플레이어 베이스스탯을 가져온다.
    {

        // 무기 데이터 로드
        var weaponImgMap = Util.mapDictionaryWithLoad<WeaponDatabase.Weapons, Sprite>("Graphics/Textures");
        WeaponDB = new WeaponDatabase(weaponImgMap);

        // 플레이어 데이터 로드
        var playerData = loadPlayerData();
        if (playerData != null)
        {
            PlayerStats = new PlayerDatabase(playerData);
        }

        mapOtherActions();
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
        StatPanel.getweaponInfo = (WeaponDatabase.Weapons weapon) => WeaponDB.GetWeaponInfo(weapon);

    }
}
