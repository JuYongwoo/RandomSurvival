using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitPanel : MonoBehaviour
{

    private enum PortraitPanelObj
    {
        HPBar,
        HPTxt,
        Remaining
    }
    private Dictionary<PortraitPanelObj, GameObject> PortraitPanelObjDict;


    // Start is called before the first frame update
    void Awake()
    {
        PortraitPanelObjDict = Util.mapDictionaryInChildren<PortraitPanelObj,GameObject>(this.gameObject);

        otherActionMapping();

    }
    private void otherActionMapping()
    {
        StatObject.PlayerCurrentStat.OnRefreshHPBar = (hp) =>
        {
            PortraitPanelObjDict[PortraitPanelObj.HPBar].GetComponent<Slider>().value = hp / 100 + 0.01f;
            PortraitPanelObjDict[PortraitPanelObj.HPTxt].GetComponent<Text>().text = (int)hp + "/100";
        };



    }

}
