using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitPanel : MonoBehaviour
{

    private enum PortraitPanelObj
    {
        HPBar,
        Remaining
    }
    private Dictionary<PortraitPanelObj, GameObject> PortraitPanelObjDict;


    // Start is called before the first frame update
    void Awake()
    {
        PortraitPanelObjDict = util.mapDictionary<PortraitPanelObj>(this.gameObject);

        otherActionMapping();

    }
    private void otherActionMapping()
    {
        Player.OnRefreshHPBar = (hp) =>
        {
            PortraitPanelObjDict[PortraitPanelObj.HPBar].GetComponent<Slider>().value = hp / 100 + 0.01f;
        };



    }

}
