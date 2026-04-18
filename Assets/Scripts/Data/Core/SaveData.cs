using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Client.Data;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class SaveData
    {
        [Header("GameSettings")]
        public bool IsGameLaunchedBefore;
        public bool IsVibrationOn;
        public bool IsMusicOn;
        public bool IsEffectsOn;
        public float MusicVolume;
        public float EffectsVolume;

        [Header("Currency")] 
        public int Currency;
        public int SignalPoints;
        public int BestSignalPoints;
        public int LevelIdx;
        public int EventLevelIdx;
        public int PlayerLevel;
        public float Experience;
        
        [Header("Timers")] 
        public string IdleRewardTimeKey;
        
        public TutorialStep CurrentTutorialStep;

        [Header("Tutorials")] public SerializedDictionary<TutorialStep, bool> TutrorialStates;
        [Header("Tutorials")] public SerializedDictionary<string, bool> IsBlockResearched;

        public void ResetToDefaults()
        {
            LevelIdx = 0;
            EventLevelIdx = 0;
            PlayerLevel = 1;
            Experience = 0;
            
            IsGameLaunchedBefore = false;
            IsVibrationOn = true;
            IsMusicOn = true;
            IsEffectsOn = true;
            MusicVolume = 0.5f;
            EffectsVolume = 0.5f;
            
            CurrentTutorialStep = 0;
            IdleRewardTimeKey = "";
            
            TutrorialStates = new SerializedDictionary<TutorialStep, bool>();
            IsBlockResearched = new SerializedDictionary<string, bool>();
        }

        public void BindData(int startMoney, List<TutorialData> tutorialDatas)
        {
            Currency = startMoney;
            for (var i = 0; i < tutorialDatas.Count; i++)
                TutrorialStates.Add((TutorialStep)i, false);
        }
    }
}