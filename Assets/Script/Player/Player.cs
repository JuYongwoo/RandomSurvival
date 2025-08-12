using System;
using UnityEngine;



public class Player : MonoBehaviour
{
    public static Action OnFaceDamaged;

    private AudioClip hitsound;

    private Animator animator;

    static public Action<float> deltaHP;

    private bool canWarp = true;


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

        deltaHP(-damage);
        OnFaceDamaged?.Invoke();

        ManagerObject.am.PlaySound(hitsound);


    }

}
