using Cinemachine.PostFX;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EnvironmentController : MonoBehaviour
{
    private static EnvironmentController instance;
    public static EnvironmentController Instance { get { return instance; } }

    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        if (instance != null)
            return;

        instance = new GameObject($"[{nameof(EnvironmentController)}]").AddComponent<EnvironmentController>();
        DontDestroyOnLoad(instance.gameObject);
    }

    public CinemachineVolumeSettings Volume;

    Bloom bloom;
    Vignette vignette;
    DepthOfField depth;

    float bloomModifier;
    float vignetteModifier;

    void Start()
    {
        Volume = FindObjectOfType<CinemachineVolumeSettings>();

        bloom = (Bloom)Volume.m_Profile.components[1];
        vignette = (Vignette)Volume.m_Profile.components[2];
        depth = (DepthOfField)Volume.m_Profile.components[4];

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // bloom.dirtIntensity.value = bloomModifier + (100f - Managers.Object.Player.Stat.Temperture) / 7f;
        // vignette.intensity.value = vignetteModifier + (100f - Managers.Object.Player.Stat.HP) / 400f;

        if (Input.GetKeyDown(KeyCode.Alpha5)) Normal();
        if (Input.GetKeyDown(KeyCode.Alpha6)) SnowStorm();
        if (Input.GetKeyDown(KeyCode.Alpha8)) Time.timeScale = 1.0f;
        if (Input.GetKeyDown(KeyCode.Alpha9)) Time.timeScale = 3.0f;
        if (Input.GetKeyDown(KeyCode.Alpha9)) Time.timeScale = 3.0f;
        if (Input.GetKeyDown(KeyCode.M))
        {
            Managers.Scene.LoadScene("WorldScene");
        }
            


    }

    public void Normal()
    {
        bloomModifier = 0;
        bloom.dirtIntensity.value = 0;
        vignetteModifier = 0.348f;
    }

    public void SnowStorm()
    {
        bloomModifier = 10f;
        bloom.dirtIntensity.value = 14f;
        vignetteModifier = 0.454f;
    }
}
