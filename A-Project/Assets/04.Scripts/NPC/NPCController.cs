using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NPCController : EntityController, IInteractable
{
    public NPCMove Move;
    // public NPCInteract Interact;
    public NPCStat Stat;
    public NPCNavigator Navi;

    private StateMachine<NPCController> FSM;
    private State<NPCController>[] States;

    public bool Interact(Interactor interactor, Action callback)
    {
        Debug.Log("다이얼로그 실행");
        callback?.Invoke();
        return true;
    }

    void Start()
    {
        AC = new AnimationController();
        AC.anim = GetComponentInChildren<Animator>();

        Move = GetComponent<NPCMove>();
        Navi = GetComponent<NPCNavigator>();
        Stat = GetComponent<NPCStat>();
        //Interact = GetComponent<NPCInteract>();

        Move.Init(AC);
        Navi.Init();
        Stat.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Stat.GlobalStatus == Define.CharacterGlobalStatus.Normal)
        {
            Vector2 next = Navi.GetNextLocation();
            Move.Move(next.x, next.y);
        }
        else
        {
            Move.LookAtSlowly(Managers.Object.Player.transform);
            Move.Move(0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { Stat.ChangeGlobalStatus(Define.CharacterGlobalStatus.Normal); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { Stat.ChangeGlobalStatus(Define.CharacterGlobalStatus.Dialoguing); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { Managers.Event.Invoke(Define.IntEventType.OnSubtitleChange, 1); }
    }
}