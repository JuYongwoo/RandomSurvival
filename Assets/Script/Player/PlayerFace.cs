using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerFace : MonoBehaviour
{
    [HideInInspector]
    public Animator Faceanim;

    // Start is called before the first frame update
    void Awake()
    {

        Faceanim = Util.AddOrGetComponent<Animator>(gameObject);
        Addressables.LoadAssetAsync<RuntimeAnimatorController>("PlayerFace").Completed += h =>
        {
            Faceanim.runtimeAnimatorController = h.Result;
        };
        Faceanim.SetBool("Damaged", false);


        Player.OnFaceDamaged = changeFaceDamage;
    }
    IEnumerator ResetFace()
    {
        yield return new WaitForSeconds(0.75f);
        Faceanim.SetBool("Damaged", false);


    }
    public void changeFaceDamage()
    {
        Faceanim.SetBool("Damaged", true);
        StartCoroutine(ResetFace());

    }


}
