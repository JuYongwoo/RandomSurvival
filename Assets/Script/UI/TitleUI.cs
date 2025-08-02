using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    private enum UIs
    {
        Start,
        Quit
    }

    private Dictionary<UIs, GameObject> TitleMap;


    private void Awake()
    {
        childsmapping();
        addListner();
    }

    void childsmapping()
    {
        TitleMap = new Dictionary<UIs, GameObject>();

        Transform[] TitleUIs = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform t in GetComponentsInChildren<Transform>(true))
        {
            if (Enum.TryParse(t.name, out UIs uiName))
            {
                if (!TitleMap.ContainsKey(uiName))
                {
                    TitleMap.Add(uiName, t.gameObject);
                }
            }
        }
    }

    void addListner()
    {
        Button StartButton = TitleMap[UIs.Start].GetComponent<Button>();
        Button QuitButton = TitleMap[UIs.Quit].GetComponent<Button>();

        StartButton.onClick.AddListener( () => { UnityEngine.SceneManagement.SceneManager.LoadScene("Main"); });
        QuitButton.onClick.AddListener( () => { Application.Quit(); });

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
