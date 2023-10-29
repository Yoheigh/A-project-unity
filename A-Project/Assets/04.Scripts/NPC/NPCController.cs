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

    private List<string[]> dialogues = new List<string[]>();
    public int dialogIndex = -1;
    public int _accessibleIndex = 4;

    private string[] strings1 = new string[]
    {
        "대체 얼마나 멀리까지 왔는지도 모르겠군.",
        "이 지긋지긋한 광경은 언제까지 지속되는 건지...",
        "길 잃지 않게 날 잘 따라오도록 하고.",
    };

    private string[] strings2 = new string[]
    {
        "가방에 뭐라도 먹을 게 남았나?",
        "가방은 'i'키로 열 수 있어.",
        "한 번 확인해 봐."
    };

    private string[] strings3 = new string[]
    {
        "역시 든 게 하나도 없나",
        "나도 더 이상 가지고 있는 게 없는데.",
    };

    private string[] strings4 = new string[]
    {
        "'Q'키를 눌러 손을 흔들어보는 건 어때?",
        "운이 좋다면 이쪽을 보고 반응해줄지도 모르지.",
    };

    private string[] strings5 = new string[]
    {
        "말하니 조금 잠이 깨는군.",
        "지금부턴 말할 체력도 아껴야겠어.",
        "뭔가 보일 때까지 대화는 중단하자.",
    };

    private int index = -1;

    public Define.PlayerInteractType Type { get => Define.PlayerInteractType.NPC; }

    public bool Interact(Interactor interactor, Action callback)
    {
        if (dialogIndex == _accessibleIndex)
        {
            Debug.Log("최대 접근 가능한 index와 동일합니다");
            return false;
        }

        if (dialogIndex < dialogues.Count)
            dialogIndex++;

        index = 0;
        Stat.ChangeGlobalStatus(Define.CharacterGlobalStatus.Dialoguing);

        // 다이얼로그 시작과 같음
        Managers.UI.ShowSubtitle<UIDialogueSubtitle>(dialogues[dialogIndex][index]);


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
        if (index < dialogues[dialogIndex].Length - 1)
        {
            index++;
            Managers.UI.ShowSubtitle<UIDialogueSubtitle>(dialogues[dialogIndex][index]);
            return true;
        }
        else
        {
            Managers.UI.CloseSubtitle();
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

        dialogues.Add(strings1);
        dialogues.Add(strings2);
        dialogues.Add(strings3);
        dialogues.Add(strings4);
        dialogues.Add(strings5);
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