using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using static Define;

public class UIDialogueSubtitle : UIToast
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

        Managers.UI.SetCanvas(gameObject, false, 100);
        BindImage(typeof(Images));
        BindText(typeof(Texts));

        // Managers.Event.AddEvent(IntEventType.OnSubtitleChange, SetInfo);

        return true;
    }

    public override void SetInfo(string msg)
    {
        GetText((int)Texts.SubtitleText).text = msg;
    }

    
}
