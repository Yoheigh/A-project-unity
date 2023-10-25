using UnityEngine;
using static Define;

public class PlayerStatController : MonoBehaviour
{
    PlayerStat currentStat
    {
        get
        {
            if (currentStat == null)
            {
                currentStat = Managers.Game.GetSaveData().stat;
                return currentStat;
            }
            else
                return currentStat;
        }
        set { }
    }

    // Ã¼·Â
    public float MaxHP = 100;
    public const float MinHP = 0;
    public float HP
    { get { return hp; } set {; } }

    // ½ºÅÂ¹Ì³Ê
    public float MaxStamina = 100;
    public const float MinStamina = 0;
    public float Stamina
    { get { return stamina; } set {; } }

    // Çã±â
    public float MaxHunger = 100;
    public const float MinHunger = 0;
    public float Hunger
    { get { return hunger; } set {; } }

    // ¿Âµµ
    public float MaxTemperture = 100;
    public const float MinTemperture = 0;
    public float Temperture
    { get { return temperture; } set {; } }

    public bool _CelsiusToFahrenheit = false;

    public CharacterGlobalStatus GlobalStatus { get; private set; }

    // private variable
    private float hp;
    private float stamina;
    private float temperture;
    private float hunger;

    private float hpDecayPerSecond;
    private float staminaDecayPerSecond;
    private float tempertureDecayPerSecond;
    private float hungerDecayPerSecond;

    private float staminaIncreaseRate = 5f;
    private float staminaDecreaseRate = 3f;
    private float tempertureDecreaseRate = 0.02f;
    private float hungerDecreaseRate = 0.02f;

    public void Init()
    {
        ChangeGlobalStatus(CharacterGlobalStatus.Normal);
    }

    private void Update()
    {
        CaculateStatAll();
    }

    public void ChangeGlobalStatus(CharacterGlobalStatus status)
    {
        GlobalStatus = status;
    }

    public void CaculateStatAll()
    {
        CalculateHP();
        CalculateStamina();
        CalculateTemperture();
        CalculateHunger();
    }

    public void CalculateHP()
    {
        hpDecayPerSecond = 0f;
        if (hunger == 0)     hpDecayPerSecond += -0.5f;
        if (temperture == 0) hpDecayPerSecond += -0.5f;

        hp += hpDecayPerSecond * Time.deltaTime;
    }

    public void CalculateStamina()
    {
        stamina += (staminaDecreaseRate + staminaDecayPerSecond) * Time.deltaTime;
    }

    public void CalculateTemperture()
    {
        tempertureDecayPerSecond = 0f;

        temperture += (tempertureDecreaseRate + tempertureDecayPerSecond) * Time.deltaTime;
    }

    public void CalculateHunger()
    {
        hungerDecayPerSecond = 0f;

        hunger += (hungerDecreaseRate + hungerDecayPerSecond) * Time.deltaTime;
    }

    // ¼·¾¾ È­¾¾ º¯°æ
    //public float CalculateTemperture(float temperture)
    //{
    //    if (_CelsiusToFahrenheit)
    //        return (temperture * 1.8f + 32);
    //    else
    //        return temperture;
    //}
}