using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public static Action OnFaceDamaged;
    public static Action<float> OnRefreshHPBar;

    private AudioClip hitsound;

    private bool canWarp = true;
    private bool isDead = false;

    private void Awake()
    {
        hitsound = Resources.Load<AudioClip>("hitsound");
        canWarp = true;
        CameraMove.playerPosition = () => { return gameObject.transform.position; };
        Teleport.warpOn = SetWarpable;
        Enemy.hitplayer = GetDamaged;
        Teleport.isplayerwarpready = () => canWarp;

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
