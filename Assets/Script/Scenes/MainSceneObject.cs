using System;
using UnityEngine;

public class MainSceneObject : MonoBehaviour
{

    public static int remainingCoins = -1;
    public static event Action<int> refreshUI;

    private void Awake()
    {
        Coin.coinGet = () => {
            remainingCoins -= 1;
            refreshUI?.Invoke(remainingCoins);
        };
    }

    private void Start()
    {

        GameObject[] remainingcoinarray = GameObject.FindGameObjectsWithTag("Coin");
        remainingCoins = remainingcoinarray.Length;
        refreshUI(remainingCoins);

    }
}