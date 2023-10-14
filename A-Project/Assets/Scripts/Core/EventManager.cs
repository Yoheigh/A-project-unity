using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class EventManager
{
    public Dictionary<VoidEventType, Action> OnVoidHandlers = new Dictionary<VoidEventType, Action>();
    public Dictionary<IntEventType, Action<int>> OnIntHandlers = new Dictionary<IntEventType, Action<int>>();
    // public Action<VoidEventType> OnVoid;

    public void Init()
    {
        VoidEventType[] voidEnums = (VoidEventType[])Enum.GetValues(typeof(VoidEventType));
        foreach (VoidEventType value in voidEnums)
        {
            Debug.Log(value);
            if(!OnVoidHandlers.ContainsKey(value))
                OnVoidHandlers.Add(value, null);
        }

        IntEventType[] intEnums = (IntEventType[])Enum.GetValues(typeof(IntEventType));
        foreach (IntEventType value in intEnums)
        {
            Debug.Log(value);
            if(!OnIntHandlers.ContainsKey(value))
                OnIntHandlers.Add(value, null);
        }
    }

    public void AddEvent(VoidEventType eventType, Action _event)
    {
        if (OnVoidHandlers.ContainsKey(eventType))
        {
            OnVoidHandlers[eventType] -= _event;
            OnVoidHandlers[eventType] += _event; 
        }
    }

    public void RemoveEvent(VoidEventType eventType, Action _event)
    {
        if(OnVoidHandlers.ContainsKey(eventType))
        { 
            OnVoidHandlers[eventType] -= _event;
        }
    }

    public void Invoke(VoidEventType eventType)
    {
        if (OnVoidHandlers.ContainsKey(eventType))
        {
            OnVoidHandlers[eventType]?.Invoke();
        }
    }

    public void AddEvent(IntEventType eventType, Action<int> _event)
    {
        if (OnIntHandlers.ContainsKey(eventType))
        {
            OnIntHandlers[eventType] -= _event;
            OnIntHandlers[eventType] += _event;
        }
    }

    public void RemoveEvent(IntEventType eventType, Action<int> _event)
    {
        if (OnIntHandlers.ContainsKey(eventType))
        {
            OnIntHandlers[eventType] -= _event;
        }
    }

    public void Invoke(IntEventType eventType, int value)
    {
        if (OnIntHandlers.ContainsKey(eventType))
        {
            OnIntHandlers[eventType]?.Invoke(value);
        }
    }
}