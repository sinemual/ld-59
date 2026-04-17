using System;
using Client.Data;
using Client.DevTools.MyTools;
using Client.Infrastructure.UI.BaseUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : BaseScreen
{
    [SerializeField] private UIButton settingsButton;
    [SerializeField] private TextMeshProUGUI moneyText;

    public event Action OpenSettingScreenButtonClick;
    public event Action<TutorialStep> StartTutorial;
    
    protected override void ManualStart()
    {
        settingsButton.Clicked += OnSettingsButtonClick;
        ShowScreen += UpdateView;
    }

    private void UpdateView()
    {
        
    }

    private void OnSettingsButtonClick() => OpenSettingScreenButtonClick?.Invoke();

    public void UpdateMoneyText(double moneyCount)
    {
        moneyText.text = $"{moneyCount:0}"; // money sprite
        //moneyText.gameObject.transform.DORewind();
        //moneyText.gameObject.transform.DOPunchScale(Vector3.one * 0.5f, 0.15f, 2, 0.5f);
    }
}