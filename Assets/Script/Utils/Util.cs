using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Util
{
    static public Dictionary<T, T2> mapDictionaryInChildren<T, T2>(GameObject go) where T : Enum where T2 : UnityEngine.Object
    {
        Dictionary<T, T2> dict = new Dictionary<T, T2>();

        Transform[] children = go.GetComponentsInChildren<Transform>();

        foreach (T enumName in Enum.GetValues(typeof(T)))
        {
            foreach (Transform child in children)
            {
                if (child.name == enumName.ToString())
                {
                    if (typeof(T2) == typeof(GameObject))
                    {
                        dict[enumName] = (T2)(object)child.gameObject;
                    }
                    else
                    {
                        dict[enumName] = child.GetComponent<T2>();
                    }
                    break;
                }
            }
        }

        return dict;

    }


    static public Dictionary<T, T2> mapDictionaryWithLoad<T, T2>(string filePath) where T : Enum where T2 : UnityEngine.Object
    {
        Dictionary<T, T2> dict = new Dictionary<T, T2>();

        foreach (T s in System.Enum.GetValues(typeof(T)))
        {
            dict[s] = Resources.Load<T2>(filePath + "/" + s.ToString());
        }

        return dict;
    }

    public static Dictionary<TEnum, TObject> MapEnumToAddressables<TEnum, TObject>(string label)
        where TEnum : Enum
        where TObject : UnityEngine.Object
    {
        var dict = new Dictionary<TEnum, TObject>();

        // Label에 해당하는 리소스 location 전부 동기 로드
        var locHandle = Addressables.LoadResourceLocationsAsync(label, typeof(TObject));
        var locations = locHandle.WaitForCompletion();

        if (locations == null || locations.Count == 0)
        {
            Debug.LogWarning($"[Util] No Addressable assets found for label {label}");
            return dict;
        }

        foreach (TEnum e in Enum.GetValues(typeof(TEnum)))
        {
            string enumName = e.ToString();

            foreach (var loc in locations)
            {
                if (loc.PrimaryKey.Equals(enumName, StringComparison.OrdinalIgnoreCase))
                {
                    // Asset 동기 로드
                    var assetHandle = Addressables.LoadAssetAsync<TObject>(loc);
                    var asset = assetHandle.WaitForCompletion();

                    if (asset != null)
                    {
                        dict[e] = asset;
                    }
                    else
                    {
                        Debug.LogWarning($"[Util] Failed to load asset for key: {enumName}");
                        dict[e] = null;
                    }
                    break;
                }
            }

            // 해당 enum 값에 맞는 key를 못 찾았을 때도 null 세팅
            if (!dict.ContainsKey(e))
            {
                Debug.LogWarning($"[Util] No matching asset for Enum {enumName} in label {label}");
                dict[e] = null;
            }
        }

        return dict;
    }

    static public T LoadOneResource<T>(string filePath) where T : UnityEngine.Object
    {
        T[] resources = Resources.LoadAll<T>(filePath);
        if (resources.Length == 0)
        {
            Debug.LogError($"No resources found at {filePath}");
            return null;
        }
        if (resources.Length > 1)
        {
            Debug.LogWarning($"Multiple resources found at {filePath}, returning the first one.");
        }
        return resources[0];
    }

    public static Dictionary<TEnum, TAsset> MapEnumToChildFile<TEnum, TAsset>(string basePath, string fileName)
    where TEnum : Enum
    where TAsset : UnityEngine.Object
    {
        var dict = new Dictionary<TEnum, TAsset>();
        foreach (TEnum e in Enum.GetValues(typeof(TEnum)))
        {
            dict[e] = Resources.Load<TAsset>($"{basePath}/{e}/{fileName}");
        }
        return dict;
    }

    public static T AddOrGetComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }


}
