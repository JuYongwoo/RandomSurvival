using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject enemyprefab;
    private float timer = 0f;
    [SerializeField]
    private GameObject[] spawnplace = new GameObject[3];

    private void Awake()
    {
        enemyprefab = Resources.Load<GameObject>("Prefabs/Enemy");
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 5f) {
            GameObject go = Instantiate(enemyprefab);
            Vector3 rd_position = spawnplace[Random.Range(0,3)].transform.position;
            go.transform.position = rd_position;

            timer = 0f;

        }

        
    }
}
