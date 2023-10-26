using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Define;

public class Interactor : MonoBehaviour
{
    public FOVSystem FOV;

    public virtual void Init()
    {
        FOV = new FOVSystem(this.transform);
        FOV.targetMask = LayerMask.GetMask("Interactable");
    }

    public virtual void StartInteract(IInteractable interactable)
    {
        
    }

    public virtual bool TryToInteract(out IInteractable interact)
    {
        Transform obj = FOV.GetClosestTransform();

        if (obj != null)
        {
            IInteractable interactable;

            if (obj.TryGetComponent<IInteractable>(out interactable))
            {
                interact = interactable;
                return true;
            }
        }
        interact = null;
        return false;
    }
}
