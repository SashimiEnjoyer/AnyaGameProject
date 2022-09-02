using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsData : MonoBehaviour
{
    public static float masterVolume = 1f;
    public static float soundEffect = 1f;
    public static float background = 1f;
    public static float lang;

    [Header("Sliders")]
    public GameObject sliderMasterVolume;
    public GameObject sliderSFX;
    public GameObject sliderBackground;

    [Header("Language")]
    public Image eN;
    public Image jP;
    public MenuSetting MsEN;
    public MenuSetting MsJP;
    public float test;

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

        if (lang == 1)
        {
            MsEN.isActive = false;
            MsJP.isActive = true;
        }
        
        if (lang == 0)
        {
            MsEN.isActive = true;
            MsJP.isActive = false;
        }
    }

    void FixedUpdate()
    {
        masterVolume = sMV.value;
        soundEffect = sSFX.value;
        background = sB.value;

        if (jP.color.a == 1)
        lang = 1;

        if (eN.color.a == 1)
        lang = 0;

        test = lang;
    }
}
