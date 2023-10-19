using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Define;

public class PlayerInteract : MonoBehaviour
{
    public FOVSystem FOV;

    public void Init()
    {
        FOV = new FOVSystem(this.transform);
    }

    public void StartInteract()
    {
        Transform obj = FOV.GetClosestTransform();

        if (obj != null)
        {
            // Interactable interactable = obj.GetComponent<Interactable>();
            // Interact(interactable);
        }
    }

    public void StopInteract()
    {

    }

    public void Interact(PlayerInteractType type)
    {
        switch (type)
        {
            case PlayerInteractType.Item:
                InteractWithItem(); break;

            case PlayerInteractType.ItemBox:
                InteractWithItemBox(); break;

            case PlayerInteractType.NPC:
                InteractWithNPC(); break;
        }
    }

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