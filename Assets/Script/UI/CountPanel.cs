using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountPanel : MonoBehaviour
{
    private enum CountPanelEnum
    {
        EnemyCountTxt
    }
    private Dictionary<CountPanelEnum, GameObject> countPanelMap;
    private Text enemyCountTxt;
    private int enemyCount = 0;

    private void Awake()
    {
        countPanelMap = Util.mapDictionary<CountPanelEnum>(this.gameObject);
        enemyCountTxt = countPanelMap[CountPanelEnum.EnemyCountTxt].GetComponent<Text>();
        EnemyBase.deltaEnemyCount = deltaEnemyCount;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void deltaEnemyCount(int delta)
    {
        enemyCount += delta;
        enemyCountTxt.text = "ÀûÀÇ ¼ö " + enemyCount;
    }
}
