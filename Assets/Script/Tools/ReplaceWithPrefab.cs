#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ReplaceWithPrefab : MonoBehaviour
{
    static string prefabname = "Assets/Resources/Prefabs/bush02.prefab"; //TODO 사용 시 대체할 프리팹으로 이름 바꿀 것
    [MenuItem("Tools/Replace Selected with Prefab")]
    static void Replace()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabname);

        if (prefab == null)
        {
            Debug.LogError("프리팹 경로 확인: " + prefabname);
            return;
        }

        foreach (GameObject obj in Selection.gameObjects)
        {
            GameObject newInstance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, obj.scene);
            newInstance.transform.position = obj.transform.position;
            newInstance.transform.rotation = obj.transform.rotation;
            newInstance.transform.localScale = obj.transform.localScale;

            DestroyImmediate(obj);
        }

        Debug.Log("선택된 오브젝트들을 프리팹으로 대체했습니다.");
    }
}
#endif
