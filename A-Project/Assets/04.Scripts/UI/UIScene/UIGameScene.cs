using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UIGameScene : UIScene, ITickEvent
{
    PlayerController Player => Managers.Object.Player;

    public override bool Init()
    {
        base.Init();

        //BindObject(typeof(GameObject));
        BindSlider(typeof(UI_SliderType));
        //BindImage(typeof(Image));
        //BindText(typeof(TMP_Text));
        //BindButton(typeof(Button));
        //BindToggle(typeof(Toggle));

        Managers.Event.AddEvent(VoidEventType.OnHPChange, SetHPSlider);
        Managers.Event.AddEvent(VoidEventType.OnStaminaChange, SetStaminaSlider);
        Managers.Event.AddEvent(VoidEventType.OnHungerChange, SetHungerSlider);
        Managers.Event.AddEvent(VoidEventType.OnTempertureChange, SetTempertureSlider);

        Managers.Tick.AddTickEvent(OnTickEvent);

        return true;
    }

    public void OnTickEvent(object sender, OnTickEventArgs args)
    {
        if(args.tick % 5 == 0)
            Debug.Log(args.tick / 5);

        //// 스탯 조정을 tick 기반으로 할지 안 할지 처리
        //player.Stat.UpdateHP();
        //player.Stat.UpdateStamina();
        //player.Stat.UpdateHunger();
        //player.Stat.UpdateTemperture();

        SetHPSlider();
        SetStaminaSlider();
        SetHungerSlider();
        SetTempertureSlider();
    }

    private void SetHPSlider()
    {
        GetSlider((int)UI_SliderType.HP_Slider).value = Player.Stat.HP / Player.Stat.MaxHP;
    }

    private void SetStaminaSlider()
    {
        GetSlider((int)UI_SliderType.Stamina_Slider).value = Player.Stat.Stamina / Player.Stat.MaxStamina;
    }

    private void SetHungerSlider()
    {
        GetSlider((int)UI_SliderType.Hunger_Slider).value = Player.Stat.Hunger / Player.Stat.MaxHunger;
    }

    private void SetTempertureSlider()
    {
        GetSlider((int)UI_SliderType.Temperture_Slider).value = Player.Stat.Temperture / Player.Stat.MaxTemperture;
    }

    void Update()
    {

    }
}
