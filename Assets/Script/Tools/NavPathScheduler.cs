using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPathScheduler : MonoBehaviour
{
    public static NavPathScheduler I { get; private set; }

    [SerializeField] int maxPathsPerFrame = 2;   // 프레임당 처리 경로 수
    readonly Queue<Job> q = new Queue<Job>();
    readonly Stack<NavMeshPath> pool = new Stack<NavMeshPath>();
    int processedThisFrame;

    void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Driver());
    }

    void LateUpdate() => processedThisFrame = 0;

    IEnumerator Driver()
    {
        var waitEOF = new WaitForEndOfFrame();
        while (true)
        {
            while (q.Count > 0 && processedThisFrame < maxPathsPerFrame)
            {
                var job = q.Dequeue();
                var path = GetPath();

                // 타겟 NavMesh 위 보정 (공중 클릭 등)
                Vector3 target = job.target;
                if (job.sampleTarget &&
                    NavMesh.SamplePosition(target, out var hit, job.sampleMaxDist, job.areaMask))
                    target = hit.position;

                bool ok = NavMesh.CalculatePath(job.origin, target, job.areaMask, path);
                job.onComplete?.Invoke(ok ? path.status : NavMeshPathStatus.PathInvalid, path);

                processedThisFrame++;
            }
            yield return waitEOF;
        }
    }

    NavMeshPath GetPath() => pool.Count > 0 ? pool.Pop() : new NavMeshPath();
    public void ReleasePath(NavMeshPath p) { if (p != null) pool.Push(p); }

    public struct Job
    {
        public Vector3 origin;
        public Vector3 target;
        public int areaMask;
        public bool sampleTarget;
        public float sampleMaxDist;
        public Action<NavMeshPathStatus, NavMeshPath> onComplete;
    }

    public void Request(Job job) => q.Enqueue(job);
}
