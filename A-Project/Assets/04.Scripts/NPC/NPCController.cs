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

    private string[] strings = new string[]
    {
        "반갑네.",
        "Korea 사람 불만 있어요?",
        "카호 안 뽑은 You 에게 regret 있으리",
        "내 할 말 끝났소"
    };
    private int index = -1;

    public Define.PlayerInteractType Type { get => Define.PlayerInteractType.NPC; }

    public bool Interact(Interactor interactor, Action callback)
    {
        index = 0;

        Stat.ChangeGlobalStatus(Define.CharacterGlobalStatus.Dialoguing);

        // 다이얼로그 시작과 같음
        Managers.UI.ShowSubtitle<UIDialogueSubtitle>(strings[index]);


        // 다음 다이얼로그 연결 index가 더 없으면
        // Stat.ChangeGlobalStatus(Define.CharacterGlobalStatus.Normal);
        return true;
    }

    public void EndInteract()
    {
        Stat.ChangeGlobalStatus(Define.CharacterGlobalStatus.Normal);
    }

    public bool NextLine()
    {
        if (index < strings.Length - 1)
        {
            index++;
            Managers.UI.ShowSubtitle<UIDialogueSubtitle>(strings[index]);
            return true;
        }
        else
        {
            index = -1;
            return false;
        }

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
    }
}