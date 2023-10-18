using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using PlayerOwnedStates;

public enum PlayerState
{
    Default,
    Falling,
    Resting,
    Dialoging,
}

public class PlayerController : EntityController
{
    public PlayerStatController Stat;
    public PlayerMove Move;
    private StateMachine<PlayerController> FSM;
    private State<PlayerController>[] States;

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        Move = GetComponent<PlayerMove>();

        FSM = new StateMachine<PlayerController>();

        States = new State<PlayerController>[Enum.GetValues(typeof(PlayerState)).Length - 1];
        States[0] = new Default();
        States[1] = new State<PlayerController>() as Falling;
        States[2] = new State<PlayerController>();

        FSM.Setup(this, States[(int)PlayerState.Default]);

        return base.Init() == true ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        FSM.Execute();
    }
}