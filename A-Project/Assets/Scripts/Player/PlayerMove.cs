using Cinemachine.PostFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static Define;

public class PlayerMove : MonoBehaviour
{
    // 기본 이동속도
    public float moveSpeed = 1f;

    // 회전 속도
    public float turnSmoothTime = 0.1f;

    string currentPlane = string.Empty;
    float turnSmoothVelocity;

    CharacterController controller;
    AnimationController ac = new AnimationController();
    Animator anim => ac.anim;
    Transform cam;
    CinemachinePostProcessing volume;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        ac.anim = GetComponentInChildren<Animator>();
        volume = FindObjectOfType<CinemachinePostProcessing>();

        anim.SetLayerWeight(1, 1f);

        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlaneCheck();
        Move();
        ac.UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.U)) { ac.Play(AnimationUpperBody.HandWave); }

        // controller.Move(new Vector3(0f, -2f, 0f) * Time.deltaTime);
    }

    void Move()
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
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }

        // 중력
        controller.Move(Vector3.down * 3f);
    }

    void PlaneCheck()
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
                    ac.ChangeAnimationLayer(AnimationLayerType.Snow, 1, 0.6f);
                    moveSpeed = 1.7f;
                    break;
                default:
                    ac.ChangeAnimationLayer(AnimationLayerType.Snow, 0, 0.9f);
                    moveSpeed = 2.5f;
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    }
}