using System;
using UnityEngine;

public class Coin : MonoBehaviour {

    private AudioClip coinsound;
    public static Action coinGet;

    private void Awake()
    {
        coinsound = Resources.Load<AudioClip>("coinsound");
    }


    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player")) return;

        PlaySoundDetached();
        coinGet();

        gameObject.SetActive(false);
    }

    private void PlaySoundDetached()
    {

        ManagerObject.am.PlaySound(coinsound);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward*Time.deltaTime*100);

    }
}