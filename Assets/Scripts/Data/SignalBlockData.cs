using System;
using Data.Base;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "GameData/SignalBlockData", fileName = "SignalBlockData")]
    [Serializable]
    public class SignalBlockData : BaseDataSO
    {
        public int Id;
        public GameObject Prefab;
    }
}