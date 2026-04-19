using System;
using Client.Data.Core;

namespace Client.Infrastructure.UI.BaseUI
{
    public abstract class BaseScreen : UIElement
    {
        public ScreenType ScreenType;
        public int SortingPriority;
        public event Action HideScreen;
        public event Action ShowScreen;

        protected SharedData Data;
        private bool _isScreenShowed;

        public bool IsScreenShowed => _isScreenShowed;

        public void Inject(SharedData data) => Data = data;

        public void Init()
        {
            _isScreenShowed = false;
            ManualStart();
        }

        public override void SetShowState(bool isShow) // [System.Runtime.CompilerServices.CallerMemberName] string memberName = "" - WHO
        {
            base.SetShowState(isShow);
            if (isShow)
            {
                ShowScreen?.Invoke();
                _isScreenShowed = true;
            }
            else
            {
                HideScreen?.Invoke();
                _isScreenShowed = false;
            }
        }

        protected void OnShowScreen() => ShowScreen?.Invoke();
        protected void OnHideScreen() => HideScreen?.Invoke();

        protected virtual void ManualStart()
        {
        }
    }

    public enum ScreenType
    {
        None = 0,
        GameScreen = 1,
        SettingsScreen = 2,
        SignalBlockScreen = 3,
        StartScreen = 4
    }
}