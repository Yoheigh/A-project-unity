using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public enum PlayerState
{
    Default,
    Ragdoll,
    Falling,
    Boarding,
    Resting,
    Dialoging,
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
}