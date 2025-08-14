using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static StatObject;



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
    public static Func<Weapons, WeaponInfo> getweaponInfo;



    private void Awake()
    {
        statPanelMap = Util.mapDictionaryInChildren<StatPanelEnum, GameObject>(this.gameObject);



        StatObject.PlayerCurrentStat.OnRefreshEXPUI = changeEXP;
    }
    private void Start()
    {
        //playerstatManager에서 액션 호출로 변경해야함
        changeWeapon(Weapons.Hand);
        changeEXP(1420);
    }

    private void changeEXP(int currentEXP)
    {
        if( statPanelMap[StatPanelEnum.LvText] == null
            || statPanelMap[StatPanelEnum.ExpText] == null) return;
        statPanelMap[StatPanelEnum.LvText].GetComponent<Text>().text = "Lv. "+ currentEXP / 100;
        statPanelMap[StatPanelEnum.ExpText].GetComponent<Text>().text = "EXP "+ currentEXP%100 + "/" + 100;
    }
    
    private void changeWeapon(Weapons weaponName)
    {
        if (statPanelMap[StatPanelEnum.WeaponImg] == null
            || statPanelMap[StatPanelEnum.WeaponNameTxt] == null
            || statPanelMap[StatPanelEnum.WeaponDmgTxt] == null) return;
        statPanelMap[StatPanelEnum.WeaponDmgTxt].GetComponent<Text>().text = getweaponInfo(weaponName).weaponName;
        statPanelMap[StatPanelEnum.WeaponImg].GetComponent<Image>().sprite = getweaponInfo(weaponName).weaponImg;
        statPanelMap[StatPanelEnum.WeaponDmgTxt].GetComponent<Text>().text = "데미지 " + getweaponInfo(weaponName).weaponDMG.ToString();

    }
}
