using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action ConfirmKeyAction = null;
    public Action RunKeyAction = null;

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return) && ConfirmKeyAction != null)
            ConfirmKeyAction.Invoke();

        if (Input.GetKeyDown(KeyCode.LeftShift) && RunKeyAction != null)
            RunKeyAction.Invoke();

        if (EventSystem.current.IsPointerOverGameObject())
            return;

    }

    public void Clear()
    {
        ConfirmKeyAction = null;
        RunKeyAction = null;
    }
}
