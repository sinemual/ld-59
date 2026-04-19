using System;
using Client.Infrastructure.UI.BaseUI;
using PrimeTween;
using TMPro;
using UnityEngine;

public class StartScreen : BaseScreen
{
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private UIButton nextButton;
    [SerializeField] private float duration;

    private readonly string _startString =
        "Wake up, Mr. Chief Scientist!\n \nWe've received a message from a galaxy far, far away!\n \nYou need to fix the SIGNAL to receive this message!";

    private Tween _textTween;

    //public event Action NextButtonClick;
    protected override void ManualStart()
    {
        nextButton.Clicked += OnNextButtonClick;
        ShowScreen += UpdateView;
    }

    private void UpdateView()
    {
        Play(_startString);
    }

    private void OnNextButtonClick()
    {
        Hide();
    } /*=> NextButtonClick?.Invoke();*/


    private void Play(string fullText)
    {
        _textTween.Stop();

        startText.text = fullText;
        startText.ForceMeshUpdate();
        startText.maxVisibleCharacters = 0;

        int totalChars = startText.textInfo.characterCount;

        _textTween = Tween.Custom(
            0,
            totalChars,
            duration,
            onValueChange: value => { startText.maxVisibleCharacters = Mathf.FloorToInt(value); });
    }
}