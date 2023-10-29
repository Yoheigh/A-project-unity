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
    public bool isMoving = false;
    public bool isSprintable = true;
    public bool isSprinting = false;

    public CharacterGlobalStatus GlobalStatus { get; private set; }

    // private variable
    [SerializeField]
    private float hp;
    [SerializeField]
    private float stamina;
    [SerializeField]
    private float temperture;
    [SerializeField]
    private float hunger;

    private float hpDecayPerSecond;
    private float staminaDecayPerSecond;
    private float tempertureDecayPerSecond;
    private float hungerDecayPerSecond;

    private float staminaDecreaseRate = -3f;
    private float tempertureDecreaseRate = -0.2f;
    private float hungerDecreaseRate = -0.2f;

    private float staminaIncreaseRate;
    private float tempertureIncreaseRate;
    private float hungerIncreaseRate;


    public void Init()
    {
        hp = MaxHP;
        stamina = MaxStamina;
        hunger = 30f; //MaxHunger;
        temperture = 30f; // MaxTemperture;

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
        if (hp < 0.001)
            Managers.Object.Player.FSM.ChangeState(Managers.Object.Player.States[(int)PlayerState.Ragdoll]);

        hpDecayPerSecond = 0f;
        if (hunger == 0)     hpDecayPerSecond += -0.5f;
        if (temperture == 0) hpDecayPerSecond += -0.5f;

        hp += hpDecayPerSecond * Time.deltaTime;
        hp = Mathf.Clamp(hp, MinHP, MaxHP);
    }

    public void CalculateStamina()
    {
        if (isSprinting == true) staminaIncreaseRate = 0;
        else if (isMoving == true) staminaIncreaseRate = 5;
        else staminaIncreaseRate = 9f;

        if(stamina < 0.1f && isSprinting)
        {
            isSprintable = false;
            isSprinting = false;
            DG.Tweening.DOVirtual.DelayedCall(5f, () => isSprintable = true);
        }

        stamina += (staminaDecreaseRate + staminaIncreaseRate + staminaDecayPerSecond) * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, MinStamina, MaxStamina);
    }

    public void CalculateTemperture()
    {
        tempertureDecayPerSecond = 0f;

        temperture += (tempertureDecreaseRate + tempertureDecayPerSecond) * Time.deltaTime;
        temperture = Mathf.Clamp(temperture, MinTemperture, MaxTemperture);
    }

    public void CalculateHunger()
    {
        hungerDecayPerSecond = 0f;

        hunger += (hungerDecreaseRate + hungerDecayPerSecond) * Time.deltaTime;
        hunger = Mathf.Clamp(hunger, MinHunger, MaxHunger);
    }

    public void TryToChangeParameter(bool original, bool toChange)
    {
        if (original != false)
            original = toChange;
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