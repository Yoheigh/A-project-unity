using Cinemachine.PostFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static Define;

public class PlayerMove : MonoBehaviour
{
    AnimationController AC => Managers.Object.Player.AC;

    // 기본 이동속도
    public float moveSpeed = 1f;

    // 회전 속도
    public float turnSmoothTime = 0.1f;

    string currentPlane = string.Empty;
    float turnSmoothVelocity;

    CharacterController controller;
    Transform cam;
    CinemachinePostProcessing volume;

    public void Init()
    {
        controller = GetComponent<CharacterController>();
        AC.anim = GetComponentInChildren<Animator>();
        volume = FindObjectOfType<CinemachinePostProcessing>();

        cam = Camera.main.transform;
    }

    public void Move()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 MoveDir = (Quaternion.Euler(0f, angle, 0f) * Vector3.forward).normalized;

            // moveSpeed를 지형 지물에 따라서 변경해줘야 함
            controller.Move(MoveDir * moveSpeed * Time.deltaTime);
            AC.anim.SetBool("Move", true);
        }
        else
        {
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    }
}