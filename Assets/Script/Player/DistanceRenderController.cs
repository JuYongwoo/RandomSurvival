using System.Collections.Generic;
using UnityEngine;

public class DistanceRenderController : MonoBehaviour
{
    private GameObject target;
    private const float disableDistance = 30f;
    private List<Renderer> targetRenderers = new List<Renderer>();


    private void Start()
    {
        target = this.gameObject; // 현재 게임 오브젝트를 타겟으로 설정

        Renderer[] allRenderers = FindObjectsOfType<Renderer>();
        foreach (var r in allRenderers)
        {
            if (!(r is SpriteRenderer) && r.gameObject.layer != LayerMask.NameToLayer("UI"))
                targetRenderers.Add(r);
        }
    }

    private void Update()
    {
        Vector3 camPos = target.transform.position;

        foreach (var rend in targetRenderers)
        {
            if (rend == null) continue; // 삭제된 오브젝트 예외 처리

            float dist = Vector3.Distance(camPos, rend.transform.position);
            bool shouldEnable = dist <= disableDistance;

            if (rend.enabled != shouldEnable)
                rend.enabled = shouldEnable;
        }
    }
}
