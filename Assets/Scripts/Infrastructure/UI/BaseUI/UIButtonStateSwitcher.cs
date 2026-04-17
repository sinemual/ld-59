using UnityEngine;

namespace Client.Infrastructure.UI.BaseUI
{
    public class UIButtonStateSwitcher : MonoBehaviour
    {
        [SerializeField] private UIButton button;
        [SerializeField] private GameObject activeState;
        [SerializeField] private GameObject inactiveState;

        private void Awake()
        {
            UpdateState(button);
            button.StateChanged += UpdateState;
        }

        private void UpdateState(bool value)
        {
            activeState.SetActive(value);
            inactiveState.SetActive(!value);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!Application.isPlaying && button == null)
            {
                button = GetComponent<UIButton>();
            }
        }
#endif
    }
}