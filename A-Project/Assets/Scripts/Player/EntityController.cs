using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public AnimationController Anim;

    private bool _init = false;

    void Start()
    {
        
    }

    public virtual bool Init()
    {
        if(_init == false)
        {

            return true;
        }
        return _init;
    }
}
