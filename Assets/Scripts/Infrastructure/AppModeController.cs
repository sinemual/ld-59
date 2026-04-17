using System.Collections.Generic;
using Cinemachine;
using Client.Data.Core;
using Client.DevTools.MyTools;
using TriInspector;
/*#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif*/
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class AppModeController : MonoBehaviour/*, IPreprocessBuildWithReport*/
    {
        public AppMode AppMode;

        public SharedData Data;
        
        public float CreativeCameraChangeValue;
        public List<CinemachineVirtualCamera> Cameras;
        public List<float> NormalFOV;

        public List<GameObject> ForRelease;
        public List<GameObject> ForRecording;
        public List<Image> ForRecordingImages;
        public List<GameObject> ForDebug;
        
/*#if UNITY_EDITOR
        public int callbackOrder { get; }
        public void OnPreprocessBuild(BuildReport report)
        {
            if (EditorUserBuildSettings.buildAppBundle)
                AppMode = AppMode.Release;
        }
#endif*/
        
        void Awake()
        {
            if (AppMode == AppMode.Release)
            {
                foreach (var go in ForDebug)
                    go.SetActive(false);

                foreach (var go in ForRecording)
                    go.SetActive(false);
                
                foreach (var go in ForRelease)
                    go.SetActive(true);

                foreach (var img in ForRecordingImages)
                    img.color = Utility.exploredAlpha;

                for (int i = 0; i < Cameras.Count; i++)
                    Cameras[i].m_Lens.FieldOfView = NormalFOV[i];

                Data.StaticData.AlwaysLoadWorldId = -1;

            }
            else if (AppMode == AppMode.Debug)
            {
                foreach (var go in ForRelease)
                    go.SetActive(false);

                foreach (var go in ForRecording)
                    go.SetActive(false);
                
                foreach (var go in ForDebug)
                    go.SetActive(true);

                foreach (var img in ForRecordingImages)
                    img.color = Utility.exploredAlpha;

                for (int i = 0; i < Cameras.Count; i++)
                    Cameras[i].m_Lens.FieldOfView = NormalFOV[i];
            }
            else if (AppMode == AppMode.Creative)
            {
                foreach (var go in ForRelease)
                    go.SetActive(false);

                foreach (var go in ForDebug)
                    go.SetActive(false);

                foreach (var go in ForRecording)
                    go.SetActive(true);

                foreach (var img in ForRecordingImages)
                    img.color = Utility.zeroAlpha;

                for (int i = 0; i < Cameras.Count; i++)
                    if (Cameras[i].m_Lens.FieldOfView > 5.0f)
                        Cameras[i].m_Lens.FieldOfView = NormalFOV[i] + CreativeCameraChangeValue;
            }
        }

        [Button]
        [ExecuteInEditMode]
        public void GetNormalFOV()
        {
            for (int i = 0; i < Cameras.Count; i++)
                NormalFOV[i] = Cameras[i].m_Lens.FieldOfView;
        }
    }

    public enum AppMode
    {
        Debug,
        Release,
        Creative
    }
}