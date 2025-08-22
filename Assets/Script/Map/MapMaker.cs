using UnityEngine;
using Unity.AI.Navigation;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

public class MapMaker : MonoBehaviour
{
    List<List<string>> MapStrings;
    private GameObject playerPrefab;
    private GameObject normalRoomPrefab;
    private GameObject weaponRoomPrefab;
    private GameObject startRoomPrefab;

    private void Awake()
    {
        MapStrings = Util.LoadGrid("Map");
        playerPrefab = Addressables.LoadAssetAsync<GameObject>("Player").WaitForCompletion();
        normalRoomPrefab = Addressables.LoadAssetAsync<GameObject>("NormalRoom").WaitForCompletion();
        startRoomPrefab = Addressables.LoadAssetAsync<GameObject>("StartRoom").WaitForCompletion();
        weaponRoomPrefab = Addressables.LoadAssetAsync<GameObject>("WeaponRoom").WaitForCompletion();

    }

    private void Start()
    {
        int rowCount = MapStrings.Count;
        int colCount = 0;
        for (int i = 0; i < rowCount; i++)
            colCount = Mathf.Max(colCount, MapStrings[i].Count);

        // �߾� �ε��� ���
        int centerRow = (rowCount % 2 == 0) ? (rowCount / 2 - 1) : (rowCount / 2);
        int centerCol = (colCount % 2 == 0) ? (colCount / 2 - 1) : (colCount / 2);

        // �÷��̾� ���� ��ġ ���� ����
        Vector3 playerStartPosition = new Vector3();

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < MapStrings[i].Count; j++)
            {
                float x = (i - centerRow) * 22f;
                float z = (j - centerCol) * 22f;

                if (MapStrings[i][j] == "o") //Room ���̸鼭 key���� MapStrings[i][j]�� �������� addressabble�� ��ȯ�ϵ��� �����ؾ���
                {
                    Instantiate(normalRoomPrefab, new Vector3(z, 0f, x), Quaternion.identity, this.transform);
                }
                else if (MapStrings[i][j] == "s")
                {
                    Instantiate(startRoomPrefab, new Vector3(z, 0f, x), Quaternion.identity, this.transform);
                    playerStartPosition = new Vector3(z, 0f, x); // �÷��̾� ���� ��ġ ����
                }
                else if (MapStrings[i][j] == "w")
                {
                    Instantiate(weaponRoomPrefab, new Vector3(z, 0f, x), Quaternion.identity, this.transform);
                }
            }
        }

        GetComponent<NavMeshSurface>().BuildNavMesh();

        Instantiate(playerPrefab, playerStartPosition, Quaternion.identity);
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