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

    public enum Weapons
    {
        Hand,
        Bow,
        Magic
    }

    public struct WeaponInfo{
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



    private Dictionary<StatPanelEnum, GameObject> statPanelMap;
    private Dictionary<Weapons, WeaponInfo> weaponInfoMap;



    private Dictionary<Weapons, Sprite> weaponImgMap;

    private void Awake()
    {
        statPanelMap = Util.mapDictionaryInChildren<StatPanelEnum, GameObject>(this.gameObject);
        weaponImgMap = Util.mapDictionaryWithLoad<Weapons, Sprite>("Graphics/Textures");
        weaponInfoMap = new Dictionary<Weapons, WeaponInfo>
        {
            { Weapons.Hand,  new WeaponInfo( "맨손", weaponImgMap[Weapons.Hand],  10) },
            { Weapons.Bow,   new WeaponInfo("활",   weaponImgMap[Weapons.Bow],   15) },
            { Weapons.Magic, new WeaponInfo("마법", weaponImgMap[Weapons.Magic], 20) },
        };


        PlayerStatObject.OnRefreshEXPUI = changeEXP;
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
        statPanelMap[StatPanelEnum.WeaponDmgTxt].GetComponent<Text>().text = weaponInfoMap[weaponName].weaponName;
        statPanelMap[StatPanelEnum.WeaponImg].GetComponent<Image>().sprite = weaponInfoMap[weaponName].weaponImg;
        statPanelMap[StatPanelEnum.WeaponDmgTxt].GetComponent<Text>().text = "데미지 " + weaponInfoMap[weaponName].weaponDMG.ToString();

    }
}
