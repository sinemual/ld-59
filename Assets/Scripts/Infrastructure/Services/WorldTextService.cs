using Client.Data.Core;
using Client.ECS.CurrentGame.Damage.Providers;
using Client.Factories;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Infrastructure.Services
{
    public class WorldTextService
    {
        private SharedData _data;
        private PrefabFactory _prefabFactory;
        private UserInterface _userInterface;

        public WorldTextService(SharedData data, PrefabFactory prefabFactory, UserInterface userInterface)
        {
            _data = data;
            _prefabFactory = prefabFactory;
            _userInterface = userInterface;
        }

        public EcsEntity CreateWorldText(GameObject prefab, Vector3 createPosition, Quaternion quaternion, Transform parent = null)
        {
            EcsEntity spawnEntity = _prefabFactory.Spawn(prefab, createPosition, quaternion,
                parent);

            //spawnEntity.Get<GameObjectProvider>().Value.transform.DOMove(createPosition, 0.5f);
            //spawnEntity.Get<GameObjectProvider>().Value.transform.DOScale(Vector3.one * 1.5f, 0.5f);

            spawnEntity.Get<DespawnAtTimerRequest>();
            spawnEntity.Get<Timer<DespawnTimer>>().Value = _data.BalanceData.HideWorldUiTime;
            _userInterface.AddScreenToWorldUiScreens(spawnEntity.Get<GameObjectProvider>().Value);
            return spawnEntity;
        }
        
        public EcsEntity CreateWorldTextWithFade(GameObject prefab, Vector3 createPosition, Quaternion quaternion, Transform parent = null)
        {
            EcsEntity spawnEntity = _prefabFactory.Spawn(prefab, createPosition, quaternion, parent);

            //spawnEntity.Get<GameObjectProvider>().Value.transform.DOMove(createPosition + Vector3.up * 0.3f, 0.9f);
            //spawnEntity.Get<GameObjectProvider>().Value.transform.DOScale(Vector3.one * 1.5f, 0.9f);
            //spawnEntity.Get<WorldUiTextProvider>().Text.DOFade(0.0f, 0.9f);

            spawnEntity.Get<DespawnAtTimerRequest>();
            spawnEntity.Get<Timer<DespawnTimer>>().Value = _data.BalanceData.HideWorldUiTime;
            _userInterface.AddScreenToWorldUiScreens(spawnEntity.Get<GameObjectProvider>().Value);
            return spawnEntity;
        }
        
        public EcsEntity CreateWorldTextWithSinSize(GameObject prefab, Vector3 createPosition, Quaternion quaternion, Transform parent = null)
        {
            EcsEntity spawnEntity = _prefabFactory.Spawn(prefab, createPosition, quaternion, parent);
            
            spawnEntity.Get<GameObjectProvider>().Value.transform.localScale = Vector3.zero;
            //spawnEntity.Get<GameObjectProvider>().Value.transform.DOMove(createPosition + Vector3.up * 0.3f, 0.5f);
            //spawnEntity.Get<GameObjectProvider>().Value.transform.DOScale(Vector3.one * 1.5f, 0.3f).SetLoops(2, LoopType.Yoyo);

            spawnEntity.Get<DespawnAtTimerRequest>();
            spawnEntity.Get<Timer<DespawnTimer>>().Value = _data.BalanceData.HideWorldUiTime;
            _userInterface.AddScreenToWorldUiScreens(spawnEntity.Get<GameObjectProvider>().Value);
            return spawnEntity;
        }
        
        public EcsEntity CreateWorldTextWithSinSizeAndSettings(GameObject prefab, string text, Vector3 createPosition, Quaternion quaternion, Transform parent = null,
            Color color = default, float duration = 1.0f, Vector3 scale = default)
        {
            EcsEntity spawnEntity = _prefabFactory.Spawn(prefab, createPosition, quaternion, parent);

            var tr = spawnEntity.Get<GameObjectProvider>().Value.transform;
            tr.localScale = Vector3.zero;
            Tween.Position(tr, endValue: createPosition + Vector3.up * 0.5f, duration);
            Tween.Scale(tr, endValue: scale, duration, cycles: 2, cycleMode: CycleMode.Yoyo);
            //Tween.Alpha(spawnEntity.Get<WorldUiTextProvider>().Text, endValue: 0f, duration);
            spawnEntity.Get<WorldUiTextProvider>().Text.text = $"{text}";
            if (color != default)
                spawnEntity.Get<WorldUiTextProvider>().Text.color = color;
            
            spawnEntity.Get<DespawnAtTimerRequest>();
            spawnEntity.Get<Timer<DespawnTimer>>().Value = _data.BalanceData.HideWorldUiTime;
            _userInterface.AddScreenToWorldUiScreens(spawnEntity.Get<GameObjectProvider>().Value);
            return spawnEntity;
        }
        
        public EcsEntity CreateWorldTextWithFadeAndSettings(GameObject prefab, string text, Vector3 createPosition, Quaternion quaternion, Transform parent = null,
            Color color = default, float duration = 1.0f, Vector3 scale = default)
        {
            EcsEntity spawnEntity = _prefabFactory.Spawn(prefab, createPosition, quaternion, parent);

            var tr = spawnEntity.Get<GameObjectProvider>().Value.transform;
            Tween.Alpha(spawnEntity.Get<WorldUiTextProvider>().Text, 1.0f, 0.0f);
            //tr.localScale = Vector3.zero;
            Tween.Position(tr, endValue: createPosition + Vector3.up * 0.5f, duration);
            //Tween.Scale(tr, endValue: scale, duration, cycles: 2, cycleMode: CycleMode.Yoyo);
            //Tween.Alpha(spawnEntity.Get<WorldUiTextProvider>().Text, endValue: 0f, duration);
            Tween.Alpha(spawnEntity.Get<WorldUiTextProvider>().Text, 0.0f, duration);
            spawnEntity.Get<WorldUiTextProvider>().Text.text = $"{text}";
            if (color != default)
                spawnEntity.Get<WorldUiTextProvider>().Text.color = color;
            spawnEntity.Get<DespawnAtTimerRequest>();
            spawnEntity.Get<Timer<DespawnTimer>>().Value = _data.BalanceData.HideWorldUiTime;
            _userInterface.AddScreenToWorldUiScreens(spawnEntity.Get<GameObjectProvider>().Value);
            return spawnEntity;
        }
    }
}