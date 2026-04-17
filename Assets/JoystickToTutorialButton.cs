using System;
using System.Collections;
using System.Collections.Generic;
using Client.Infrastructure.UI.BaseUI;
using UnityEngine;

namespace Client
{
    public class JoystickToTutorialButton : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private UIButton tutorialButton;

        private void Awake()
        {
            joystick.PointerDown += tutorialButton.SimulateClick;
        }
    }
}
