using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : EntityController
{
    public PlayerStatController Stat;
    private PlayerMove move;
    private StateMachine<PlayerController> FSM;
    private State<PlayerController>[] States;

    void Start()
    {
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
