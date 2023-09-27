using Cinemachine.PostFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float _animModifier = -0.3f;

    public bool _PlaneIsFollowing = true;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    string currentPlane = string.Empty;
    int currentAnimLayer = 1;

    int _changeLayer;
    float _changeWeight;

    public Transform _Plane;

    CharacterController controller;
    Transform cam;
    CinemachinePostProcessing volume;

    Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        volume = FindObjectOfType<CinemachinePostProcessing>();

        anim.SetLayerWeight(1, 1f);
        anim.speed = anim.speed + _animModifier;

        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(_PlaneIsFollowing)
            _Plane.transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        PlaneCheck();
        UpdateAnimation();

        controller.Move(new Vector3(0f, -2f, 0f) * Time.deltaTime);

        if (direction.magnitude > 0.1f)
        {
            Move(direction);
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }

    }

    void Move(Vector2 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 MoveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;

        // moveSpeed를 지형 지물에 따라서 변경해줘야 함
        controller.Move(MoveDir * moveSpeed * Time.deltaTime);
    }

    void PlaneCheck()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, 1f);

        if (hit.collider == null) return;

        if(currentPlane != hit.collider.gameObject.tag)
        {
            currentPlane = hit.collider.gameObject.tag;

            switch (hit.collider.gameObject.tag)
            {
                case "Snow":
                    AnimationChange(1, 1);
                    moveSpeed = 1f;
                    break;
                default:
                    AnimationChange(1, 0);
                    moveSpeed = 1.8f;
                    break;
            }
        }
    }

    void AnimationChange(int layerIndex, float weight)
    {
        _changeLayer = layerIndex;
        _changeWeight = weight;
    }

    void UpdateAnimation()
    {
        float tempWeight = anim.GetLayerWeight(currentAnimLayer);
        if(_changeWeight >= tempWeight)
        {
            tempWeight += Time.deltaTime;
        }
        else if(_changeWeight <= tempWeight )
        {
            tempWeight -= Time.deltaTime;
        }
        anim.SetLayerWeight(_changeLayer, tempWeight);
    }
}
