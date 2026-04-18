using System;
using Client.Data;
using Data.Base;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "GameData/SignalBlockData", fileName = "SignalBlockData")]
    [Serializable]
    public class SignalBlockData : BaseDataSO
    {
        public SignalBlockType SignalBlockType;
        public InputDirection InputDirection;
        public GameObject Prefab;
        public Sprite BlockSprite;
        public string BlockName;
        public string BlockDescription;
        public int BuyPrice;
        public int ResearchPrice;
    }
}