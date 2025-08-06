using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject enemyPrefab;
    private List<GameObject> spawnPlace = new List<GameObject>();
    private bool isActive = false;

    private void Awake()
    {
        cacheChilds();
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            isActive = true;
            StartCoroutine(spawnEnemy());
        }
    }

    private void cacheChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPlace.Add(transform.GetChild(i).gameObject);
        }
    }

    private IEnumerator spawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            Vector3 rd_position = spawnPlace[Random.Range(0, spawnPlace.Count)].transform.position;
            GameObject go = Instantiate(enemyPrefab, rd_position, Quaternion.identity);
        }
    }
}
