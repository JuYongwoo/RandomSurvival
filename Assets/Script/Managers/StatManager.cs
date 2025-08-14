using UnityEngine;


public class StatManager
{

    public WeaponDatabase WeaponDB { get; private set; }
    public PlayerDatabase PlayerStats { get; private set; }


    public void OnAwake() // SO���� PlayerData�� �ε��ϰ� ���ӿ� �ʿ��� �÷��̾� ���̽������� �����´�.
    {

        // ���� ������ �ε�
        var weaponImgMap = Util.mapDictionaryWithLoad<WeaponDatabase.Weapons, Sprite>("Graphics/Textures");
        WeaponDB = new WeaponDatabase(weaponImgMap);

        // �÷��̾� ������ �ε�
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
