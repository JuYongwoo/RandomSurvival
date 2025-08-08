using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPanel : MonoBehaviour
{
    private enum StatPanelEnum
    {
        LvText,
        ExpText,
        WeaponImg,
        WeaponNameTxt,
        WeaponDmgTxt
    }
    private Dictionary<StatPanelEnum, GameObject> statPanelmap;

    private void Awake()
    {
        statPanelmap = util.mapDictionary<StatPanelEnum>(this.gameObject);
    }

    private void changeLv(int lv)
    {
        statPanelmap[StatPanelEnum.LvText].GetComponent<Text>().text = "Lv. "+ lv;
    }
    private void changeEXP(int currentEXP, int maxEXP)
    {
        statPanelmap[StatPanelEnum.ExpText].GetComponent<Text>().text = "EXP "+ currentEXP + "/" + maxEXP;
    }
    
    private void changeWeaponName(Weapons weaponName)
    {
        statPanelmap[StatPanelEnum.WeaponNameTxt].GetComponent<Text>().text = weaponName.ToString();

    }
    private void changeWeaponDmg(int weaponDMG)
    {
        statPanelmap[StatPanelEnum.WeaponDmgTxt].GetComponent<Text>().text = weaponDMG.ToString();

    }
}
