using System;
using UnityEngine;

public class TutorialMonoProvider : MonoProvider<TutorialProvider>
{
    [SerializeField] private bool isUiTutr;
    [HideInInspector] public bool IsSortingOverrided;

    private void OnEnable()
    {
        if (isUiTutr)
            UpdateSortingOverride();
    }

    public void UpdateSortingOverride()
    {
        if (IsSortingOverrided)
        {
            Value.UiElementCanvas.overrideSorting = true;
            Value.UiElementCanvas.sortingOrder = 10;
        }
        else
        {
            Value.UiElementCanvas.sortingOrder = Value.SavedSortingOrder;
            Value.UiElementCanvas.overrideSorting = false;
        }
    }
}