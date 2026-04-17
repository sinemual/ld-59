using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GrayscaleColorImageUiElement
{
    public Image image;
    
    private Material _mat;
    private static readonly int EffectAmount = Shader.PropertyToID("_EffectAmount");

    public void Init()
    {
        _mat = new Material(image.material);
        image.material = _mat;
    }

    public void EnableGrayscale() => _mat.SetFloat(EffectAmount, 1.0f);
    public void DisableGrayscale() => _mat.SetFloat(EffectAmount, 0.0f);
}