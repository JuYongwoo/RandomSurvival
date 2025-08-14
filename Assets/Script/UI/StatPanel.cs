using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static StatManager;



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
    public static Func<WeaponDatabase.Weapons, WeaponDatabase.WeaponInfo> getweaponInfo;



    private void Awake()
    {
        statPanelMap = Util.mapDictionaryInChildren<StatPanelEnum, GameObject>(this.gameObject);

        PlayerDatabase.PlayerCurrentStat.OnRefreshEXPUI = changeEXP;
        PlayerDatabase.PlayerCurrentStat.OnChangeWeapon = changeWeapon;
    }

    private void changeEXP(int currentEXP)
    {
        if( statPanelMap[StatPanelEnum.LvText] == null
            || statPanelMap[StatPanelEnum.ExpText] == null) return;
        statPanelMap[StatPanelEnum.LvText].GetComponent<Text>().text = $"Lv.{currentEXP / 100 + 1}";
        statPanelMap[StatPanelEnum.ExpText].GetComponent<Text>().text = $"EXP {currentEXP%100}/{100}";
    }
    
    private void changeWeapon(WeaponDatabase.Weapons weaponName)
    {
        if (statPanelMap[StatPanelEnum.WeaponImg] == null
            || statPanelMap[StatPanelEnum.WeaponNameTxt] == null
            || statPanelMap[StatPanelEnum.WeaponDmgTxt] == null) return;
        statPanelMap[StatPanelEnum.WeaponDmgTxt].GetComponent<Text>().text = getweaponInfo(weaponName).weaponName;
        statPanelMap[StatPanelEnum.WeaponImg].GetComponent<Image>().sprite = getweaponInfo(weaponName).weaponImg;
        statPanelMap[StatPanelEnum.WeaponDmgTxt].GetComponent<Text>().text = $"µ¥¹ÌÁö {getweaponInfo(weaponName).weaponDMG.ToString()}";

    }
}
