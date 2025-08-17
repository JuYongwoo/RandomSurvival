using UnityEngine;
using Unity.AI.Navigation;

public class MainSceneObject : MonoBehaviour
{

    private void Awake()
    {
    }

    private void Start()
    {
        NavMeshSurface surface = GetComponent<NavMeshSurface>();
        ManagerObject.am.PlayBGM(AudioManager.Sounds.BGM);
        SpawnRooms();
        surface.BuildNavMesh();
    }

    private void SpawnRooms() //여기서 기획적으로 랜덤으로 방을 생성해야 할 것으로 보임
    {
        GameObject roomPrefab = Resources.Load<GameObject>("Etc/Room");

        for (int x = -10; x <= 10; x++)
        {
            for (int z = -10; z <= 10; z++)
            {
                Vector3 pos = new Vector3(x * 22f, 0f, z * 22f);
                Instantiate(roomPrefab, pos, Quaternion.identity, this.transform);
            }
        }
    }
}