using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : class
{
    private T owner;
    private State<T> currentState;

    public void Setup(T owner, State<T> entryState)
    {
        this.owner = owner;
        this.currentState = entryState;
    }

    public void Execute()
    {
        if (currentState != null)
        {
            currentState.Execute(owner);
        }
    }

    public void ChangeState(State<T> newState)
    {
        if (newState == null) return;
        if (currentState != null)
        {
            currentState.Exit(owner);
        }

        currentState = newState;
        currentState.Enter(owner);
    }
}

public class State<T>
{
    public virtual void Enter(T entity) { }
    public virtual void Execute(T entity) { }
    public virtual void Exit(T entity) { }
}