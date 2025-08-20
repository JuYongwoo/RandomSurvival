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
        string minutesString = time / 60 < 10 ? $"0{(time / 60)}" : $"{(time / 60)}"; // ���� 10���� ������ �տ� 0�� ����
        string seconsdString = time % 60 < 10 ? $"0{(time % 60)}" : $"{(time % 60)}"; // �ʰ� 10���� ������ �տ� 0�� ����
        string timeString = $"{minutesString}:{seconsdString}";
        TimePanelmap[TimePanelEnum.RemainingText].GetComponent<Text>().text = timeString;
    }
}
