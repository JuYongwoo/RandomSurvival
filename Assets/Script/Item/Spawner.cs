using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Spawner : MonoBehaviour
{
    private GameObject enemyPrefab;
    private List<GameObject> spawnPlace = new List<GameObject>();
    private bool isActive = false;

    private void Awake()
    {
        cacheChilds();
        enemyPrefab = Addressables.LoadAssetAsync<GameObject>("Wolf").WaitForCompletion();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            isActive = true;
            InvokeRepeating("spawnEnemy", 5f, 5f);
        }
    }

    private void cacheChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPlace.Add(transform.GetChild(i).gameObject);
        }
    }

    private void spawnEnemy()
    {
        Vector3 rd_position = spawnPlace[Random.Range(0, spawnPlace.Count)].transform.position;
        GameObject go = Instantiate(enemyPrefab, rd_position, Quaternion.identity);
    }
}
