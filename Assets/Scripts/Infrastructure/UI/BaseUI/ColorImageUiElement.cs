using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ColorImageUiElement
{
    [SerializeField] private Image image;
    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;

    public virtual void SetColorToEnable() => image.color = enabledColor;
    public virtual void SetColorToDisable() => image.color = disabledColor;
}

[Serializable]
public class ColorImageUiElementWithText : ColorImageUiElement
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Color textEnabledColor;
    [SerializeField] private Color textDisabledColor;

    public override void SetColorToEnable()
    {
        base.SetColorToEnable();
        text.color = textEnabledColor;
    }

    public override void SetColorToDisable()
    {
        base.SetColorToDisable();
        text.color = textDisabledColor;
    }
}