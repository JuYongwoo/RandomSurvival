using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseSceneManager : MonoBehaviour
{
    private void Start()
    {
        Animator anim = GameObject.Find("unitychan").GetComponent<Animator>();
        anim.SetBool("Win", false);
        anim.SetBool("Lose", true);
        ManagerObject.input.ConfirmKeyAction -= Regame;
        ManagerObject.input.ConfirmKeyAction += Regame;
    }

    void Regame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
