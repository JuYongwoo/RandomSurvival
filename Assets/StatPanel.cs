using System.Collections.Generic;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    private enum StatPanelEnum
    {

    }
    private Dictionary<StatPanelEnum, GameObject> StatPanelmap;

    private void Awake()
    {
        StatPanelmap = util.mapDictionary<StatPanelEnum>(this.gameObject);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
