using System.IO;
using Client.DevTools.MyTools;
using Client.Infrastructure.Services;
using Data;
using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Client.Data.Core
{
    public class SharedData : MonoBehaviour
    {
        public SaveData SaveData;
        public StaticData StaticData;
        public BalanceData BalanceData;
        public RuntimeData RuntimeData;
        public SceneData SceneData;
        public AudioData AudioData;
        
        private ISaveLoadService _saveLoadService;

        public void ManualStart(ISaveLoadService saveLoadService)
        {
            Debug.Log(Utility.GetDataPath());

            _saveLoadService = saveLoadService;
            
            RuntimeData = new RuntimeData();

            RuntimeData.InjectData(this);
            SaveData.ResetToDefaults();
            SaveData.BindData(BalanceData.StartMoney, StaticData.TutorialData);
            StaticData.InitComfortableData();

            Load();
        }

        private void Save()
        {
            SaveData.IsGameLaunchedBefore = true;
            _saveLoadService.Save(SaveData, "_0");
        }

        private void Load()
        {
            SaveData = _saveLoadService.Load<SaveData>("_0") ?? SaveData;
        }

        [ExecuteInEditMode]
        public void ResetData()
        {
            SaveData.ResetToDefaults();
        }

#if UNITY_EDITOR
        [ExecuteInEditMode]
        [MenuItem("Tools/DeleteAllGameData")]
        public static void DeleteAllGameData()
        {
            if (Directory.Exists(Utility.GetDataPath()))
                Directory.Delete(Utility.GetDataPath(), true);
        }
#endif
        private void OnApplicationPause(bool pause)
        {
            if (pause)
                Save();
        }

        private void OnApplicationQuit()
        {
            Save();
        }
    }
}