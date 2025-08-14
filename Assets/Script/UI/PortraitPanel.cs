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
    private const float SliderVisualOffset = 0.01f;

    // Start is called before the first frame update
    void Awake()
    {
        PortraitPanelObjDict = Util.mapDictionaryInChildren<PortraitPanelObj,GameObject>(this.gameObject);

        otherActionMapping();

    }
    private void otherActionMapping()
    {
        PlayerDatabase.PlayerCurrentStat.OnRefreshHPBar = (hp,max) =>
        {
            if( PortraitPanelObjDict[PortraitPanelObj.HPBar] == null
                || PortraitPanelObjDict[PortraitPanelObj.HPTxt] == null) return;
            PortraitPanelObjDict[PortraitPanelObj.HPBar].GetComponent<Slider>().value = hp / max + SliderVisualOffset;
            PortraitPanelObjDict[PortraitPanelObj.HPTxt].GetComponent<Text>().text = $"{(int)hp}/{max}";
        };



    }

}
