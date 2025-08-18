using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InputManager
{
    public Action Enter = null;
    public Action<GameObject> Attack = null;
    public Action MovingAttack = null;
    public static Action<PlayerState> setPlayerstate;
    public static Action<Vector3> setDestination;
    public static Action<GameObject> setAttackTarget;

    private Texture2D attackCursor;
    private Texture2D currentCursor = null;

    public void OnAwake()
    {
        attackCursor = Addressables.LoadAssetAsync<Texture2D>("AttackIcon").WaitForCompletion();

    }

    public void OnUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Return))
            Enter.Invoke();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = LayerMask.GetMask("EnemyClick", "Ground");

        if (Physics.Raycast(ray, out RaycastHit hit, 100, mask))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("EnemyClick")) //적 마우스
            {
                setCursor(attackCursor);

                if (Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(1))
                {
                    setAttackTarget(hit.transform.gameObject);
                    setPlayerstate(PlayerState.AttackStart);

                }
                return;
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) //땅 마우스
            {
                setCursor(null);

                if (Input.GetMouseButtonDown(1))
                {
                    setDestination(hit.point);
                    setPlayerstate(PlayerState.MoveStart);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    setDestination(hit.point);
                    setPlayerstate(PlayerState.AttackMove_MoveStart);
                }
                return;

            }
            else
            {
                setCursor(null);
                return;

            }

        }
    }

    private void setCursor(Texture2D attackCursor)
    {
        currentCursor = attackCursor;
        Cursor.SetCursor(currentCursor, Vector2.zero, CursorMode.Auto);
    }
}
