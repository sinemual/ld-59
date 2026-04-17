using System;
using Client.Infrastructure.UI.BaseUI;
using TMPro;
using UnityEngine;

public class DebugScreen : BaseScreen
{
    [SerializeField] private UIButton changeCameraButton;
    [SerializeField] private UIButton changeControlButton;
    //[SerializeField] private TextMeshProUGUI numText;

    public event Action ChangeCameraButtonClick;
    public event Action ChangeControlButtonClick;

    protected override void ManualStart()
    {
        changeCameraButton.Clicked += OnChangeCameraButtonClick;
        changeControlButton.Clicked += OnChangeControlButtonClick;
        ShowScreen += UpdateView;
    }

    private void OnChangeCameraButtonClick() => ChangeCameraButtonClick?.Invoke();
    private void OnChangeControlButtonClick() => ChangeControlButtonClick?.Invoke();

    /*public void UpdateNumText(int num)
    {
        numText.text = $"Camera #{num}";
    }*/
    
    private void UpdateView()
    {
    }
}