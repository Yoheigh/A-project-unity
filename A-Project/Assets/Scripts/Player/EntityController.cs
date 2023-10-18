using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public AnimationController AC;

    private bool _init = false;

    public virtual bool Init()
    {
        if(_init == false)
        {
            AC = new AnimationController();
            return true;
        }
        return _init;
    }
}
