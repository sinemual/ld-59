using System;
using AYellowpaper.SerializedCollections;
using Cinemachine;
using UnityEngine;

namespace Client.Data
{
    [Serializable]
    public class CameraSceneData
    {
        public Camera MainCamera;
        public CinemachineImpulseSource ShakeSource;

        [Header("VCs")] public SerializedDictionary<CameraType, CinemachineVirtualCameraBase> Cameras;
    }
}