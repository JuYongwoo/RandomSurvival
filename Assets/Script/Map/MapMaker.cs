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

        //TODO JYW 2025-08-22
        //Addressable 라벨"Room"으로 등록된 애들을 전부 찾고
        //그 라벤들 의 key값을 저장
        //s, o, w key값이 있다면
        //csv에서 s, o, w 값과 위 key값을 매칭시켜 소환
        //이렇게 하면 나중에 g, p 등 추가할 때 csv와 리소스만 추가하면 자동 적용이 가능
        /////////////////////


        int rowCount = MapStrings.Count;
        int colCount = 0;
        for (int i = 0; i < rowCount; i++)
            colCount = Mathf.Max(colCount, MapStrings[i].Count);

        // 중앙 인덱스 계산
        int centerRow = (rowCount % 2 == 0) ? (rowCount / 2 - 1) : (rowCount / 2);
        int centerCol = (colCount % 2 == 0) ? (colCount / 2 - 1) : (colCount / 2);

        // 플레이어 시작 위치 저장 변수
        GameObject playerStartRoom = new GameObject();

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < MapStrings[i].Count; j++)
            {
                float x = (i - centerRow) * 22f;
                float z = (j - centerCol) * 22f;

                if (MapStrings[i][j] == "o") //Room 라벨이면서 key값이 MapStrings[i][j]인 프리팹을 addressabble로 소환하도록 변경해야함
                {
                    Instantiate(normalRoomPrefab, new Vector3(z, 0f, x), Quaternion.identity, this.transform);
                }
                else if (MapStrings[i][j] == "s")
                {
                    playerStartRoom = Instantiate(startRoomPrefab, new Vector3(z, 0f, x), Quaternion.identity, this.transform);
                }
                else if (MapStrings[i][j] == "w")
                {
                    Instantiate(weaponRoomPrefab, new Vector3(z, 0f, x), Quaternion.identity, this.transform);
                }
            }
        }

        GetComponent<NavMeshSurface>().BuildNavMesh();

        Instantiate(playerPrefab, playerStartRoom.transform.position, Quaternion.identity);
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