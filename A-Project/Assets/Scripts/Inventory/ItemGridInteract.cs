using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGridInteract : UIPopup
{
    [SerializeField]
    private ItemGrid itemBox;

    void Start()
    {
        BindEvent(gameObject, () => { Debug.Log("UI 감지"); }, null, Define.UIEvent.PointerEnter);
        BindEvent(gameObject, () => { Debug.Log("UI 범위 벗어남"); }, null, Define.UIEvent.PointerExit);
        BindEvent(gameObject, () => {
            Vector2 mousePos = Camera.main.ViewportToScreenPoint(Input.mousePosition);
            Debug.Log(mousePos);
            Vector2Int tilePos = itemBox.GetTileMousePosition(mousePos);
            
            itemBox.PickUpItem(tilePos.x, tilePos.y);
        }, (args) => { Debug.Log("드래그중임"); }, Define.UIEvent.PointerDown);
    }
}
