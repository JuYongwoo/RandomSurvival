using System;
using UnityEngine;

public class Coin : MonoBehaviour {

    private AudioClip coinsound;

    private void Awake()
    {
        coinsound = Resources.Load<AudioClip>("coinsound");
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