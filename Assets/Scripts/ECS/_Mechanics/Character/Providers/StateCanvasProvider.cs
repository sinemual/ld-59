using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct StateCanvasProvider
{
    public GameObject Canvas;
    public Image HpBar;
    public Image HpHighlightBar;
    public TextMeshProUGUI HpText;
    public GameObject ArmorPanel;
    public TextMeshProUGUI ArmorText;
}