using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using static Define;

public class UIDialogueSubtitle : UIPopup
{
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

        Debug.Log("UI!! 초기화!!!!!!!!!!!!! 완료");
        return true;
    }

    private void SetSubtitleText(int index)
    {
        GetText((int)Texts.SubtitleText).text = "나는 키보토스의 선생님이다";
        DOVirtual.DelayedCall(5f, () => base.ClosePopupUI());
    }
}
