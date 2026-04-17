using System;
using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.Services;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using SRDebugger;
using UnityEngine;

namespace Client
{
    public class CheatSystem : IEcsInitSystem, IEcsRunSystem
    {
        private SharedData _data;
        private UserInterface _ui;
        private EcsWorld _world;
        private DebugService _debugService;

        private SlowMotionService _slowMotionService;


        public void Init()
        {
            _debugService.AddOptionToSrDebuger("CheatGetMoney", CheatGetMoney);
            _debugService.AddOptionToSrDebuger("CheatGetHeroLevel", CheatGetHeroLevel);
            _debugService.AddOptionToSrDebuger("CheatNextLevel", CheatNextLevel);
            _debugService.AddOptionToSrDebuger("CheatHideUi", CheatHideUi);
            _debugService.AddOptionToSrDebuger("CheatResetPlayerData", CheatResetPlayerData);
            _debugService.AddOptionToSrDebuger("OpenAllWorlds", OpenAllWorlds);
            _debugService.AddOptionToSrDebuger("CompleteAllTutorials", CompleteAllTutorials);
            _debugService.AddOptionToSrDebuger("GoToPredLastLevel", GoToPredLastLevel);
            _debugService.AddOptionToSrDebuger("TimeScale_One", TimeScale_One);
            _debugService.AddOptionToSrDebuger("TimeScale_Three", TimeScale_Three);
            _debugService.AddOptionToSrDebuger("TimeScale_Five", TimeScale_Five);
            _debugService.AddOptionToSrDebuger("MaxUpgrades", MaxUpgrades);
            _debugService.AddOptionToSrDebuger("ObnulenieMoney", ObnulenieMoney);
        }

        public void Run()
        {
            if (Input.GetKeyDown(KeyCode.R))
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    _data.ResetData();

            if (Input.GetKeyDown(KeyCode.Alpha1)) Time.timeScale = Math.Abs(Time.timeScale - 1.0f) < 0.01 ? 0.0f : 1.0f;

            if (Input.GetKeyDown(KeyCode.Alpha2)) Time.timeScale = 3.0f;

            if (Input.GetKeyDown(KeyCode.Alpha3)) Time.timeScale = 5.0f;

            if (Input.GetKeyDown(KeyCode.U)) _ui.TriggerShowStateAllScreen();

            //if (Input.GetKeyDown(KeyCode.S)) _slowMotionService.StartSlowMotion(0.2f, 1.0f, 5.0f);

            if (Input.GetKeyDown(KeyCode.T))
                CompleteAllTutorials();

            /*if (Input.GetKeyDown(KeyCode.N))
                OpenAllWorlds();*/

            if (Input.GetKeyDown(KeyCode.L))
                CheatNextLevel();

            if (Input.GetKeyDown(KeyCode.M))
                CheatGetMoney();

            if (Input.GetKeyDown(KeyCode.J))
            {
                GoToPredLastLevel();
                MaxUpgrades();
                CompleteAllTutorials();
            }
        }

        private void CheatGetMoney()
        {
            _world.NewEntity().Get<AddCurrencyRequest>().Value = 100;
        }
        
        private void CheatGetHeroLevel() => _data.SaveData.PlayerLevel += 10;

        private void CheatNextLevel()
        {
            _data.SaveData.LevelIdx += 1;
            _data.SaveData.EventLevelIdx += 1;
        }

        private void CompleteAllTutorials()
        {
            /*for (int i = 0; i < _data.SaveData.TutrorialStates.Count; i++)
                _world.NewEntity().Get<CompleteTutorialRequest>().TutorialStep = (TutorialStep)i;*/
            for (int i = 0; i < _data.SaveData.TutrorialStates.Count; i++)
                _data.SaveData.TutrorialStates[(TutorialStep)i] = true;
        }

        private void TimeScale_One() => Time.timeScale = 1.0f;
        private void TimeScale_Three() => Time.timeScale = 3.0f;
        private void TimeScale_Five() => Time.timeScale = 5.0f;

        private void MaxUpgrades()
        {
            /*foreach (var upgrade in _data.StaticData.AllUpgradeDataByUpgradeId)
                _data.SaveData.UpgradesLevel[upgrade.Key] = _data.StaticData.AllUpgradeDataByUpgradeId[upgrade.Key].MaxLevel;*/
        }

        private void OpenAllWorlds()
        {
            _data.SaveData.EventLevelIdx = 100;
        }

        private void GoToPredLastLevel()
        {
        }

        private void ObnulenieMoney()
        {
            _data.SaveData.Currency = 0;
            _world.NewEntity().Get<AddCurrencyRequest>().Value = 0;
        }

        private void CheatHideUi() => _ui.TriggerShowStateAllScreen();

        private void CheatResetPlayerData()
        {
            _data.ResetData();
            PlayerPrefs.DeleteAll();
        }
    }
}