using System.Collections;
using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TutorialSystem : IEcsInitSystem, IEcsRunSystem
    {
        private SharedData _data;
        private UserInterface _ui;
        private EcsWorld _world;
        private CameraService _cameraService;
        private ICoroutineRunner _coroutineRunner;

        private EcsFilter<TutorialProvider> _tutorialFilter;
        private EcsFilter<StartTutorialRequest> _startFilter;
        private EcsFilter<CompleteTutorialRequest> _completeFilter;
        private EcsFilter<AttentionPointProvider> _attentionPointFilter;
        private EcsFilter<CurrentTargetPoint> _currentTargetFilter;

        public void Init()
        {
            //return;
            _coroutineRunner.StartCoroutine(DelayStart());
        }

        private IEnumerator DelayStart()
        {
            yield return new WaitForSeconds(0.5f);
            if (!ReturnToTheGameCheckTutorial())
                CheckTutorialStates();
        }

        public void Run()
        {
            foreach (var idx in _startFilter)
            {
                ref var entity = ref _startFilter.GetEntity(idx);
                ref var startRequest = ref entity.Get<StartTutorialRequest>();

                //_ui.ShowTutorialScreen(startRequest.TutorialStep);
                Debug.Log($"startRequest.TutorialStep 1: {startRequest.TutorialStep}");
                _data.SaveData.CurrentTutorialStep = startRequest.TutorialStep;
                if (IsNextStepCompleted())
                {
                    entity.Del<StartTutorialRequest>();
                    continue;
                }

                Debug.Log($"startRequest.TutorialStep 2: {startRequest.TutorialStep}");
                //Debug.Log($"_startFilter");

                foreach (var tutr in _tutorialFilter)
                {
                    ref var tutrEntity = ref _tutorialFilter.GetEntity(tutr);
                    ref var tutorialProvider = ref tutrEntity.Get<TutorialProvider>();
                    ref var tutorialProviderGo = ref tutrEntity.Get<GameObjectProvider>().Value;
                    //Debug.Log($"tutorialProviderGo: {tutorialProviderGo}", tutorialProviderGo);
                    if (startRequest.TutorialStep == tutorialProvider.TutorialStep)
                    {
                        //Debug.Log($"tutorialProviderGo: {tutorialProviderGo}", tutorialProviderGo);
                        if (tutorialProvider.TutorialHand)
                        {
                            //Debug.Log($"{tutorialProvider.TutorialHand}", tutorialProvider.TutorialHand);
                            tutorialProvider.TutorialHand.SetActive(true);
                        }

                        if (tutorialProvider.IsUiTutorial /*&& tutorialProvider.UiElementCanvas.transform.gameObject.activeInHierarchy*/)
                        {
                            tutorialProvider.SavedSortingOrder = tutorialProvider.UiElementCanvas.sortingOrder;
                            tutorialProvider.MonoProvider.IsSortingOverrided = true;
                            tutorialProvider.Button.gameObject.SetActive(true);
                            tutorialProvider.UiElementCanvas.sortingOrder = 10;
                            tutorialProvider.UiElementCanvas.overrideSorting = true;
                            tutorialProvider.MonoProvider.UpdateSortingOverride();
                            tutorialProvider.Button.Clicked += CompleteUiTutorial;
                            if (tutorialProvider.IsStopTime)
                                Time.timeScale = 0.0f;
                        }
                    }
                }

                var data = _data.StaticData.TutorialDataByStep[startRequest.TutorialStep];
                if (data.IsArrowNavigationNeeded)
                {
                    foreach (var idy in _currentTargetFilter)
                    {
                        ref var currentTargetEntity = ref _currentTargetFilter.GetEntity(idy);
                        currentTargetEntity.Del<CurrentTargetPoint>();
                    }

                    foreach (var idp in _attentionPointFilter)
                    {
                        ref var attentionPointEntity = ref _attentionPointFilter.GetEntity(idp);
                        ref var attentionPoint = ref attentionPointEntity.Get<AttentionPointProvider>();
                        ref var attentionPointGo = ref attentionPointEntity.Get<GameObjectProvider>().Value;

                        //Debug.Log($"data.IsArrowNavigationNeeded {attentionPoint.PointId} == {data.PointId}");
                        if (attentionPoint.PointId == data.PointId)
                        {
                            //Debug.Log($"data.IsArrowNavigationNeeded YES {attentionPoint.PointId == data.PointId}", attentionPointGo.transform);
                            attentionPointEntity.Get<CurrentTargetPoint>().Value = attentionPointGo.transform.position + attentionPoint.Offset;
                        }
                    }
                }

                if (data.IsCameraMovingToPoint)
                {
                    foreach (var idp in _attentionPointFilter)
                    {
                        ref var attentionPointEntity = ref _attentionPointFilter.GetEntity(idp);
                        ref var attentionPoint = ref attentionPointEntity.Get<AttentionPointProvider>();
                        ref var attentionPointGo = ref attentionPointEntity.Get<GameObjectProvider>().Value;

                        if (attentionPoint.PointId == data.PointId)
                            _cameraService.ShowPointWithTime(attentionPointGo.transform, 1.2f);
                    }
                }

                entity.Del<StartTutorialRequest>();
            }

            foreach (var idx in _completeFilter)
            {
                ref EcsEntity entity = ref _completeFilter.GetEntity(idx);
                ref CompleteTutorialRequest completeRequest = ref entity.Get<CompleteTutorialRequest>();

                _ui.HideTutorialScreen(completeRequest.TutorialStep);
                /*if (tutorialProvider.TutorialHand)
                    tutorialProvider.TutorialHand.SetActive(true);*/
                _data.SaveData.TutrorialStates[completeRequest.TutorialStep] = true;

                foreach (var tutr in _tutorialFilter)
                {
                    ref var tutrEntity = ref _tutorialFilter.GetEntity(tutr);
                    ref var tutorialProvider = ref tutrEntity.Get<TutorialProvider>();
                    if (completeRequest.TutorialStep == tutorialProvider.TutorialStep)
                        if (tutorialProvider.TutorialHand)
                            tutorialProvider.TutorialHand.SetActive(false);
                }

                _world.NewEntity().Get<TutorialCompleteEvent>().TutorialStep = completeRequest.TutorialStep;
                entity.Del<CompleteTutorialRequest>();

                CheckTutorialStates();
            }
        }

        private void CheckTutorialStates()
        {
            Debug.Log($"CheckTutorialStates() 0");
            if (_data.SaveData.TutrorialStates[_data.SaveData.CurrentTutorialStep] &&
                _data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].IsAutoStartNextStep &&
                !_data.SaveData.TutrorialStates[
                    _data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].NextTutorialData.TutorialStep])
            {
                Debug.Log($"CheckTutorialStates() 1");
                _world.NewEntity().Get<StartTutorialRequest>().TutorialStep =
                    (TutorialStep)(_data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].NextTutorialData.TutorialStep);
                return;
            }

            if (_data.SaveData.TutrorialStates[_data.SaveData.CurrentTutorialStep] &&
                _data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].NextTutorialData &&
                _data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].NextTutorialData.IsShowByMoney)
            {
                if (_data.SaveData.Currency >= _data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].NextTutorialData.ShowMoney)
                {
                    Debug.Log($"CheckTutorialStates() 2");
                    _world.NewEntity().Get<StartTutorialRequest>().TutorialStep =
                        (TutorialStep)(_data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].NextTutorialData.TutorialStep);
                    return;
                }
            }


            foreach (var tutorialData in _data.StaticData.TutorialData)
            {
                //Debug.Log($"tutorialData.TutorialStep: {tutorialData.TutorialStep}");
                if (tutorialData.IsShowByMoney && _data.SaveData.Currency >= tutorialData.ShowMoney &&
                    !_data.SaveData.TutrorialStates[tutorialData.TutorialStep] &&
                    _data.SaveData.TutrorialStates[tutorialData.PreviousTutorialData.TutorialStep])
                {
                    Debug.Log($"CheckTutorialStates() 3");
                    _world.NewEntity().Get<StartTutorialRequest>().TutorialStep = tutorialData.TutorialStep;
                    return;
                }
            }
        }

        private bool IsNextStepCompleted()
        {
            bool isNextStepCompleted = false;
            if (_data.SaveData.TutrorialStates[_data.SaveData.CurrentTutorialStep] &&
                _data.SaveData.TutrorialStates[_data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].NextTutorialData.TutorialStep])
            {
                isNextStepCompleted = true;
                _data.SaveData.CurrentTutorialStep =
                    _data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].NextTutorialData.TutorialStep;
                CheckTutorialStates();
            }

            return isNextStepCompleted;
        }

        private bool ReturnToTheGameCheckTutorial()
        {
            Debug.Log(
                $"ReturnToTheGameCheckTutorial {_data.SaveData.CurrentTutorialStep} {_data.SaveData.TutrorialStates[_data.SaveData.CurrentTutorialStep]}");
            if (_data.SaveData.TutrorialStates[_data.SaveData.CurrentTutorialStep])
                return false;

            var isHaveTutrDontCompleted = false;

            while (_data.SaveData.CurrentTutorialStep > 0 &&
                   _data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].PreviousTutorialData.IsAutoStartNextStep)
            {
                _data.SaveData.CurrentTutorialStep =
                    _data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].PreviousTutorialData.TutorialStep;
                _data.SaveData.TutrorialStates[_data.SaveData.CurrentTutorialStep] = false;
                isHaveTutrDontCompleted = true;
            }

            if (isHaveTutrDontCompleted)
            {
                if (_data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].IsShowByMoney)
                {
                    if (_data.SaveData.Currency >= _data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].ShowMoney)
                        _world.NewEntity().Get<StartTutorialRequest>().TutorialStep =
                            (TutorialStep)(_data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].TutorialStep);
                }
                else
                {
                    _world.NewEntity().Get<StartTutorialRequest>().TutorialStep =
                        (TutorialStep)(_data.StaticData.TutorialDataByStep[_data.SaveData.CurrentTutorialStep].TutorialStep);
                    //Debug.Log($"isHaveTutrDontCompleted");
                }
            }

            return isHaveTutrDontCompleted;
        }

        /*private bool ReturnToTheGameCheckTutorial()
        {
            bool hasUncompleted = false;

            while (_data.SaveData.CurrentTutorialStep > 0)
            {
                int prevStepIndex = (int)_data.SaveData.CurrentTutorialStep - 1;

                if (!_data.StaticData.TutorDependenceByStep[(TutorialStep)prevStepIndex])
                    break;

                var prevStep = (TutorialStep)prevStepIndex;

                _data.SaveData.CurrentTutorialStep = prevStep;
                _data.SaveData.TutrorialStates[prevStep] = false;

                hasUncompleted = true;
            }

            if (hasUncompleted)
                _world.NewEntity().Get<StartTutorialRequest>().TutorialStep = _data.SaveData.CurrentTutorialStep;

            return hasUncompleted;
        }*/

        private void CompleteUiTutorial()
        {
            _ui.HideTutorialScreen(_data.SaveData.CurrentTutorialStep);

            _data.SaveData.TutrorialStates[_data.SaveData.CurrentTutorialStep] = true;

            foreach (var tutr in _tutorialFilter)
            {
                ref var tutrEntity = ref _tutorialFilter.GetEntity(tutr);
                ref var tutorialProvider = ref tutrEntity.Get<TutorialProvider>();
                if (_data.SaveData.CurrentTutorialStep == tutorialProvider.TutorialStep)
                {
                    if (tutorialProvider.TutorialHand)
                        tutorialProvider.TutorialHand.SetActive(false);
                    if (tutorialProvider.IsUiTutorial)
                    {
                        tutorialProvider.MonoProvider.IsSortingOverrided = false;
                        tutorialProvider.UiElementCanvas.overrideSorting = false;
                        tutorialProvider.UiElementCanvas.sortingOrder = tutorialProvider.SavedSortingOrder;
                        tutorialProvider.MonoProvider.UpdateSortingOverride();
                        tutorialProvider.Button.Clicked -= CompleteUiTutorial;
                        if (tutorialProvider.IsStopTime)
                            Time.timeScale = 1.0f;
                    }
                }
            }

            CheckTutorialStates();
        }
    }

    public struct TutorialCompleteEvent
    {
        public TutorialStep TutorialStep;
    }
}