using System;
using Client.DevTools.MyTools;
using Client.Infrastructure.UI.BaseUI;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignalBlockPanel : MonoBehaviour
{
    [SerializeField] private Image panelImage;
    [SerializeField] private Image blockImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private UIButton buyButton;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI researchPriceText;
    [SerializeField] private GameObject coverGo;
    [SerializeField] private GameObject notReserchedYeatPanel;
    [SerializeField] private RectTransform rectT;

    public event Action<SignalBlockData> BuyBlock;
    private SignalBlockData _signalBlockData;

    //private bool _isAlreadyBought;

    public RectTransform RectT => rectT;

    public void UpdateInfoByData(SignalBlockData signalBlockData, int money, bool isResearched, bool isTutorialStep)
    {
        _signalBlockData = signalBlockData;
        bool isCanBuyIt = money >= _signalBlockData.BuyPrice;
        blockImage.sprite = _signalBlockData.BlockSprite;
        nameText.text = $"{_signalBlockData.InputDirection}";
        descriptionText.text = $"{_signalBlockData.BlockDescription}";
        priceText.text = $"{Utility.Format(_signalBlockData.BuyPrice)}<sprite=0>";
        researchPriceText.text = $"{Utility.Format(_signalBlockData.BuyPrice)}<sprite=0>";

        buyButton.Clicked -= BuyBlockClick;
        buyButton.SetInteractable(isCanBuyIt);
        if (isCanBuyIt)
            buyButton.Clicked += BuyBlockClick;

        coverGo.SetActive(!isCanBuyIt);
        notReserchedYeatPanel.SetActive(!isResearched);
    }

    private void OnDisable()
    {
        buyButton.Clicked -= BuyBlockClick;
    }

    private void BuyBlockClick()
    {
        BuyBlock?.Invoke(_signalBlockData);
    }
}