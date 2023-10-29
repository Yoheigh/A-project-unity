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

    public StateMachine<PlayerController> FSM;
    public State<PlayerController>[] States;

    // �ش� index�� �̺�Ʈ�� �Ϸ�Ǹ�, �� �̻� �Ͼ�� �ʴ´�.
    // ���� �̰� ����� �� �� �ƴ� �������� �� �̰�
    public int _eventIndex = 0;

    private void Start()
    {
        Init();
    }

    public override bool Init()
    {
        AC = new AnimationController();
        AC.anim = GetComponentInChildren<Animator>();

        Move = GetComponent<PlayerMove>();
        Interact = GetComponent<PlayerInteract>();
        Stat = GetComponent<PlayerStatController>();

        Move.Init();
        Stat.Init();
        Interact.Init();

        FSM = new StateMachine<PlayerController>();

        States = new State<PlayerController>[Enum.GetValues(typeof(PlayerState)).Length - 1];
        States[(int)PlayerState.Default] = new Default();
        States[(int)PlayerState.Falling] = new Falling();
        States[(int)PlayerState.Ragdoll] = new Ragdoll();
        States[(int)PlayerState.Dialoging] = new Dialoging();

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