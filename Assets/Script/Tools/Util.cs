using System.Collections.Generic;
using System;
using UnityEngine;

static public class util
{
    static public Dictionary<T, GameObject> mapDictionary<T>(GameObject go)
    {
        Dictionary<T, GameObject>  dict = new Dictionary<T, GameObject>();

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
}