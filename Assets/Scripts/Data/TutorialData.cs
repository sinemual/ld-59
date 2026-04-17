using System;
using Client.Data;
using Data.Base;
using TriInspector;
using UnityEngine;
using CameraType = Client.Data.CameraType;

namespace Data
{
    [CreateAssetMenu(menuName = "GameData/TutorialData", fileName = "TutorialData")]
    [Serializable]
    public class TutorialData : BaseDataSO
    {
        public int Id;
        public TutorialStep TutorialStep;
        public string PointId;
        public bool IsCameraMovingToPoint;
        public bool IsArrowNavigationNeeded;
        public bool IsStartByMoney;
        [ShowIf("IsStartByMoney")] public int StartMoney;
        public bool IsShowByMoney;
        [ShowIf("IsShowByMoney")] public int ShowMoney;
        

        public CameraType CameraType;
        public string TutorialDescriptionText;
        public int GoalValue;
        public double Reward;
        public TutorialData PreviousTutorialData;
        public TutorialData NextTutorialData;
        public bool IsAutoStartNextStep;
    }
}