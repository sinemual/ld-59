using System;
using TMPro;
using UnityEngine;

[Serializable]
public class ColorTextUiElement
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;
    
    public void SetColorToEnable() => text.color = enabledColor;
    public void SetColorToDisable() => text.color = disabledColor;
}