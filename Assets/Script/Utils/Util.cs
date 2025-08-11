using System;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    static public Dictionary<T, GameObject> mapDictionary<T>(GameObject go) where T : Enum
    {
        Dictionary<T, GameObject> dict = new Dictionary<T, GameObject>();

        Transform[] children = go.GetComponentsInChildren<Transform>();

        foreach (T enumName in Enum.GetValues(typeof(T)))
        {
            foreach (Transform child in children)
            {
                if (child.name == enumName.ToString())
                {
                    dict[enumName] = child.gameObject;
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

    public static T AddOrGetComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
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
