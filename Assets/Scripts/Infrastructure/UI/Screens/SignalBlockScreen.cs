using System;
using Client;
using Client.Infrastructure.UI.BaseUI;
using UnityEngine;

public class SignalBlockScreen : BaseScreen
{
    [SerializeField] private SignalBlockPanels signalBlockPanels;
    [SerializeField] private UIButton closeButton;
    [SerializeField] private UIButton deleteModeButton;
    [SerializeField] private MovablePanel movablePanel;
    [SerializeField] private TutorialHandAnimation tutorialHandAnimation;

    public event Action CloseButtonClick;
    public event Action DeleteModeButtonClick;

    public SignalBlockPanels SignalBlockPanels => signalBlockPanels;

    protected override void ManualStart()
    {
        closeButton.Clicked += OnCloseButtonClick;
        deleteModeButton.Clicked += OnDeleteModeButtonClick;
        ShowScreen += UpdateView;
    }

    private void UpdateView()
    {
    }
    
    protected override void Show()
    {
        gameObject.SetActive(true);
        //base.Show();
        movablePanel.MoveToEnd();
    }

    protected override void Hide()
    {
        //base.Hide();
        tutorialHandAnimation.gameObject.SetActive(false);
        movablePanel.MoveToStart();
    }

    private void OnMoveToEndComplete()
    {
        gameObject.SetActive(true);
    }

    private void OnCloseButtonClick() => CloseButtonClick?.Invoke();
    private void OnDeleteModeButtonClick() => DeleteModeButtonClick?.Invoke();
}