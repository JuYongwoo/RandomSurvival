#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class TilePrefabGrid
{
    [MenuItem("Assets/Tile Prefab/Make 11x11 Grid At World Origin", priority = 2000)]
    private static void MakeGrid11x11()
    {
        var prefab = GetSelectedPrefabAsset();
        if (prefab == null) return;

        if (!TryGetPrefabBounds(prefab, out var bounds))
        {
            EditorUtility.DisplayDialog("Tile Prefab", "프리팹에서 유효한 Renderer를 찾지 못했습니다.", "OK");
            return;
        }

        float spacing = 22f;

        Vector3 center = Vector3.zero;

        const int half = 10; // -10..+10
        var root = new GameObject($"{prefab.name}_Grid_11x11").transform;
        Undo.RegisterCreatedObjectUndo(root.gameObject, "Create Grid Root");

        for (int gx = -half; gx <= half; gx++)
        {
            for (int gz = -half; gz <= half; gz++)
            {
                Vector3 pos = center + new Vector3(gx * spacing, 0f, gz * spacing);
                var inst = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                Undo.RegisterCreatedObjectUndo(inst, "Instantiate Prefab");
                inst.transform.SetParent(root, true);

                float offsetY = -bounds.center.y;
                inst.transform.position = new Vector3(pos.x, pos.y + offsetY, pos.z);
                inst.transform.rotation = Quaternion.identity;
            }
        }

        Selection.activeObject = root.gameObject;
        EditorGUIUtility.PingObject(root.gameObject);
    }

    [MenuItem("Assets/Tile Prefab/Make 11x11 Grid At World Origin", true)]
    private static bool ValidateMakeGrid11x11()
    {
        return GetSelectedPrefabAsset() != null;
    }

    private static GameObject GetSelectedPrefabAsset()
    {
        var obj = Selection.activeObject;
        if (!obj) return null;
        string path = AssetDatabase.GetAssetPath(obj);
        if (string.IsNullOrEmpty(path)) return null;

        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        return prefab != null && PrefabUtility.IsPartOfPrefabAsset(prefab) ? prefab : null;
    }

    private static bool TryGetPrefabBounds(GameObject prefabAsset, out Bounds bounds)
    {
        bounds = new Bounds();
        string path = AssetDatabase.GetAssetPath(prefabAsset);
        if (string.IsNullOrEmpty(path)) return false;

        GameObject tempRoot = null;
        try
        {
            tempRoot = PrefabUtility.LoadPrefabContents(path);
            var renderers = tempRoot.GetComponentsInChildren<Renderer>(true);
            if (renderers == null || renderers.Length == 0)
                return false;

            bounds = renderers[0].bounds;
            foreach (var r in renderers.Skip(1))
                bounds.Encapsulate(r.bounds);

            return true;
        }
        finally
        {
            if (tempRoot != null)
                PrefabUtility.UnloadPrefabContents(tempRoot);
        }
    }
}
#endif
