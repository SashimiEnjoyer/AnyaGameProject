using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsData : MonoBehaviour
{
    public static float masterVolume;
    public static float soundEffect;
    public static float background;

    public float test;

    [Header("Sliders")]
    public GameObject sliderMasterVolume;
    public GameObject sliderSFX;
    public GameObject sliderBackground;

    private Slider sMV;
    private Slider sSFX;
    private Slider sB;

    void Awake()
    {
        sMV = sliderMasterVolume.GetComponent<Slider>();
        sSFX = sliderSFX.GetComponent<Slider>();
        sB = sliderBackground.GetComponent<Slider>();

        sMV.value = masterVolume;
        sSFX.value = soundEffect;
        sB.value = background;
    }

    void FixedUpdate()
    {
        masterVolume = sMV.value;
        soundEffect = sSFX.value;
        background = sB.value;

        test = masterVolume;
    }
}
