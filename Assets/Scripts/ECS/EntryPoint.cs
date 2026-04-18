using System;
using System.Collections;
using System.Collections.Generic;
using Client.Data;
using Client.Data.Core;
using Client.ECS._Mechanics.Armor.Systems;
using Client.ECS.CurrentGame.Hit.Systems;
using Client.ECS.CurrentGame.Mining;
using Client.Factories;
using Client.Infrastructure.Services;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
#if UNITY_EDITOR
using Leopotam.Ecs.UnityIntegration;
#endif
using UnityEngine;

namespace Client
{
    internal sealed class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        [Header("Data")] [SerializeField] private SharedData data;
        [Header("UserInterface")] [SerializeField] private UserInterface userInterface;

        [Header("Services")] 
        private TimeManagerService _timeManagerService;
        private CameraService _cameraService;
        private ISaveLoadService _saveLoadService;
        private AudioService _audioService;
        private PrefabFactory _prefabFactory;
        private PoolService _poolService;
        private CleanService _cleanService;
        private LoadGameService _loadGameService;
        private SlowMotionService _slowMotionService;
        private AddressablesService _addressablesService;
        private VibrationService _vibrationService;
        private VfxService _vfxService;
        private WorldTextService _worldTextService;
        private DebugService _debugService;

        private EcsWorld _ecsWorld;

        private EcsSystems _updateSystems;
        private EcsSystems _lateUpdateSystems;
        private EcsSystems _fixedUpdateSystems;

        private async void Awake()
        {
            _saveLoadService = new JsonSaveLoadService();
            data.ManualStart(_saveLoadService);
            _timeManagerService = new TimeManagerService(data, this);
            _addressablesService = new AddressablesService();
            _loadGameService = new LoadGameService(data, _addressablesService, userInterface);
            StartCoroutine(ManualStart());
            await _loadGameService.StartGameLoad();
        }

        private IEnumerator ManualStart()
        {
            _ecsWorld = new EcsWorld();
            _updateSystems = new EcsSystems(_ecsWorld, " - UPDATE");
            _lateUpdateSystems = new EcsSystems(_ecsWorld, " - LATE UPDATE");
            _fixedUpdateSystems = new EcsSystems(_ecsWorld, " - FIXED UPDATE");

#if UNITY_EDITOR
            EcsWorldObserver.Create(_ecsWorld);
            EcsSystemsObserver.Create(_updateSystems);
            EcsSystemsObserver.Create(_lateUpdateSystems);
            EcsSystemsObserver.Create(_fixedUpdateSystems);
#endif
            //---Services---
            _audioService = new AudioService(data);
            _vibrationService = new VibrationService(data);
            _debugService = new DebugService();
            _poolService = new PoolService(data);
            _slowMotionService = new SlowMotionService(this);
            _cleanService = new CleanService(_poolService);
            _cameraService = new CameraService(data.SceneData.CameraSceneData, this);
            _prefabFactory = new PrefabFactory(_ecsWorld, _poolService, _cleanService);
            _vfxService = new VfxService(data, _prefabFactory);
            _worldTextService = new WorldTextService(data, _prefabFactory, userInterface);

            //---Injects---
            userInterface.Init(data);

            SetTargetFrameRate();
            ProvideMonoEntitiesFromScene();

            //---SystemGroups---
            var inputSystems = InputSystems();
            var spawnSystems = SpawnSystems();
            var movementSystems = MovementSystems();
            var timerSystems = TimerSystems();
            var userInterfaceSystems = UserInterfaceSystems();

            _updateSystems
                //---GameState---
                .Add(new InitGameSystem())
                .Add(new SetGameStateSystem())
                //---General---
                .Add(new VibrationSystem())
                .Add(new AudioSystem())
                .Add(new CheatSystem())
                .Add(new DespawnLevelSystem())
                .Add(timerSystems)
                .Add(spawnSystems)
                .Add(inputSystems)
                .Add(movementSystems)
                .Add(userInterfaceSystems)
                //---Init---
                .Add(new StartInitLevelSystem())
                .Add(new SpawnEntitiesAtInitLevelSystem())
                .Add(new CompleteInitLevelSystem())
                //---Level---
                .Add(new StartLevelSystem())
                .Add(new StartLevelSoundSystem())
                .Add(new InitGridSystem())
                //---Level State---
                .Add(new LevelProgressSystem())
                .Add(new GameOverSystem())
                //---Manipulator---
                .Add(new ManipulatorSystem())
                //---Blocks---
                //.Add(new DeleteModeSystem())
                .Add(new BuyBlockSystem())
                .Add(new RotateBlockSystem())
                .Add(new TakeBlockSystem())
                .Add(new PlaceBlockSystem())
                .Add(new SellBlockSystem())
                //---SignalChain---
                .Add(new StartSignalChainButtonTapSystem())
                .Add(new SignalChainBuildSystem())
                .Add(new SignalCalculateSystem())
                .Add(new TryToSendSignalSystem())
                //---Currency---
                .Add(new CalculateCurrencySystem())
                .Add(new UserInterfaceCurrencySystem())
                //.Add(new DespawnAtEndOfFrameSystem())
                //---Tutorial---
                .Add(new TutorialSystem())
                //---OneFrames---
                .OneFrame<GameStateChangedEvent>()
                .OneFrame<DeathEvent>()
                .OneFrame<MovingCompleteEvent>()
                .OneFrame<AroundMovingCompleteEvent>()
                .OneFrame<EarnCurrencyEvent>()
                .OneFrame<StepSoundEvent>()
                .OneFrame<SpendCurrncyEvent>()
                .OneFrame<TutorialCompleteEvent>()
                .OneFrame<TapRaycastEvent>()
                .OneFrame<SignalStartButtonTapEvent>()
                //---Injects---
                .Inject(this) // for coroutine runner
                .Inject(data)
                .Inject(userInterface)
                .Inject(_slowMotionService)
                .Inject(_cameraService)
                .Inject(_vibrationService)
                .Inject(_vfxService)
                .Inject(_worldTextService)
                .Inject(_debugService)
                .Inject(_prefabFactory)
                .Inject(_audioService)
                .Inject(_timeManagerService)
                .Init();

            _lateUpdateSystems
                .Add(new WorldUiSystem())
                .Inject(userInterface)
                .Inject(_cameraService)
                .Init();

            _fixedUpdateSystems
                .Add(new DirectionalLaunchSystem())
                .Add(new HeroTriggerSystem())
                .Add(new VelocityMovingSystem())
                .Add(new PhysicForceAddSystem())
                .Add(new MouseRaycastSystem())
                .Add(new TapRaycastSystem())
                //---OneFrames---
                .OneFrame<OnCollisionEnterEvent>()
                .OneFrame<OnCollisionStayEvent>()
                .OneFrame<OnTriggerEnterEvent>()
                .OneFrame<OnTriggerExitEvent>()
                .OneFrame<RaycastEvent>()
                //---Injects---
                .Inject(userInterface)
                .Inject(data)
                .Inject(_prefabFactory)
                .Inject(_vfxService)
                .Inject(_audioService)
                .Inject(_cameraService)
                .Init();

            yield return null;
        }

