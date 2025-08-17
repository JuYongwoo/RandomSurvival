using UnityEngine;
using Unity.AI.Navigation;

public class MainSceneObject : MonoBehaviour
{
    [HideInInspector]
    //public GameObject player;
    private void Awake()
    {
    }

    private void Start()
    {
        ManagerObject.am.PlayBGM(AudioManager.Sounds.BGM);
        //SpawnRooms();
        //NavMeshSurface surface = Util.AddOrGetComponent<NavMeshSurface>(this.gameObject);
        //surface.BuildNavMesh();
        //player = Instantiate(Resources.Load<GameObject>("Player/Player"), new Vector3(0, 1, 0), Quaternion.identity);
    }

    private void SpawnRooms() //���⼭ ��ȹ������ �������� ���� �����ؾ� �� ������ ����
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