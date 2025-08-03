using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{

    private enum UIName
    {
        HPBar,
        StaminaBar,
        Remaining,
        Mission,
        HowToPlay
    }
    private Dictionary<UIName, GameObject> UIObjMap;


    // Start is called before the first frame update
    void Awake()
    {
        ManagerObject.input.ConfirmKeyAction = () =>
        {
            Time.timeScale = 1;
            UIObjMap[UIName.HowToPlay].SetActive(false);
        };
        mapDictionary();
        otherActionMapping();

        UIObjMap[UIName.HowToPlay].SetActive(true);
        Time.timeScale = 0;

    }
    private void mapDictionary()
    {
        UIObjMap = new Dictionary<UIName, GameObject>();

        foreach (Transform t in GetComponentsInChildren<Transform>(true))
        {
            if (Enum.TryParse(t.name, out UIName uiName))
            {
                if (!UIObjMap.ContainsKey(uiName))
                {
                    UIObjMap.Add(uiName, t.gameObject);
                }
            }
        }
    }
    private void otherActionMapping()
    {
        Player.OnRefreshHPBar = (hp) =>
        {
            UIObjMap[UIName.HPBar].GetComponent<Slider>().value = hp / 100 + 0.01f;
        };

        PlayerMove.OnRefreshStaminaBar = (stm) =>
        {
            UIObjMap[UIName.StaminaBar].GetComponent<Slider>().value = stm / 100;
        };

        MainSceneObject.refreshUI += (coincount) =>
        {
            UIObjMap[UIName.Remaining].GetComponent<Text>().text = coincount + "°³ ³²À½";
        };

    }

}
