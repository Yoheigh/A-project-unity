using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public FOVSystem FOV;
    
    public virtual void Init()
    {
        FOV = new FOVSystem(this.transform);
    }

    public virtual void StartInteract()
    {
        Transform obj = FOV.GetClosestTransform();

        if (obj != null)
        {
            IInteractable interactable = obj.GetComponent<IInteractable>();

            if (interactable == null)
            {
                Debug.Log("�׷��� �ƹ� �ϵ� ������.");
                return;
            }

            interactable.Interact(this);
        }
    }
}
