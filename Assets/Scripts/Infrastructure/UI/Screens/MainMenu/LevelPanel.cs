using System;
using Client;
using Client.DevTools.MyTools;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chapterNumText;
    [SerializeField] private TextMeshProUGUI chapterNameText;
    [SerializeField] private TextMeshProUGUI chapterDifficultyText;
    [SerializeField] private Image levelImage;
    [SerializeField] private GameObject lockGo;
    [SerializeField] private GameObject raysGo;
    [SerializeField] private GameObject completedGo;
    [SerializeField] private GameObject heroImage;
    public Image LevelImage => levelImage;

    private Material _mat;

    private void Awake()
    {
         //_mat = new Material(levelImage.material);
        //levelImage.material = _mat;
    }

    public void UpdateInfoByData(LevelData levelData, int currentWorld)
    {
        /*chapterNumText.text = $"CHAPTER {levelData.Id + 1}";
        chapterNameText.text = $"{levelData.LevelName}";
        
        //chapterDifficultyText.text = $"<{Utility.ToRGBHex(TextColorByDifficulty[worldData.Difficulty])}>{worldData.Difficulty}";
        LevelImage.sprite = levelData.LevelSprite;

        raysGo.SetActive(currentWorld == levelData.Id);
        heroImage.SetActive(currentWorld == levelData.Id);
        lockGo.SetActive(currentWorld < levelData.Id);
        completedGo.SetActive(currentWorld > levelData.Id);*/
        
        //_mat.SetFloat("_EffectAmount", currentWorld < levelData.Id ? 1.0f : 0.0f);
    }
}