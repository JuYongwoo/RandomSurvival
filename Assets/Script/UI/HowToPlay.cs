using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{

    private enum PortraitPanelEnum
    {
        HowToPlay
    }
    private Dictionary<PortraitPanelEnum, GameObject> HowToPlaymap;


    private void Awake()
    {
        HowToPlaymap = Util.mapDictionary<PortraitPanelEnum>(this.gameObject);

        Time.timeScale = 0;
        ManagerObject.input.Enter = () =>
        {
            Time.timeScale = 1;
            HowToPlaymap[PortraitPanelEnum.HowToPlay].SetActive(false);
        };
    }

    void Start()
    {
        HowToPlaymap[PortraitPanelEnum.HowToPlay].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
