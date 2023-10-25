using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Define;

public class PlayerInteract : Interactor
{
    public virtual void StopInteract()
    {

    }

    //public void Interact(PlayerInteractType type)
    //{
    //    switch (type)
    //    {
    //        case PlayerInteractType.Item:
    //            InteractWithItem(); break;

    //        case PlayerInteractType.ItemBox:
    //            InteractWithItemBox(); break;

    //        case PlayerInteractType.NPC:
    //            InteractWithNPC(); break;
    //    }
    //}

    public void InteractWithItem()
    {

    }

    public void InteractWithItemBox()
    {

    }

    public void InteractWithNPC()
    {

    }
}

public interface IInteractable
{
    public bool Interact(Interactor interactor, Action callback = null);
}