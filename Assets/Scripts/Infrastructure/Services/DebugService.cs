using System;
using System.Text;
using Leopotam.Ecs;
using SRDebugger;
using UnityEngine;

namespace Client.Infrastructure.Services
{
    public class DebugService
    {
        private DynamicOptionContainer _container;

        public DebugService()
        {
            _container = new DynamicOptionContainer();
            SRDebug.Instance.AddOptionContainer(_container);
        }

        public void AddOptionToSrDebuger(string name, Action callback)
        {
            _container.AddOption(OptionDefinition.FromMethod(name, callback));
        }

        public void RemoveAllOptionsFromSrDebuger()
        {
            SRDebug.Instance.RemoveOptionContainer(_container);
        }

        public void Log(string log)
        {
            Debug.Log($"<color=white><b>{log}</b></color>");
        }

        public void LogCurrentDebug(string log)
        {
            string s = $"<color=purple><b>{log}</b></color>";
            s = s.ToUpper();
            Debug.Log(s);
        }

        public void LogError(string log)
        {
            Debug.LogError($"<color=red><b>{log}</b></color>");
        }

        public void LogMyWarning(string log)
        {
            Debug.Log($"MyError: <color=yellow><b>{log}</b></color>");
        }

        public void LogWarning(string log)
        {
            Debug.LogWarning($"<color=warning><b>{log}</b></color>");
        }

        public void LogSystemWork(IEcsRunSystem log)
        {
            return;
            Debug.Log($"<color=blue><b>{log}</b></color>");
        }

        public void LogSystemWork(IEcsRunSystem log, object additionalLog)
        {
            return;
            Debug.Log($"<color=blue><b>{log} - {additionalLog}</b></color>");
        }

        public void LogSystemInit(IEcsInitSystem log)
        {
            Debug.Log($"<color=green><b>{log}</b></color>");
        }

        public void LogWithGo(string log, GameObject go)
        {
            Debug.Log($"{log}", go);
        }

        public void LogWithEcsInfo(string log, EcsEntity entity)
        {
            Debug.Log($"{log} {GetDebugEcsInfo(entity)}");
        }

        public void LogWithGoAndEcsInfo(string log, GameObject go, EcsEntity entity)
        {
            Debug.Log($"{log} [{go.name}] {GetDebugEcsInfo(entity)}", go);
        }

        private StringBuilder GetDebugEcsInfo(EcsEntity entity)
        {
            int count = entity.GetComponentsCount();
            Type[] types = new Type[count];
            entity.GetComponentTypes(ref types);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
                sb.Append($"{types[i]}:");
            return sb;
        }
    }
}