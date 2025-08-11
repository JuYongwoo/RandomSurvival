using System;
using UnityEngine;



public class Player : MonoBehaviour
{
    public static Action OnFaceDamaged;
    public static Action<float> OnRefreshHPBar;

    private AudioClip hitsound;

    private Animator animator;


    private bool canWarp = true;
    private bool isDead = false;


    private void Awake()
    {
        hitsound = Resources.Load<AudioClip>("hitsound");
        animator = GetComponentInChildren<Animator>();
        CameraMove.playerPosition = () => { return gameObject.transform.position; };
        Teleport.warpOn = SetWarpable;
        EnemyBase.hitplayer = GetDamaged;
        Teleport.isplayerwarpready = () => canWarp;
        PlayerStateMachine.animator = () => animator;



    }
    private void Start()
    {
        canWarp = true;

    }



    private void SetWarpable(bool enable)
    {
        canWarp = enable;
    }

    public void GetDamaged(float damage)
    {
        if (isDead) return;

        ManagerObject.playerStatObj.playerCurrentStat.HP -= damage;
        OnFaceDamaged?.Invoke();
        OnRefreshHPBar?.Invoke(ManagerObject.playerStatObj.playerCurrentStat.HP);

        ManagerObject.am.PlaySound(hitsound);

        if (ManagerObject.playerStatObj.playerCurrentStat.HP <= 0f)
        {
            isDead = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
        }
    }

}
