using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScene : BaseScene
{
    protected override void Init()
    {
        var sub = Managers.UI.ShowPopupUI<UIDialogueSubtitle>();
        var ui = Managers.UI.ShowSceneUI<UIGameStatus>();

        //var sub = Managers.UI.MakeSubItem<UIDialogueSubtitle>(Managers.UI.Root.transform, null, true);
        // #7 ¿ÃΩ¥
        sub.Init();
        ui.Init();
    }

    protected override void Clear()
    {

    }
}
