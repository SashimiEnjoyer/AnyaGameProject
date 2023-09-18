using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    public Slider skillSlider;
    public Image skillImage;
    public Image fillImage;

    public void SetMaxValue(float maxVal)
    {
        skillSlider.maxValue = maxVal;
        skillSlider.value = skillSlider.maxValue;
    }

    public void SetValueToZero()
    {
        skillSlider.value = 0;
        fillImage.enabled = true;
    }

    public void SetSkillImageSprite(Sprite spriteToSet)
    {
        skillImage.sprite = spriteToSet;
    }

    public void OnSliderChange(float value)
    {
        if(value < skillSlider.maxValue)
        {
            skillImage.color = Color.gray;
        }
        else
        {
            skillImage.color = Color.white;
            fillImage.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (skillSlider.value >= skillSlider.maxValue)
            return;

        skillSlider.value += Time.deltaTime;
    }
}
