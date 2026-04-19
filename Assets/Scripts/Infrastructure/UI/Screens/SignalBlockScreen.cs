using System;
using Client;
using Client.Infrastructure.UI.BaseUI;
using TMPro;
using UnityEngine;

public class SignalBlockScreen : BaseScreen
{
    [SerializeField] private SignalBlockPanels signalBlockPanels;
    [SerializeField] private UIButton closeButton;
    [SerializeField] private UIButton deleteModeButton;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private MovablePanel movablePanel;
    [SerializeField] private TutorialHandAnimation tutorialHandAnimation;
    [SerializeField] private UIButton[] tabButtons;

    public event Action CloseButtonClick;
    public event Action<int> ChangeTabButtonClick;
    public event Action DeleteModeButtonClick;

    public SignalBlockPanels SignalBlockPanels => signalBlockPanels;

    protected override void ManualStart()
    {
        closeButton.Clicked += OnCloseButtonClick;
        deleteModeButton.Clicked += OnDeleteModeButtonClick;
        ShowScreen += UpdateView;
        for (int i = 0; i < tabButtons.Length; i++)
        {
            var iTemp = i;
            tabButtons[i].Clicked += () => OnTabClicked(iTemp);
        }
    }

    private void OnTabClicked(int index)
    {
        ChangeTabButtonClick?.Invoke(index);
    }

    private void UpdateView()
    {
    }

    public void UpdateDescriptionText(string blockDescription)
    {
        description.text = blockDescription;
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