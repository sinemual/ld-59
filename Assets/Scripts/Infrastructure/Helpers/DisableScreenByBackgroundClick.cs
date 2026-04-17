using System.Collections;
using System.Collections.Generic;
using Client.Infrastructure.UI.BaseUI;
using UnityEngine;

namespace Client
{
    public class DisableScreenByBackgroundClick : MonoBehaviour
    {
        [SerializeField] private UIButton button;
        [SerializeField] private BaseScreen screen;
        
        void Awake()
        {
            button.Clicked += () => screen.SetShowState(false);
        }
    }
}
