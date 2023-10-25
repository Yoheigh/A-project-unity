using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using static Define;

public class UIDialogueSubtitle : UIPopup
{
    private float nextDialogWaitTime = 5f;

    enum Images
    {
        SpeakerImage,
        BackGroundImage,
    }

    enum Texts
    {
        SpeakerNameText,
        SubtitleText,
    }

    public void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Image>(typeof(Images));
        Bind<TMP_Text>(typeof(Texts));

        Managers.Event.AddEvent(IntEventType.OnSubtitleChange, SetSubtitleText);

        return true;
    }

    private void SetSubtitleText(int index)
    {
        GetText((int)Texts.SubtitleText).text = "index로 Dialog 불러오기 기능 대기중";
        DOVirtual.DelayedCall(5f, () => base.ClosePopupUI());

        // 이 자막이 ClosePopupUI로 꺼지면 안 되기 때문에 #7 이슈에 적어둠
    }

    
}
