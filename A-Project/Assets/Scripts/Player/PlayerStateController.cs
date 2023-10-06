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

    // ü��
    public float HP
    { get { return hp; } set {; } }

    // ���¹̳�
    public float Stamina
    { get { return stamina; } set {; } }

    // ���
    public float Hunger
    { get { return hunger; } set {; } }

    // �µ�
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

    // HP�� ����� �� �����ϴ� �Լ�
    public void UpdateHP()
    {

    }

    // HP�� ����� �� �����ϴ� �Լ�
    public void UpdateStamina()
    {
       stamina = Time.deltaTime * stamina;
    }

    // HP�� ����� �� �����ϴ� �Լ�
    public void UpdateTemperture()
    {

    }

    // HP�� ����� �� �����ϴ� �Լ�
    public void UpdateHunger()
    {

    }

    // ���� ȭ�� ����
    public float CalculateTemperture(float temperture)
    {
        if (_CelsiusToFahrenheit)
            return (temperture * 1.8f + 32);
        else
            return temperture;
    }
}
