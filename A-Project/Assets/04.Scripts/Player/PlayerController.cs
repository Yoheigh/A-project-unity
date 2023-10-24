using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using PlayerOwnedStates;

public class PlayerController : EntityController
{
    public PlayerMove Move;
    public PlayerInteract Interact;
    public PlayerStatController Stat;

    private StateMachine<PlayerController> FSM;
    private State<PlayerController>[] States;

    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        AC = new AnimationController();

        Move = GetComponent<PlayerMove>();
        Interact = GetComponent<PlayerInteract>();
        Stat = GetComponent<PlayerStatController>();

        Move.Init();
        // Stat.Init();
        Interact.Init();

        FSM = new StateMachine<PlayerController>();

        States = new State<PlayerController>[Enum.GetValues(typeof(PlayerState)).Length - 1];
        States[0] = new Default();
        States[1] = new Falling();
        States[2] = new State<PlayerController>();

        FSM.Setup(this, States[(int)PlayerState.Default]);
        FSM.ChangeState(States[(int)PlayerState.Default]);

        return base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        FSM.Execute();
    }
}