using System.Collections.Generic;
using Client.Infrastructure.UI.BaseUI;
using TMPro;
using UnityEngine;

public class UIButtonWithText : UIButton
{
    [SerializeField] private List<TextMeshProUGUI> texts;
    [SerializeField] private Color textEnabledColor;
    [SerializeField] private Color textDisabledColor;

    public override void SetInteractable(bool value)
    {
        base.SetInteractable(value);
        foreach (var text in texts)
            text.color = value ? textEnabledColor : textDisabledColor;
    }
}