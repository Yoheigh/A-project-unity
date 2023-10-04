using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class Define
{
    public enum Scene
    {
        Unknown,
        TitleScene,
        LobbyScene,
        GameScene,
    }

    public enum Sound
    {
        Bgm,
        SubBgm,
        Effect,
        Max,
    }

    public enum PlayerState
    {
        Normal,
        Rest,
        Damaged,
        Died
    }

    public enum PlayerMoveType
    {
        Default,
        Slippery
    }

    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
    }

    public enum ObjectType
    {
        Player,
        Monster,
    }

    public enum ItemType
    {
        Armor,
        Food,
        Tool
    }

    public static int ID_BRONZE_KEY = 201;
    public static int ID_SILVER_KEY = 202;
    public static int ID_GOLD_KEY = 203;
}
