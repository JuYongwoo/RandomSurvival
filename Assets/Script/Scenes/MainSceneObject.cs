using UnityEngine;

public class MainSceneObject : MonoBehaviour
{
    private AudioClip bgm;

    private void Awake()
    {
        bgm = Resources.Load<AudioClip>("BGM");
    }
    private void Start()
    {
        ManagerObject.am.PlayBGM(bgm);
    }
}