using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class Define
{
    #region GameSetting
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
    }
    #endregion

    #region System
    public enum Sound
    {
        Bgm,
        SubBgm,
        Effect,
        Max = 10,
    }
    #endregion

    #region Player
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

    public enum PlayerInteractType
    {
        Item,
        ItemBox,
        NPC,
    }
    #endregion

    #region Scene
    public enum Scene
    {
        Pre,
        Unknown,
        TitleScene,
        LobbyScene,
        GameScene,
    }
    #endregion

    #region Event
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerEnter,
        PointerExit,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
    }

    public enum VoidEventType
    {
        OnHPChange,
        OnStaminaChange,
        OnHungerChange,
        OnTempertureChange
    }

    public enum IntEventType
    {

    }
    #endregion

    #region UI
    public enum UI_SliderType
    {
        HP_Slider,
        Stamina_Slider,
        Hunger_Slider,
        Temperture_Slider,

    }
    #endregion

    #region Object
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
    #endregion

    public static int ID_BRONZE_KEY = 201;
    public static int ID_SILVER_KEY = 202;
    public static int ID_GOLD_KEY = 203;
}
