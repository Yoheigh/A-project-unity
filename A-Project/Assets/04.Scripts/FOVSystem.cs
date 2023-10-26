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

    public float viewRadius = 3;                // �þ� �Ÿ�
    public float viewAngle = 180;                 // �þ� ��
    public float interactionRadius = 3f;    // ��ȣ�ۿ� �Ÿ�
    public float refreshDelay = 0.1f;       // ��Ž�� �ð�

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();      // ���̴� Ÿ�� ����Ʈ

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
        // �þ� �ȿ� ������Ʈ�� ������ return null;
        if (visibleTargets.Count == 0) return null;

        Transform returnValue = null;
        //float closestDistance = Mathf.Infinity;

        //for (int i = 0; i < visibleTargets.Count; i++)
        //{
        //    Transform _target = visibleTargets[i];
        //    float dstToTarget = Vector3.Distance(transform.position, _target.position);

        //    Vector3 dirToTarget = (_target.position - transform.position).normalized;

        //    // Debug.DrawLine(transform.position, transform.position + dirToTarget, Color.red, refreshDelay);

        //    // ��ȣ�ۿ��� ������Ʈ�� ���ͷ����� �� �ִ� ���� �ȿ� ������ ���
        //    if (Physics.Raycast(transform.position, dirToTarget, interactionRadius, targetMask))   // ��ֹ��� �տ� �ִ���
        //    {
        //        // ������ �ִ� ������Ʈ �� ���� ����� �� ����
        //        if (dstToTarget < closestDistance)
        //        {
        //            returnValue = _target;
        //            closestDistance = dstToTarget;
        //        }
        //    }
        //}
        returnValue = visibleTargets[0];
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

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))   // ��ֹ��� �տ� �ִ���
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