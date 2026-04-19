using System;
using System.Collections.Generic;
using Client.Data;
using Client.Data.Core;
using Data.Base;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

namespace Data
{
    [Serializable]
    public class RuntimeData : BaseData
    {
        public GameStateType CurrentGameStateType;
        public GameStateType PreviousGameStateType;

        public int CurrentTaskId = -1;
        public float LevelProgress;
        public int BlockCounter;

        public int LastLevelIdx = -1;
        
        public bool IsWeHaveInternetTime;
        public bool IsLoopedLevel;
        public bool IsDeleteState;

        public void InjectData(SharedData sharedData) => base.SharedData = sharedData;

        public override void ResetData()
        {
        }

        public void ResetLevelData()
        {
            LevelProgress = 0;
        }

        public int GetLevelReward()
        {
            int value = SharedData.SaveData.EventLevelIdx * SharedData.BalanceData.BaseLevelReward;
            if (value < 500)
                value = 500;
            return value;
        }

        public int GetResearchPoints()
        {
            return 100;
        }

        public int ExperienceToNextLevel() =>
            ExperienceToLevel(SharedData.SaveData.PlayerLevel + 1) - ExperienceToLevel(SharedData.SaveData.PlayerLevel);

        public int ExperienceToLevel(int level) =>
            (int)(SharedData.BalanceData.LevelBaseCoef * Mathf.Pow(SharedData.BalanceData.LevelMultiCoef, level));
    }
}