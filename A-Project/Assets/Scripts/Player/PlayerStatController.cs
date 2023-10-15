using UnityEngine;

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

    // 체력
    public float MaxHP = 100;
    public float MinHP = 0;
    public float HP
    { get { return hp; } set {; } }

    // 스태미너
    public float MaxStamina = 100;
    public float MinStamina = 0;
    public float Stamina
    { get { return stamina; } set {; } }

    // 허기
    public float MaxHunger = 100;
    public float MinHunger = 0;
    public float Hunger
    { get { return hunger; } set {; } }

    // 온도
    public float MaxTemperture = 100;
    public float MinTemperture = 0;
    public float Temperture
    { get { return temperture; } set {; } }

    public bool _CelsiusToFahrenheit = false;

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

    public void Start()
    {
        Init();
    }

    private void Init()
    {

    }

    // HP가 변경될 때 적용하는 함수
    public void UpdateHP()
    {

    }

    // HP가 변경될 때 적용하는 함수
    public void UpdateStamina()
    {
       stamina = Time.deltaTime * stamina;
    }

    // HP가 변경될 때 적용하는 함수
    public void UpdateTemperture()
    {

    }

    // HP가 변경될 때 적용하는 함수
    public void UpdateHunger()
    {

    }

    // 섭씨 화씨 변경
    public float CalculateTemperture(float temperture)
    {
        if (_CelsiusToFahrenheit)
            return (temperture * 1.8f + 32);
        else
            return temperture;
    }
}
