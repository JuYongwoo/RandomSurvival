using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static StatPanel;

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

    public enum Weapons
    {
        Hand,
        Bow,
        Magic
    }
    private Dictionary<StatPanelEnum, GameObject> statPanelMap;
    private Dictionary<Weapons, Sprite> weaponImgMap;
    private Dictionary<Weapons, string> weaponNameMap;


    private void Awake()
    {
        statPanelMap = Util.mapDictionary<StatPanelEnum>(this.gameObject);
        weaponImgMap = Util.mapDictionaryWithLoad<Weapons, Sprite>("Graphics/Textures");
        weaponNameMap = new Dictionary<Weapons, string>
        {
            { Weapons.Hand, "맨손" },
            { Weapons.Bow, "활" },
            { Weapons.Magic, "마법" }
        };
    }
    private void Start()
    {
        //playerstatManager에서 액션 호출로 변경해야함
        changeWeaponImg(Weapons.Hand);
        changeWeaponName(Weapons.Hand);
        changeWeaponDmg(10);
    }

    private void changeLv(int lv)
    {
        statPanelMap[StatPanelEnum.LvText].GetComponent<Text>().text = "Lv. "+ lv;
    }
    private void changeEXP(int currentEXP, int maxEXP)
    {
        statPanelMap[StatPanelEnum.ExpText].GetComponent<Text>().text = "EXP "+ currentEXP + "/" + maxEXP;
    }
    
    private void changeWeaponName(Weapons weaponName)
    {
        go.GetComponent<Text>().text = weaponNameMap[weaponName];

    }
    private void changeWeaponDmg(int weaponDMG)
    {
        statPanelMap[StatPanelEnum.WeaponDmgTxt].GetComponent<Text>().text = "데미지 " + weaponDMG.ToString();

    }
    private void changeWeaponImg(Weapons weaponIMG)
    {
        statPanelMap[StatPanelEnum.WeaponImg].GetComponent<Image>().sprite = weaponImgMap[weaponIMG];

    }
}
