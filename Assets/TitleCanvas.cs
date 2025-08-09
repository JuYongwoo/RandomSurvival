using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCanvas : MonoBehaviour
{

    private enum TitleCanvasObjs{
        Start,
        Quit
    }
    private Dictionary<TitleCanvasObjs, GameObject> titleCanvasMap;

    // Start is called before the first frame update
    void Awake()
    {
        titleCanvasMap = util.mapDictionary<TitleCanvasObjs>(this.gameObject);
    }


    private void Start()
    {
        titleCanvasMap[TitleCanvasObjs.Start].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        });

        titleCanvasMap[TitleCanvasObjs.Quit].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
