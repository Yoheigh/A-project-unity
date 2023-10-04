using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootIKTest : MonoBehaviour
{
    public Vector3 _LeftFootPos;
    public Vector3 _RightFootPos;

    public AnimationCurve _LeftFootCurve;
    public AnimationCurve _RightFootCurve;

    [Range(0, 1f)]
    public float DistanceToGround;

    public Animator anim;
    public LayerMask playerLayer;

    void Start()
    {
        // _LeftFootPos = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        //_LeftFootPos = anim.GetIKPosition(AvatarIKGoal.LeftFoot);
        //_RightFootPos = anim.GetIKPosition(AvatarIKGoal.RightFoot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if(anim)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f); // float �κп� Ŀ�� �ְų� Ʈ�� �ְų� �ϸ� ��
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);

            // LeftFoot
            RaycastHit hit;
            Ray ray = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            // �� �ؿ����� ���ø� �Լ� ������� �� ����
            // ���ʿ� �̴�� �۵� �� �� ����
            // bool leftFootCheck = Physics.Raycast(_LeftFootPos + Vector3.up, Vector3.down * DistanceToGround, out leftFootHit, ~playerLayer);
            if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, ~playerLayer) == true)
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    Vector3 leftFootPos = hit.point;
                    leftFootPos.y += DistanceToGround;
                    anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);
                    anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }

            // RightFoot
            ray = new Ray(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, ~playerLayer) == true)
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    Vector3 rightFootPos = hit.point;
                    rightFootPos.y += DistanceToGround;
                    anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);
                    anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.down);
        Gizmos.DrawLine(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.down);
    }
}