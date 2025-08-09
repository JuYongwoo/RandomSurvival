using UnityEngine;

public class MainSceneObject : MonoBehaviour
{
    static public int EnemyCount = 0;
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