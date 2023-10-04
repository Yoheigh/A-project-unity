using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : MonoBehaviour
{
    PlayerMove move;
    StateMachine<PlayerController> FSM;
    private State<PlayerController>[] States; 

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class PlayerStat
{
    // ü��
    public float HP { get { return _hp; } set {; } }

    // ���¹̳�
    public float Stamina { get { return _stamina; } set {; } }

    // �µ�
    public float temperture { get { return _temperture;} set { ;} }
    public bool _CelsiusToFahrenheit = false;

    // ���
    public float Hunger { get { return _hunger; } set {; } }

    // private variable
    private float _hp;
    private float _stamina;
    private float _temperture;
    private float _hunger;

    // ���� ȭ�� ����
    public float CalculateTemperture(float temperture)
    {
        if (_CelsiusToFahrenheit)
            return (temperture * 1.8f + 32);
        else
            return temperture;
    }
}

namespace PlayerOwnedStates
{
    public class Normal : State<PlayerController>
    {
        public override void Enter(PlayerController entity) { }
        public override void Execute(PlayerController entity) { }
        public override void Exit(PlayerController entity) { }
    }
}