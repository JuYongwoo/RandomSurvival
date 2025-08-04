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
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                setDestination(hit.point);
                setPlayerstate(PlayerState.AttackMove_MoveStart);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Transform root = hit.transform.root; //Enemy의 자식 collider를 누르고 root 인 Enemy를 공격하도록 한다.
                if (root.GetComponentInParent<IAttackable>() != null)
                {
                    setAttackTarget(root.gameObject);
                    setPlayerstate(PlayerState.AttackStart);

                }
                else //root에 IAttackable이 없으면 땅, 이동
                {
                    setDestination(hit.point);
                    setPlayerstate(PlayerState.MoveStart);
                }
            }
        }

            return;

    }

}
