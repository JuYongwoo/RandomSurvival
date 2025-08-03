#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ReplaceWithPrefab : MonoBehaviour
{
    static string prefabname = "Assets/Resources/Prefabs/bush02.prefab"; //TODO ��� �� ��ü�� ���������� �̸� �ٲ� ��
    [MenuItem("Tools/Replace Selected with Prefab")]
    static void Replace()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabname);

        if (prefab == null)
        {
            Debug.LogError("������ ��� Ȯ��: " + prefabname);
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

        Debug.Log("���õ� ������Ʈ���� ���������� ��ü�߽��ϴ�.");
    }
}
#endif
