using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerInventory : UIPopup
{
    private void Start()
    {
        base.Init();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void ClosePopupUI()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        base.ClosePopupUI();
    }
}
