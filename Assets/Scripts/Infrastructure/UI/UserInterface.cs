using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI.BaseUI;
using UnityEngine;

namespace Client.Infrastructure.UI
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform uiRoot;
        [SerializeField] private RectTransform worldUiRoot;
        [Header("Tutorials")] public SerializedDictionary<TutorialStep, TutorialBaseScreen> TutorialsScreens;

        private List<BaseScreen> _screens;
        private List<GameObject> _worldUiScreens;

        // Services:
        private bool _isUiEnabled;
        private bool _isWorldUiEnabled;
        private SharedData _sharedData;

        public List<GameObject> WorldUiScreens => _worldUiScreens;

        public void Init(SharedData sharedData)
        {
            _sharedData = sharedData;
            _screens = new List<BaseScreen>();
            _worldUiScreens = new List<GameObject>();
            _isUiEnabled = true;
        }

        public T ShowScreen<T>(Action onShownCallback = null) where T : BaseScreen
        {
            var currentScreen = GetScreen<T>();
            if (currentScreen != null)
            {
                currentScreen.SetShowState(true);
                onShownCallback?.Invoke();
                return currentScreen;
            }

            var screenPrefab = Resources.Load<T>("Screens/" + typeof(T).Name);
            var screen = Instantiate(screenPrefab, uiRoot);
            screen.Init();
            screen.Inject(_sharedData);
            
            _screens.Add(screen);

            if (onShownCallback != null)
                screen.ShowScreen += onShownCallback;

            screen.SetShowState(true);
            ReorderScreens();
            return screen;
        }

        public void ShowScreenByScreenType(ScreenType screenType, Action onShownCallback = null)
        {
            foreach (var screen in _sharedData.StaticData.PrefabData.ScreenPrefabs)
                if (screen.Key == screenType)
                {
                    foreach (var scr in _screens)
                    {
                        if (screenType == scr.ScreenType)
                        {
                            scr.SetShowState(true);
                            ReorderScreens();
                            return;
                        }
                    }
                    var screenPrefab = screen.Value;
                    var screenObject = Instantiate(screenPrefab, uiRoot);

                    if (onShownCallback != null)
                        screenObject.ShowScreen += onShownCallback;
                    _screens.Add(screenObject);
                    screenObject.SetShowState(true);
                    ReorderScreens();
                }
        }

        public void HideScreenByType(ScreenType screenType)
        {
            foreach (var scr in _screens)
            {
                if (screenType == scr.ScreenType)
                {
                    scr.SetShowState(false);
                    return;
                }
            }
        }
        
        public void HideScreen<T>(Action onHiddenCallback = null) where T : BaseScreen
        {
            var screen = GetScreen<T>();

            if (screen == null)
            {
                onHiddenCallback?.Invoke();
                return;
            }

            if (onHiddenCallback != null)
            {
                screen.HideScreen += onHiddenCallback;
            }

            screen.SetShowState(false);
        }

        public T GetScreen<T>() where T : BaseScreen
        {
            foreach (var screen in _screens)
                if (screen is T typedScreen)
                    return typedScreen;

            var screenPrefab = Resources.Load<T>("Screens/" + typeof(T).Name);
            var screenObject = Instantiate(screenPrefab, uiRoot);
            screenObject.Init();
            screenObject.Inject(_sharedData);
            _screens.Add(screenObject);
            screenObject.gameObject.SetActive(false);

            return screenObject;
        }

        public void HideAllScreens()
        {
            foreach (var screen in _screens)
                screen.SetShowState(false);
        }

        public void TriggerShowStateAllScreen()
        {
            _isUiEnabled = !_isUiEnabled;
            foreach (var screen in _screens)
                screen.gameObject.SetActive(_isUiEnabled);
        }

        public void TriggerShowStateAllWorldUiScreen()
        {
            _isWorldUiEnabled = !_isWorldUiEnabled;
            foreach (var screen in WorldUiScreens)
                screen.gameObject.SetActive(_isWorldUiEnabled);
        }

        public void AddScreenToWorldUiScreens(GameObject worldUiScreen)
        {
            WorldUiScreens.Add(worldUiScreen);
        }

        public void RemoveScreenToWorldUiScreens(GameObject worldUiScreen)
        {
            WorldUiScreens.Remove(worldUiScreen);
        }

        public void ShowTutorialScreen(TutorialStep step)
        {
            if (TutorialsScreens.ContainsKey(step))
                TutorialsScreens[step].SetShowState(true);
        }

        public void HideTutorialScreen(TutorialStep step)
        {
            if (TutorialsScreens.ContainsKey(step))
                TutorialsScreens[step].SetShowState(false);
        }

        public void ReorderScreens()
        {
             //Debug.Log($"ReorderScreens");
            _screens.Sort((a, b) =>
                a.SortingPriority.CompareTo(b.SortingPriority));

            for (int i = 0; i < _screens.Count; i++)
                _screens[i].transform.SetSiblingIndex(i);
        }

        public Canvas GetMainCanvas() => canvas;
        public RectTransform GetWorldUiRoot() => worldUiRoot;
    }
}