using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScene : BaseScene
{
    protected override void Init()
    {
        // var sub = Managers.UI.ShowSubtitle<UIDialogueSubtitle>("ÀÚ¸·¹Ù");
        var ui = Managers.UI.ShowSceneUI<UIGameStatus>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //var sub = Managers.UI.MakeSubItem<UIDialogueSubtitle>(Managers.UI.Root.transform, null, true);
        // #7 ÀÌ½´
        //sub.Init();
        ui.Init();
    }

    protected override void Clear()
    {

    }
}
