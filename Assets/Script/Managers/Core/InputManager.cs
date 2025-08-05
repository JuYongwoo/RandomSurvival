using System;
using UnityEngine;

public class InputManager
{
    public Action Enter = null;
    public Action<GameObject> Attack = null;
    public Action MovingAttack = null;
    public static Action<PlayerState> setPlayerstate;
    public static Action<Vector3> setDestination;
    public static Action<GameObject> setAttackTarget;


    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Enter.Invoke();

        if (Input.GetKeyDown(KeyCode.A))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask mask = LayerMask.GetMask("Ground");

            if (Physics.Raycast(ray, out RaycastHit hit, 100, mask))
            {
                setDestination(hit.point);
                setPlayerstate(PlayerState.AttackMove_MoveStart);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask mask = LayerMask.GetMask("EnemyClick", "Ground");
            if (Physics.Raycast(ray, out RaycastHit hit, 100, mask))
            {
                Transform parent = hit.transform; //Enemy의 자식 collider를 누르고 parent인 Enemy를 공격하도록 한다.
                if (parent.GetComponentInParent<EnemyBase>() != null)
                {
                    setAttackTarget(parent.gameObject);
                    setPlayerstate(PlayerState.AttackStart);

                }
                else
                {
                    setDestination(hit.point);
                    setPlayerstate(PlayerState.MoveStart);
                }
            }
        }

            return;

    }

}
