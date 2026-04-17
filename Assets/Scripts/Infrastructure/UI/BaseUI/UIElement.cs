using System.Collections;
using PrimeTween;
using UnityEngine;

namespace Client.Infrastructure.UI.BaseUI
{
    public abstract class UIElement : MonoBehaviour
    {
        public virtual void SetShowState(bool isShow)
        {
            if (isShow)
                Show();
            else
                Hide();
        }

        protected virtual void Show()
        {
            gameObject.SetActive(true);
            Tween.Scale(transform, 1.0f, 0.25f);
        }
        
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }

        protected virtual void Hide()
        {
            Tween.Scale(transform, 0.0f, 0.25f).OnComplete(() => gameObject.SetActive(false));
        }
    }

    public enum ShowType
    {
        None = 0,
        Scale = 1,
        FromLeft = 2
    }
}