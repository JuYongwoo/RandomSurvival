using System;
using UnityEngine;

public enum PlayerState
{
    Idle,
    MoveStart,
    Moving,
    AttackStart,
    Attack_Attacking,
    Attack_MoveStart,
    Attack_Moving,
    AttackMove_MoveStart,
    AttackMove_Moving,
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
        EnemyBase.hitplayer = GetDamaged;
        Teleport.isplayerwarpready = () => canWarp;
        PlayerStateMachine.playerstate = () => state;
        PlayerStateMachine.setPlayerstate = setPlayerState;
        InputManager.setPlayerstate = setPlayerState;


        animator = GetComponentInChildren<Animator>();

    }
    private void setPlayerState(PlayerState s)
    {
        state = s;
        Debug.Log("PlayerState = " + s);


        int animState = (int)s;
        if (s == PlayerState.AttackMove_Moving || s == PlayerState.Attack_Moving) animState = (int)PlayerState.Moving;
        if (s == PlayerState.AttackMove_Attacking) animState = (int)PlayerState.Attack_Attacking;


        animator.SetInteger("State", animState);
        
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
