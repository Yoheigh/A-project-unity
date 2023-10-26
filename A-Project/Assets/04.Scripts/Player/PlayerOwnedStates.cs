using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public enum PlayerState
{
    Default,
    Ragdoll,
    Falling,
    Dialoging,
    Boarding,
    Resting,
}

namespace PlayerOwnedStates
{
    public class Default : State<PlayerController>
    {
        public override void Enter(PlayerController entity) 
        {
            entity.Interact.FOV.StartFindTargets();
        }
        public override void Execute(PlayerController entity) 
        {
            entity.Move.PlaneCheck();
            entity.Move.Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if(Input.GetKeyDown(KeyCode.E))
            {
                PlayerInteractType interactType = entity.Interact.InteractionCheck();

                switch(interactType)
                {
                    case PlayerInteractType.None:
                        Debug.Log("어이 니 눈엔 지금 상호작용할 게 보이냐");
                        break;
                    case PlayerInteractType.Item:
                        break;
                    case PlayerInteractType.ItemBox:
                        break;
                    case PlayerInteractType.NPC:
                        entity.FSM.ChangeState(entity.States[(int)PlayerState.Dialoging]);
                        break;
                }
            }

            entity.AC.UpdateAnimation();

            if (Input.GetKeyDown(KeyCode.U)) { entity.AC.Play(AnimationUpperBody.HandWave); }
            if (Input.GetKeyDown(KeyCode.I)) { entity.AC.Play(AnimationUpperBody.Drink); }
        }
        public override void Exit(PlayerController entity) { }
    }

    public class Ragdoll : State<PlayerController>
    {
        public override void Enter(PlayerController entity) { }
        public override void Execute(PlayerController entity)
        {

        }
        public override void Exit(PlayerController entity) { }
    }

    public class Falling : State<PlayerController>
    {
        public override void Enter(PlayerController entity) { }
        public override void Execute(PlayerController entity) 
        { 

        }
        public override void Exit(PlayerController entity) { }
    }

    public class Dialoging : State<PlayerController>
    {
        public override void Enter(PlayerController entity) 
        {
            Debug.Log("다이얼로그 중");
            entity.Move.Move(0f, 0f);
        }

        public override void Execute(PlayerController entity)
        {
            if(Input.anyKeyDown)
            {
                if(!entity.Interact.currentInteract.NextLine())
                {
                    entity.FSM.ChangeState(entity.States[(int)PlayerState.Default]);
                    entity.Interact.currentInteract.EndInteract();
                }
            }

            entity.AC.UpdateAnimation();
        }
        public override void Exit(PlayerController entity) 
        {
            Debug.Log("다이얼로그 종료!!!!!!");
        }
    }
}