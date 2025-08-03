using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GameObject.Find("unitychan").GetComponent<Animator>();
        ManagerObject.input.ConfirmKeyAction = () => { UnityEngine.SceneManagement.SceneManager.LoadScene("Title"); };
    }
    private void Start()
    {
        anim.SetBool("Win", true);
        anim.SetBool("Lose", false);

    }
}
