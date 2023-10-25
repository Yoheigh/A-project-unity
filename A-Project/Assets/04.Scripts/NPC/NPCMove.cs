using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{
    // �⺻ �̵��ӵ�
    public float moveSpeed = 1f;

    // ȸ�� �ӵ�
    public float turnSmoothTime = 0.1f;

    string currentPlane = string.Empty;
    float turnSmoothVelocity;

    CharacterController controller;

    // Animation property
    private AnimationController AC;

    public void Init(AnimationController ac)
    {
        controller = GetComponent<CharacterController>();
        AC = ac;
    }

    public void Move(float xInput, float yInput)
    {
        Vector2 direction = new Vector2(xInput, yInput).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 MoveDir = (Quaternion.Euler(0f, angle, 0f) * Vector3.forward).normalized;

            // moveSpeed�� ���� ������ ���� ��������� ��
            controller.Move(MoveDir * moveSpeed * Time.deltaTime);
            AC.anim.SetBool("Move", true);
        }
        else
        {
            AC.anim.SetBool("Move", false);
        }

        // �߷�
        controller.Move(Vector3.down * 3f);
    }

    public void PlaneCheck()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 1f);

        if (hit.collider == null) return;

        if (currentPlane != hit.collider.gameObject.tag)
        {
            currentPlane = hit.collider.gameObject.tag;

            switch (hit.collider.gameObject.tag)
            {
                case "Snow":
                    AC.ChangeAnimationLayer(AnimationLayerType.Snow, 1, 1f);
                    moveSpeed = 1.7f;
                    break;
                default:
                    AC.ChangeAnimationLayer(AnimationLayerType.Snow, 0, 1f);
                    moveSpeed = 3.4f;
                    break;
            }
        }
    }

    public void LookAtSlowly(Transform target, float speed = 1f)
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        dir.y = 0;

        var nextRot = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, nextRot, Time.deltaTime * speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    }
}
