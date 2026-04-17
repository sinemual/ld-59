using Client.Data;
using Client.Data.Core;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class OfflineTimeForDailyTasksSystem : IEcsInitSystem
    {
        private SharedData _data;
        private EcsWorld _world;

        private TimeManagerService _timeManagerService;

        public void Init()
        {
            var sec = 0;/*TimeManagerService.Offline.LoadOfflineBonus().Second;*/
            
            Debug.Log($"{sec}>{60 * 60 * _data.BalanceData.TimeToResetDailyyTasks}?");
            
            /*if (sec > 60 * 60 * _data.BalanceData.TimeToResetDailyyTasks)
                if (_data.PlayerData.TutrorialStates[TutorialStep.GoToHeroMetaUpgradeScreen])
                {
                    _data.PlayerData.IsTodayTaskCompleted = false;
                    _data.PlayerData.TodayCompletedTaskCoutner = 0;
                    _world.NewEntity().Get<StartNextTaskRequest>();
                }*/
        }
    }
}