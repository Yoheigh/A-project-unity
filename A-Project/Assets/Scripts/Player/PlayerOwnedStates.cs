using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

namespace PlayerOwnedStates
{
    public class Default : State<PlayerController>
    {
        public override void Enter(PlayerController entity) 
        { 

        }
        public override void Execute(PlayerController entity) 
        {
            entity.Move.PlaneCheck();
            entity.Move.Move();
            entity.AC.UpdateAnimation();

            if (Input.GetKeyDown(KeyCode.U)) { entity.AC.Play(AnimationUpperBody.HandWave); }
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