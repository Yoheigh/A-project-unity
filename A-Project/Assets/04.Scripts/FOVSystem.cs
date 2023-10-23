using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class FOVSystem
{
    public FOVSystem(Transform transform)
    {
        this.transform = transform;
    }

    public Transform transform;

    public float viewRadius;                // 시야 거리
    public float viewAngle;                 // 시야 각
    public float interactionRadius = 1f;    // 상호작용 거리
    public float refreshDelay = 0.1f;       // 재탐색 시간

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();      // 보이는 타겟 리스트

    private Coroutine co;

    public void StartFindTargets()
    {
        if (co != null)
            CoroutineManager.StopCoroutine(co);

        co = CoroutineManager.StartCoroutine(FindTargetsWithDelay(refreshDelay));
    }

    public void StopFindTargets()
    {
        CoroutineManager.StopCoroutine(co);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
            FindVisibleTargets();
        }
    }

    public void ChangeStartTransform(Transform transform)
    {
        this.transform = transform;
    }

    public void SetTargetLayer(LayerMask targetLayer)
    {
        targetMask = targetLayer;
    }

    public Transform GetClosestTransform()
    {
        // 시야 안에 오브젝트가 없으면 return null;
        if (visibleTargets.Count == 0) return null;

        Transform returnValue = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < visibleTargets.Count; i++)
        {
            Transform _target = visibleTargets[i];
            float dstToTarget = Vector3.Distance(transform.position, _target.position);

            Vector3 dirToTarget = (_target.position - transform.position).normalized;

            Debug.DrawLine(transform.position, transform.position + dirToTarget, Color.red, refreshDelay);

            // 상호작용할 오브젝트가 인터랙션할 수 있는 범위 안에 들어왔을 경우
            if (Physics.Raycast(transform.position, dirToTarget, interactionRadius, targetMask))   // 장애물이 앞에 있는지
            {
                // 범위에 있는 오브젝트 중 가장 가까운 걸 리턴
                if (dstToTarget < closestDistance)
                {
                    returnValue = _target;
                    closestDistance = dstToTarget;
                }
            }
        }
        return returnValue;
    }

    void FindVisibleTargets()
    {
        ClearTargets();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)                      // ?
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))   // 장애물이 앞에 있는지
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public void ClearTargets()
    {
        visibleTargets.Clear();
    }

    public Vector3 DirFromAngle(float angleInDegress, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegress += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegress * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegress * Mathf.Deg2Rad));
    }
}