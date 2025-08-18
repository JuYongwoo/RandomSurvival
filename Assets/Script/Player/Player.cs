using System;
using UnityEngine;



public class Player : MonoBehaviour
{
    public static Action OnFaceDamaged;

    private Animator animator;

    static public Action<float> deltaHP;

    private bool canWarp = true;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        CameraMove.playerPosition = () => { return gameObject.transform.position; };
        Teleport.warpOn = SetWarpable;
        EnemyBase.hitplayer = GetDamaged;
        Teleport.isplayerwarpready = () => canWarp;
        PlayerStateMachine.animator = () => animator;



    }
    private void Start()
    {
        ManagerObject.playerStatObj.PlayerStatDB.deltaEXP(0); // 초기화용
        ManagerObject.playerStatObj.PlayerStatDB.deltaHP(0); // 초기화용
        ManagerObject.playerStatObj.PlayerStatDB.setCurrentWeapon(WeaponDatabase.Weapons.Hand); // 초기화용

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

        ManagerObject.am.PlaySound(AudioManager.Sounds.hitsound);


    }

}
