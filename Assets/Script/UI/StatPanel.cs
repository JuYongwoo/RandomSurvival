using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class StatPanel : MonoBehaviour
{

public enum StatPanelEnum
{
    LvText,
    ExpText,
    WeaponImg,
    WeaponNameTxt,
    WeaponDmgTxt
}


    private Dictionary<StatPanelEnum, GameObject> statPanelMap;



    private void Awake()
    {
        statPanelMap = Util.mapDictionaryInChildren<StatPanelEnum, GameObject>(this.gameObject);

        PlayerDatabase.OnRefreshEXPUI = changeEXP;
        PlayerDatabase.OnChangeWeapon = changeWeapon;
    }

    private void changeEXP(int currentEXP, int maxEXP)
    {
        if( statPanelMap[StatPanelEnum.LvText] == null
            || statPanelMap[StatPanelEnum.ExpText] == null) return;
        statPanelMap[StatPanelEnum.LvText].GetComponent<Text>().text = $"Lv.{currentEXP / maxEXP + 1}";
        statPanelMap[StatPanelEnum.ExpText].GetComponent<Text>().text = $"Exp {currentEXP% maxEXP}/{maxEXP}";
    }
    
    private void changeWeapon(WeaponDatabase.Weapons weaponName)
    {
        if (statPanelMap[StatPanelEnum.WeaponImg] == null
            || statPanelMap[StatPanelEnum.WeaponNameTxt] == null
            || statPanelMap[StatPanelEnum.WeaponDmgTxt] == null) return;
        statPanelMap[StatPanelEnum.WeaponImg].GetComponent<Image>().sprite = ManagerObject.playerStatObj.WeaponStatDB.GetInfo(weaponName).Icon;
        statPanelMap[StatPanelEnum.WeaponNameTxt].GetComponent<Text>().text = ManagerObject.playerStatObj.WeaponStatDB.GetInfo(weaponName).Name;
        statPanelMap[StatPanelEnum.WeaponDmgTxt].GetComponent<Text>().text = $"µ¥¹ÌÁö {ManagerObject.playerStatObj.WeaponStatDB.GetInfo(weaponName).BaseDMG.ToString()}";

    }
}
