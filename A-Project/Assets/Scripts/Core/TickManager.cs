using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    private static TickManager instance;
    public static TickManager Instance { get { return instance; } }

    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        if (instance != null)
            return;

        instance = new GameObject($"[{nameof(TickManager)}]").AddComponent<TickManager>();
        DontDestroyOnLoad(instance.gameObject);
    }

    public static event EventHandler<OnTickEventArgs> OnTick;

    private const float TICK_TIMER = 0.2f;

    private int tick;
    private float tickTimer;

    void Update()
    {
        // Ư���� ��Ȳ�� ���ߵ��� �ؾ� ��
        UpdateTick();
    }

    void UpdateTick()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIMER)
        {
            tickTimer -= TICK_TIMER;
            tick++;
            OnTick!.Invoke(this, new OnTickEventArgs() { tick = tick });
        }
    }

    public void AddTickEvent(EventHandler<OnTickEventArgs> action)
    {
        OnTick -= action;
        OnTick += action;
    }
}

public interface ITickEvent
{
    public abstract void OnTickEvent(object sender, OnTickEventArgs args);
}

public class OnTickEventArgs : EventArgs
{
    public int tick;
    //public Action tickEvent;
}

public struct TickArgument
{
    public int tickNeedCounts;
}
