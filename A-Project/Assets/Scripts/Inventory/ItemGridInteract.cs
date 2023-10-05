using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGridInteract : UIPopup
{
    [SerializeField]
    private ItemGrid itemBox;

    void Start()
    {
        BindEvent(gameObject, () => { Debug.Log("UI 감지"); }, null, Define.UIEvent.PointerDown);
        BindEvent(gameObject, () => { Debug.Log("UI 범위 벗어남"); }, null, Define.UIEvent.PointerUp);
    }
}
