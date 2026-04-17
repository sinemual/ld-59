using System;
using Client.Infrastructure.UI.BaseUI;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : BaseScreen
{
    [Header("Vibration")] 
    [SerializeField] private UIButton vibrationButton;
    [SerializeField] private Sprite vibrationOnSprite;
    [SerializeField] private Sprite vibrationOffSprite;
    [SerializeField] private Image vibrationStateImage;

    [Header("Music")] 
    [SerializeField] private UIButton musicButton;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Image musicStateImage;
    
    [Header("Sound Effects")]
    [SerializeField] private UIButton effectsButton;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Sprite effectsOnSprite;
    [SerializeField] private Sprite effectsOffSprite;
    [SerializeField] private Image effectsStateImage;

    [SerializeField] private UIButton closeButton;

    public event Action VibrationTriggerButtonTap;
    public event Action EffectsTriggerButtonTap;
    public event Action MusicTriggerButtonTap;
    public event Action<float> MusicVolumeChanged;
    public event Action<float> EffectsVolumeChanged;
    public event Action CloseButtonClick;
    

    protected override void ManualStart()
    {
        //Debug.Log("SettingScreen ManualStart");
        ShowScreen += UpdateView;
        
        musicButton.Clicked += MusicButtonTap;
        effectsButton.Clicked += EffectsButtonTap;
        vibrationButton.Clicked += VibrationButtonTap;
        closeButton.Clicked += OnCloseButtonClick;
        
        musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
        effectsSlider.onValueChanged.AddListener(OnEffectsValueChanged);
    }

    private void MusicButtonTap()
    {
        MusicTriggerButtonTap?.Invoke();
        musicStateImage.sprite = Data.SaveData.IsMusicOn ? musicOffSprite : musicOnSprite;
    }
    
    private void EffectsButtonTap()
    {
        EffectsTriggerButtonTap?.Invoke();
        effectsStateImage.sprite = Data.SaveData.IsMusicOn ? effectsOnSprite : effectsOffSprite;
    }

    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(OnMusicValueChanged);
        effectsSlider.onValueChanged.RemoveListener(OnEffectsValueChanged);
        closeButton.Clicked -= OnCloseButtonClick;
    }

    public void UpdateView()
    {
        vibrationStateImage.sprite = Data.SaveData.IsVibrationOn ? vibrationOnSprite : vibrationOffSprite;
        musicStateImage.sprite = Data.SaveData.IsMusicOn ? musicOnSprite : musicOffSprite;
        effectsStateImage.sprite = Data.SaveData.IsEffectsOn ? effectsOnSprite : effectsOffSprite;
        musicSlider.value = Data.SaveData.MusicVolume;
        effectsSlider.value = Data.SaveData.EffectsVolume;
    }

    private void OnMusicValueChanged(float value)
    {
        MusicVolumeChanged?.Invoke(value);
    }

    private void OnEffectsValueChanged(float value) => EffectsVolumeChanged?.Invoke(value);

    public void UpdateSliders()
    {
        musicSlider.value = Data.SaveData.MusicVolume;
        effectsSlider.value = Data.SaveData.EffectsVolume;
        OnMusicValueChanged(musicSlider.value);
        OnEffectsValueChanged(effectsSlider.value);
    }
    
    /*protected override void Show()
    {
        gameObject.SetActive(true);
        Tween.Scale(transform, 1.0f, 0.25f);
    }
        
    protected override void Hide()
    {
        Debug.Log($"HideHideHide");
        Tween.Scale(transform, 0.0f, 0.25f).OnComplete(() => gameObject.SetActive(false));
    }*/

    private void VibrationButtonTap()
    {
        VibrationTriggerButtonTap?.Invoke();
        vibrationStateImage.sprite = Data.SaveData.IsVibrationOn ? vibrationOnSprite : vibrationOffSprite;
    }

    private void OnCloseButtonClick()
    {
        CloseButtonClick?.Invoke();
    }
}