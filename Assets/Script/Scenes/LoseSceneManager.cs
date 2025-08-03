using UnityEngine;

public class LoseSceneManager : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GameObject.Find("unitychan").GetComponent<Animator>();
        ManagerObject.input.ConfirmKeyAction = () => { UnityEngine.SceneManagement.SceneManager.LoadScene("Title"); };
    }
    private void Start()
    {
        anim.SetBool("Win", false);
        anim.SetBool("Lose", true);
        
    }
}
