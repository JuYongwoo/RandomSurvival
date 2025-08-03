using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action ConfirmKeyAction = null;
    public Action RunKeyAction = null;
    public Action<RaycastHit> GroundMouseAction = null;
    public Action<GameObject> EnemyMouseAction = null;

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            ConfirmKeyAction.Invoke();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            RunKeyAction.Invoke();

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Transform root = hit.transform.root; //Enemy의 자식 collider를 누르고 root 인 Enemy를 공격하도록 한다.
                if (root.GetComponentInParent<IAttackable>() != null)
                {
                    EnemyMouseAction(root.gameObject);
                }
                else //root에 IAttackable이 없으면 땅, 이동
                {
                    GroundMouseAction(hit);
                }
            }
        }

            return;

    }

    public void Clear()
    {
        ConfirmKeyAction = null;
        RunKeyAction = null;
    }
}
