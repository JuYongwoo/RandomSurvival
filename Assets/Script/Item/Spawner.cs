using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject enemyprefab;
    [SerializeField]
    private GameObject[] spawnplace = new GameObject[3];

    private void Awake()
    {
        enemyprefab = Resources.Load<GameObject>("Prefabs/Enemy");
        StartCoroutine(spawnEnemy());
    }

    private IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(5);

        GameObject go = Instantiate(enemyprefab);
        Vector3 rd_position = spawnplace[Random.Range(0, 3)].transform.position;
        go.transform.position = rd_position;

        StartCoroutine(spawnEnemy());
    }
}
