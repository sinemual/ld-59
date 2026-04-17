using System;
using Client.Infrastructure.UI.BaseUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpScreen : BaseScreen
{
    [SerializeField] private UIButton okButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image levelProgressBarImage;

    public event Action OkButtonTap;
    
    protected override void ManualStart()
    {
        okButton.Clicked += () =>
        {
            SetShowState(false);
            OkButtonTap?.Invoke();
        };

        ShowScreen += UpdateInfo;
    }

    private void UpdateInfo()
    {
        levelText.text = $"{Data.SaveData.PlayerLevel}";
        levelProgressBarImage.fillAmount = Data.SaveData.Experience / Data.RuntimeData.ExperienceToNextLevel();
        //levelProgressBarImage.DOFillAmount(1.0f, 1.5f).SetEase(Ease.OutQuint).SetDelay(0.2f);
    }
}