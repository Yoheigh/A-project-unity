using Cinemachine.PostFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static Define;

public class PlayerMove : MonoBehaviour
{
    AnimationController AC => Managers.Object.Player.AC;
    PlayerStatController Stat => Managers.Object.Player.Stat;

    // 기본 이동속도
    public float moveSpeed = 1f;
    public float moveSpeedByNormal = 0f;

    // 회전 속도
    public float turnSmoothTime = 0.1f;

    string currentPlane = string.Empty;
    float turnSmoothVelocity;

    public CharacterController controller;
    Transform cam;

    // Animation Parameter
    public float _animMoveSpeed { get; private set; }

    public void Init()
    {
        controller = GetComponent<CharacterController>();

        cam = Camera.main.transform;
    }

    public bool IsSprinting()
    {
        if (Stat.isSprintable == false)
        {
            return false;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
                Stat.isSprinting = true;
            else
                Stat.isSprinting = false;
        }
        return Stat.isSprinting;
    }

    public void Move(float xInput, float yInput)
    {
        Vector2 direction = new Vector2(xInput, yInput).normalized;

        moveSpeed = IsSprinting() ? 2.1f : 0.7f;
        float moveSpeedAnim = moveSpeed >= 2 ? 1f : 0f;
        AC.anim.SetFloat("MoveSpeed", moveSpeedAnim);
        moveSpeed += moveSpeedByNormal;


        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 MoveDir = (Quaternion.Euler(0f, angle, 0f) * Vector3.forward).normalized;

            // moveSpeed를 지형 지물에 따라서 변경해줘야 함
            controller.Move(MoveDir * moveSpeed * Time.deltaTime);

            Managers.Object.Player.Stat.isMoving = true;
            AC.anim.SetBool("Move", true);
        }
        else
        {
            Managers.Object.Player.Stat.isMoving = false;
            AC.anim.SetBool("Move", false);
        }

        // 중력
        controller.Move(Vector3.down * 3f);
    }

    public void PlaneCheck()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 1f);

        if (hit.collider == null) return;

        IsNormalFacingUp(hit);

        if (currentPlane != hit.collider.gameObject.tag)
        {
            currentPlane = hit.collider.gameObject.tag;

            switch (hit.collider.gameObject.tag)
            {
                case "Snow":
                    AC.ChangeAnimationLayer(AnimationLayerType.Snow, 1);
                    Managers.Env.SnowStorm();
                    Stat.isSprintable = false;
                    break;
                default:
                    AC.ChangeAnimationLayer(AnimationLayerType.Snow, 0);
                    Managers.Env.Normal();
                    Stat.TryToChangeParameter(Stat.isSprintable, true);
                    break;
            }
        }
    }

    bool IsNormalFacingUp(RaycastHit hit)
    {
        float angle = Vector3.Angle(hit.normal, Vector3.up);

        moveSpeedByNormal = 1 - Mathf.Lerp(0, 1, angle / 90f);
        return Vector3.Dot(hit.normal, Vector3.up) > 0.8f; // 정확한 값은 상황에 따라 조정할 수 있음
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    }
}