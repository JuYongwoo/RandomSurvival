using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCanvas : MonoBehaviour
{

    private enum TitleCanvasObjs{
        StartBtn,
        QuitBtn
    }
    private Dictionary<TitleCanvasObjs, GameObject> titleCanvasMap;

    // Start is called before the first frame update
    void Awake()
    {
        titleCanvasMap = Util.mapDictionaryInChildren<TitleCanvasObjs, GameObject>(this.gameObject);
    }


    private void Start()
    {
        titleCanvasMap[TitleCanvasObjs.StartBtn].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        });

        titleCanvasMap[TitleCanvasObjs.QuitBtn].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
