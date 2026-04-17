using PrimeTween;
using UnityEngine;

namespace Client.Infrastructure.UI.BaseUI
{
    public class AnimatedUIButton : UIButton
    {
        [SerializeField] private float scaleMultiplier = 0.9f;
        [SerializeField]  private float scaleDuration = 0.05f;

        private float _defaultScale = 1.0f;

        public void OverrideScale(float scale) => _defaultScale = scale;

        protected override void OnPress()
        {
            base.OnPress();
            
            transform.localScale = Vector3.one;
            Tween.Scale(transform, Vector3.one * scaleMultiplier, scaleDuration);
        }

        protected override void OnUnpress()
        {
            base.OnUnpress();

            transform.localScale = Vector3.one * scaleMultiplier;
            Tween.Scale(transform, _defaultScale, scaleDuration);
        }
    }
}