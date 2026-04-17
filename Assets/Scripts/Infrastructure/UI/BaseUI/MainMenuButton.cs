using System;
using Client.Infrastructure.UI.BaseUI;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MainMenuButton
{
    [SerializeField] private UIButton button;
    [SerializeField] private Image image;
    [SerializeField] private Image iconImage;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;
    [SerializeField] private Vector3 enabledScale;
    [SerializeField] private Vector3 iconScale;

    public UIButton Button => button;

    public void SetColorToEnable()
    {
        image.color = enabledColor;
        //Button.transform.DOScale(enabledScale, 0.5f);
        //iconImage.transform.DOScale(iconScale, 0.5f);
        iconImage.transform.localPosition = Vector3.zero + new Vector3(0.0f, 40, 0.0f);;
        //iconImage.transform.DOMoveY(iconImage.transform.position.y + 40.0f , 0.5f);
        canvas.sortingOrder = 5;
    }

    public void SetColorToDisable()
    {
        image.color = disabledColor;
        //Button.transform.DOScale(Vector3.one, 0.5f);
        //iconImage.transform.DOScale(Vector3.one, 0.5f);
        iconImage.transform.localPosition = Vector3.zero + new Vector3(0.0f, 80, 0.0f);
        //iconImage.transform.DOMoveY(iconImage.transform.position.y - 40.0f , 0.5f);
        canvas.sortingOrder = 1;
    }
}