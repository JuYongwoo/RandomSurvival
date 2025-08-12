using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePanel : MonoBehaviour
{
    private enum TimePanelEnum
    {
        RemainingText
    }
    private Dictionary<TimePanelEnum, GameObject> TimePanelmap;
    private int time = 1800;


    private void Awake()
    {
        TimePanelmap = Util.mapDictionaryInChildren<TimePanelEnum, GameObject>(this.gameObject);
        InvokeRepeating("flowTime", 1f, 1f);
    }
    void Start()
    {

    }

    private void flowTime ()
    {
        time--;
        TimePanelmap[TimePanelEnum.RemainingText].GetComponent<Text>().text = time / 60 + ":" + time % 60;
    }
}
