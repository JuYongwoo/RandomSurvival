using UnityEngine;
using UnityEngine.AddressableAssets;

public class Coin : MonoBehaviour {

    private AudioClip coinsound;

    private void Awake()
    {
        coinsound = Addressables.LoadAssetAsync<AudioClip>("coinsound").WaitForCompletion();

    }


    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player")) return;

        ManagerObject.am.PlaySound(AudioManager.Sounds.coinsound);

        gameObject.SetActive(false);
    }


    private void Update()
    {
        transform.Rotate(Vector3.forward*Time.deltaTime*100);

    }
}