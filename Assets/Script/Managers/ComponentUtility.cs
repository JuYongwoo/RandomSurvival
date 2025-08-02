using UnityEngine;

public static class ComponentUtility
{
    public static T AddOrGetComponent<T>(this GameObject obj) where T : Component
    {
        T component = obj.GetComponent<T>();
        if (component == null)
        {
            component = obj.AddComponent<T>();
        }
        return component;
    }
}
