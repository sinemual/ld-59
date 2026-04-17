using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Client.Infrastructure.UI.BaseUI
{
    [RequireComponent(typeof(Button))]
    public class UIButton : UIElement, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler, IPointerClickHandler
    {
        private bool _isPointerEntered;
        private bool _isPressed;
        private bool _isReadyToClick;
        private bool _isInteractable = true;
        
        public event Action Clicked;
        public event Action<bool> StateChanged;
        
        public bool Interactable
        {
            get => _isInteractable;
            set
            {
                _isInteractable = value;
                StateChanged?.Invoke(_isInteractable);
            }
        }
        
        public virtual void SetInteractable(bool value)
        {
            _isInteractable = value;
        }

        protected virtual void OnClick()
        {
            Clicked?.Invoke();
        }
        
        public void SimulateClick()
        {
            Clicked?.Invoke();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isInteractable)
            {
                return;
            }

            _isPointerEntered = true;
            OnPress();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isPressed)
            {
                _isPressed = false;
                _isReadyToClick = false;
                OnUnpress();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isPointerEntered)
            {
                _isPressed = false;
                OnUnpress();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isReadyToClick)
            {
                return;
            }

            OnClick();

            _isPressed = false;
            _isReadyToClick = false;
            OnUnpress();
        }

        protected virtual void OnPress()
        {
            _isPressed = true;
            _isReadyToClick = true;
        }

        protected virtual void OnUnpress()
        {
            _isPointerEntered = false;
        }
    }
}