using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGridInteract : UIPopup
{
    [SerializeField]
    private ItemGrid itemBox;

    void Start()
    {
        BindEvent(gameObject, () => { Debug.Log("UI ����"); }, null, Define.UIEvent.PointerEnter);
        BindEvent(gameObject, () => { Debug.Log("UI ���� ���"); }, null, Define.UIEvent.PointerExit);
        BindEvent(gameObject, () => {
            Vector2 mousePos = Camera.main.ViewportToScreenPoint(Input.mousePosition);
            Debug.Log(mousePos);
            Vector2Int tilePos = itemBox.GetTileMousePosition(mousePos);
            
            itemBox.PickUpItem(tilePos.x, tilePos.y);
        }, (args) => { Debug.Log("�巡������"); }, Define.UIEvent.PointerDown);
    }
}
