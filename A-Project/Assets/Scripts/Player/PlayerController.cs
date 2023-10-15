using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : MonoBehaviour
{
    public PlayerStatController Stat;
    private PlayerMove move;
    private StateMachine<PlayerController> FSM;
    private State<PlayerController>[] States; 

    void Start()
    {
        Init();
    }

    private bool Init()
    {
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}