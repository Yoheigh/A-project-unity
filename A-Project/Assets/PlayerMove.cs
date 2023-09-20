using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float _animModifier = -0.3f;

    CharacterController controller;

    Animator anim;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        anim.SetLayerWeight(1, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.anyKey)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
            anim.speed = moveSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            controller.Move(Vector3.forward * moveSpeed * Time.fixedDeltaTime);
            anim.speed = moveSpeed + _animModifier;
        }

        if (Input.GetKey(KeyCode.S))
        {
            controller.Move(Vector3.back * moveSpeed * Time.fixedDeltaTime);
        }
    }

}
