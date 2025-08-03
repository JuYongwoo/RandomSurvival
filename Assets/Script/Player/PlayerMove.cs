using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private AudioClip sprintsound;

    private bool isSprinting = false;
    private bool isSprintReady = true;
    private Coroutine sprintRoutine = null;

    public static Action<float> OnRefreshStaminaBar;

    private void Awake()
    {
        PlayerAttack.moveStop = () => { agent.destination = gameObject.transform.position; };
        ManagerObject.input.GroundMouseAction = HandleClickInput;
        sprintsound = Resources.Load<AudioClip>("sprintsound");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.acceleration = 1000f;
        agent.angularSpeed = 0f;
        agent.autoBraking = false;
        agent.stoppingDistance = 0f;

        Enemy.isplayersprinting = () => isSprinting;
    }

    private void Update()
    {
        HandleSprintInput();
        RefreshStamina();

        if (!agent.pathPending && agent.remainingDistance > 0.1f)
        {
            Vector3 dir = agent.desiredVelocity.normalized;

            if (dir.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(dir);
                transform.position += dir * agent.speed * Time.deltaTime;
            }

            animator.SetFloat("Speed", agent.speed);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    private void HandleClickInput(RaycastHit hit)
    {
        agent.SetDestination(hit.point);
    }

    private void RefreshStamina()
    {
        var stat = ManagerObject.playerStatObj.playerCurrentStat;
        var baseStat = ManagerObject.playerStatObj.playerBaseStat;

        if (Input.GetKey(KeyCode.LeftShift) && stat.Stamina > 0)
        {
            AdjustStamina(-Time.deltaTime * baseStat.StaminaDrainRate);
        }
        else if (stat.Stamina < baseStat.MaxStamina)
        {
            AdjustStamina(Time.deltaTime * baseStat.StaminaRegenRate);
        }
    }

    private void AdjustStamina(float delta)
    {
        var stat = ManagerObject.playerStatObj.playerCurrentStat;
        var baseStat = ManagerObject.playerStatObj.playerBaseStat;

        stat.Stamina = Mathf.Clamp(stat.Stamina + delta, 0f, baseStat.MaxStamina);
        OnRefreshStaminaBar?.Invoke(stat.Stamina);
    }

    private void HandleSprintInput()
    {
        var stat = ManagerObject.playerStatObj.playerCurrentStat;
        var baseStat = ManagerObject.playerStatObj.playerBaseStat;

        if (Input.GetKeyDown(KeyCode.LeftControl) && isSprintReady && stat.Stamina >= baseStat.SprintStaminaCost)
        {
            if (sprintRoutine == null)
                sprintRoutine = StartCoroutine(SprintRoutine());
        }
    }

    private IEnumerator SprintRoutine()
    {
        var baseStat = ManagerObject.playerStatObj.playerBaseStat;

        isSprinting = true;
        isSprintReady = false;
        AdjustStamina(-baseStat.SprintStaminaCost);

        agent.speed = baseStat.RunSpeed;
        animator.SetBool("Sprint", true);
        ManagerObject.am.PlaySound(sprintsound);

        yield return new WaitForSeconds(baseStat.SprintDuration);

        isSprinting = false;
        agent.speed = baseStat.BaseMoveSpeed;
        animator.SetBool("Sprint", false);

        yield return new WaitForSeconds(baseStat.SprintCooldown);
        isSprintReady = true;
        sprintRoutine = null;
    }
}
