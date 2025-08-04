using System;
using UnityEngine;

public enum PlayerState
{
    Idle,
    MoveStart,
    Moving,
    AttackStart,
    Attacking,
    AttackMove_MoveStart,
    AttackMove_Moving,
    AttackMove_AttackStart,
    AttackMove_Attacking

}
public class Player : MonoBehaviour
{
    public static Action OnFaceDamaged;
    public static Action<float> OnRefreshHPBar;

    private AudioClip hitsound;

    private Animator animator;


    private bool canWarp = true;
    private bool isDead = false;

    private PlayerState state;

    private void Awake()
    {
        hitsound = Resources.Load<AudioClip>("hitsound");
        canWarp = true;
        CameraMove.playerPosition = () => { return gameObject.transform.position; };
        Teleport.warpOn = SetWarpable;
        Enemy.hitplayer = GetDamaged;
        Teleport.isplayerwarpready = () => canWarp;
        PlayerMove.playerstate = () => state;
        PlayerAttack.playerstate = () => state;
        InputManager.setPlayerstate = setPlayerState;
        PlayerMove.setPlayerstate = setPlayerState;
        PlayerAttack.setPlayerstate = setPlayerState;


        animator = GetComponentInChildren<Animator>();

    }
    private void setPlayerState(PlayerState s)
    {
        state = s;
        Debug.Log("PlayerState = " + s);


        int animState = (int)s;
        if (animState >= 6) animState -= 4;
        animator.SetInteger("State", animState);
        
    }


    private void SetWarpable(bool enable)
    {
        canWarp = enable;
    }

    public void GetDamaged()
    {
        if (isDead) return;

        ManagerObject.playerStatObj.playerCurrentStat.HP -= ManagerObject.playerStatObj.playerBaseStat.HitDamage;
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
