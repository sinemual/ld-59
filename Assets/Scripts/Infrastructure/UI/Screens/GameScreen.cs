using System;
using System.Collections.Generic;
using Client;
using Client.DevTools.MyTools;
using Client.Infrastructure.UI.BaseUI;
using Data;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : BaseScreen
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI signalPointText;
    [SerializeField] private TextMeshProUGUI bestSignalPointText;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private UIButton pauseButton;

    [Header("Level Progress Bar")] [SerializeField]
    private TextMeshProUGUI levelText;

    [SerializeField] private Image levelProgressMoveBarFill;

    public event Action PauseButtonClick;
    protected override void ManualStart()
    {
        pauseButton.Clicked += OnPauseButtonClick;
        ShowScreen += UpdateView;
    }

    private void UpdateView()
    {
    }

    private void OnPauseButtonClick() => PauseButtonClick?.Invoke();

    public void UpdateMoneyText(int moneyAmount)
    {
        //moneyText.text = $"{Utility.Format(moneyCount)}"; // money sprite
        int currentMoney = moneyAmount;

        Tween.Custom(
            startValue: (float)currentMoney,
            endValue: (float)moneyAmount,
            duration: 0.3f,
            onValueChange: value => moneyText.text = $"{value:0}",
            ease: Ease.OutQuad
        );

        Tween.PunchScale(
            target: moneyText.transform,
            strength: Vector3.one * 0.1f,
            duration: 0.15f
        );
    }
    
    public void UpdateSignalPointText(int moneyAmount)
    {
        //moneyText.text = $"{Utility.Format(moneyCount)}"; // money sprite
        int currentMoney = moneyAmount;

        Tween.Custom(
            startValue: (float)currentMoney,
            endValue: (float)moneyAmount,
            duration: 0.3f,
            onValueChange: value => moneyText.text = $"{value:0}",
            ease: Ease.OutQuad
        );

        Tween.PunchScale(
            target: moneyText.transform,
            strength: Vector3.one * 0.1f,
            duration: 0.15f
        );
    }

    public void UpdateLevelText(int level) => levelText.text = $"Level {level + 1}";
    public void UpdateLevelText(string levelName) => levelText.text = $"{levelName}";
}