using UnityEngine;

public class MainSceneObject : MonoBehaviour
{

    private void Start()
    {
        ManagerObject.am.PlayBGM(AudioManager.Sounds.BGM);
    }
}