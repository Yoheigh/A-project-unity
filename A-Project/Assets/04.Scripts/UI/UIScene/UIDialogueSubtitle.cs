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

        Debug.Log("UI!! �ʱ�ȭ!!!!!!!!!!!!! �Ϸ�");
        return true;
    }

    private void SetSubtitleText(int index)
    {
        GetText((int)Texts.SubtitleText).text = "���� Ű���佺�� �������̴�";
        DOVirtual.DelayedCall(5f, () => base.ClosePopupUI());
    }
}