        private void Update() => _updateSystems?.Run();

        private void LateUpdate() => _lateUpdateSystems?.Run();

        private void FixedUpdate() => _fixedUpdateSystems?.Run();

        private void OnDestroy()
        {
            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _updateSystems = null;

                _lateUpdateSystems.Destroy();
                _lateUpdateSystems = null;

                _fixedUpdateSystems.Destroy();
                _fixedUpdateSystems = null;

                _ecsWorld.Destroy();
                _ecsWorld = null;
            }

            //_debugService.RemoveAllOptionsFromSrDebuger();
        }

        //------------------SYSTEM GROUPS---------------
        private EcsSystems SpawnSystems()
        {
            return new EcsSystems(_ecsWorld, "SpawnSystems")
                .Add(new DespawnAtTimerSystem())
                .Add(new SpawnLevelSystem())
                //.Add(new SpawnMenuLevelSystem())
                .Add(new SpawnPointSystem());
        }

        private EcsSystems InputSystems()
        {
            return new EcsSystems(_ecsWorld, "InputSystems");
        }

        private EcsSystems MovementSystems()
        {
            return new EcsSystems(_ecsWorld, "MovementSystems")
                .Add(new TransformMovingSystem())
                .Add(new LocalPosMovingSystem())
                .Add(new TransformAroundMovingSystem())
                .Add(new ScaleByMovingToTargetSystem())
                .Add(new TweenAroundMovementSystem());
        }

        private EcsSystems TimerSystems()
        {
            return new EcsSystems(_ecsWorld, "TimerSystems")
                .Add(new TimerSystem<EnableTimer>())
                .Add(new TimerSystem<DisableTimer>())
                .Add(new TimerSystem<EnableCanvasTimer>())
                .Add(new TimerSystem<DisableCanvasTimer>())
                .Add(new TimerSystem<DeadDespawnTimer>())
                .Add(new TimerSystem<DelayTimer>())
                .Add(new TimerSystem<ReloadingTimer>())
                .Add(new TimerSystem<TimerToLoot>())
                .Add(new TimerSystem<DespawnTimer>())
                .Add(new TimerSystem<ShowingEarningViewTimer>());
        }

        private EcsSystems UserInterfaceSystems()
        {
            return new EcsSystems(_ecsWorld, "UserInterfaceSystems")
                .Add(new GameScreenSystem())
                .Add(new SignalBlockScreenSystem())
                .Add(new SettingScreenSystem());
        }
        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                if (_timeManagerService.IsNetTimeGot && data.RuntimeData.IsWeHaveInternetTime)
                {
                    _timeManagerService.SaveWithNetTime(ref data.SaveData.IdleRewardTimeKey, TimeSpan.Zero);
                }
            }
        }

        private void OnApplicationQuit()
        {
            if (_timeManagerService.IsNetTimeGot && data.RuntimeData.IsWeHaveInternetTime)
            {
                _timeManagerService.SaveWithNetTime(ref data.SaveData.IdleRewardTimeKey, TimeSpan.Zero);
            }
        }

        private static void SetTargetFrameRate()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void ProvideMonoEntitiesFromScene()
        {
            List<MonoEntity> monoEntities = new List<MonoEntity>();
            monoEntities.AddRange(FindObjectsByType<MonoEntity>(FindObjectsSortMode.None));
            foreach (var monoEntity in monoEntities)
            {
                if(monoEntity.transform.TryGetComponent(out PoolObject _))
                    continue;
                var ecsEntity = _ecsWorld.NewEntity();
                monoEntity.Provide(ref ecsEntity);
            }
        }
    }
}