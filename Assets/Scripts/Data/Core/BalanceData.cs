using Data.Base;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "GameData/BalanceData", fileName = "BalanceData")]
    public class BalanceData : BaseDataSO
    {
        [Header("Time")]
        [field: SerializeField] public float TimeToDropItem { get; private set; }
        [field: SerializeField] public float TimeToDespawnDeadBody { get; private set; }
        [field: SerializeField] public float TimeToResetDailyyTasks { get; private set; }
        [field: SerializeField] public float DespawnVfxTime { get; private set; }
        [field: SerializeField] public float HideWorldUiTime { get; private set; }
        [field: SerializeField] public AnimationCurve CutOffCurve { get; private set; }

        [Header("OfflineBonus")]
        [field: SerializeField] public Vector2 OfflineTimeHourCap { get; private set; }

        [Header("Nums Balance")]
        [field: SerializeField]
        public int StartMoney { get; private set; }

        [field: SerializeField] public int BaseLevelReward { get; private set; }
        [field: SerializeField] public float LevelMultiCoef { get; private set; }
        [field: SerializeField] public float LevelBaseCoef { get; private set; }
        [field: SerializeField] public float DistanceBetweenBlocks { get; private set; }
        [field: SerializeField] public float DistanceToTriggerInteraction { get; private set; }
        [field: SerializeField] public float TimeToTriggerInteraction { get; private set; }
        [field: SerializeField] public int WinSignal { get; private set; }
        [field: SerializeField] public Vector3 ButtonLocalPos { get; private set; }
        [field: SerializeField] public Vector3 ButtonStartScale { get; private set; }
        [field: SerializeField] public float ButtonPressDuration { get; private set; }
        [field: SerializeField] public float StartFov { get; private set; }
        [field: SerializeField] public float FovCoef { get; private set; }

        [Header("Character")]
        [field: SerializeField]
        public float CharacterRotateSpeed { get; private set; }

        [Header("Physic Balance")]
        [field: SerializeField]
        public Vector2 LaunchDropItemForce { get; private set; }

        [field: SerializeField] public float ExplosionRadius { get; private set; }
        [field: SerializeField] public float ExplosionForce { get; private set; }
        [field: SerializeField] public float DropItemSpeed { get; private set; }
        [field: SerializeField] public float PushForceCoef { get; private set; }

        [Header("Debug")]
        [field: SerializeField]
        public bool IsTestUpgrades { get; private set; }

        [field: SerializeField] public bool IsShowUpgradesButton { get; private set; }

        public override void ResetData()
        {
        }

        /*[Button]
        [ExecuteInEditMode]
        public void WriteTaskDataIdInListOrder()
        {
            var counter = 0;
            foreach (var data in TasksData)
            {
                data.Id = counter;
                counter++;
            }
        }*/
    }
}