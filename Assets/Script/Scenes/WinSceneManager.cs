using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
    private void Start()
    {
        Animator anim = GameObject.Find("unitychan").GetComponent<Animator>();
        anim.SetBool("Win", true);
        anim.SetBool("Lose", false);
        ManagerObject.input.ConfirmKeyAction -= Regame;
        ManagerObject.input.ConfirmKeyAction += Regame;
    }

    void Regame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
