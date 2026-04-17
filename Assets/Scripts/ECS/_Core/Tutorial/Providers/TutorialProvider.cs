using System;
using Client.Data;
using Client.Infrastructure.UI.BaseUI;
using TriInspector;
using UnityEngine;

[Serializable]
public struct TutorialProvider
{
    public TutorialStep TutorialStep;
    public GameObject TutorialHand;
    public bool IsStopTime;
    public bool IsUiTutorial;
    [ShowIf("IsUiTutorial")] public Canvas UiElementCanvas;
    [ShowIf("IsUiTutorial")] public UIButton Button;
    [HideInInspector] public int SavedSortingOrder;
    public TutorialMonoProvider MonoProvider;
}