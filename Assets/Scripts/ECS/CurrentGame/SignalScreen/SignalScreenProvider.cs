using System;
using TMPro;
using UnityEngine;

[Serializable]
public struct SignalScreenProvider
{
    public TextMeshProUGUI SignalText;
    public TextMeshProUGUI BestSignalText;
    public TextMeshProUGUI ErrorText;
    public TextMeshProUGUI EndText;
    public GameObject ProjectSolutionCharacter;
    public GameObject SignalCharacter;
    public Camera SpaceCamera;
}