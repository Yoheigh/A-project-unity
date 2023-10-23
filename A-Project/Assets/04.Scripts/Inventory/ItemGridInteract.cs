using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGridInteract : UIPopup
{
    [SerializeField]
    private ItemGrid itemBox;

    void Start()
    {
        BindEvent(gameObject, () => { Debug.Log("UI ����"); }, null, Define.UIEvent.PointerDown);
        BindEvent(gameObject, () => { Debug.Log("UI ���� ���"); }, null, Define.UIEvent.PointerUp);
    }
}
