using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Define;

public class PlayerInteract : Interactor
{
    public IInteractable currentInteract;

    public PlayerInteractType InteractionCheck()
    {
        if (TryToInteract(out IInteractable obj))
        {
            if (obj.Interact(this) == true)
            {
                StartInteract(obj);
                return obj.Type;
            }
            else
                return PlayerInteractType.None;
        }
        else
            return PlayerInteractType.None;
    }

    public override void StartInteract(IInteractable interactable)
    {
        currentInteract = interactable;
    }

    public virtual void StopInteract()
    {
        currentInteract = null;
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
    public PlayerInteractType Type { get; }

    public bool Interact(Interactor interactor, Action callback = null);
    public void EndInteract();
    public bool NextLine();
}