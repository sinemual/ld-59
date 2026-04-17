using System.Collections;
using Cinemachine;
using Client;
using Client.Data;
using UnityEngine;
using CameraType = Client.Data.CameraType;

public class CameraService
{
    private CameraSceneData _cameraSceneData;
    private ICoroutineRunner _coroutineRunner;

    private CameraType _currentCameraType;
    private Transform _currentFollowTarget;
    private Transform _currentLookAtTarget;
    private Transform _defaultTarget;

    public CameraService(CameraSceneData cameraSceneData, ICoroutineRunner coroutineRunner)
    {
        _cameraSceneData = cameraSceneData;
        _coroutineRunner = coroutineRunner;
    }

    public void SetCamera(CameraType cameraType, Transform followT = null, Transform lookAtT = null, bool isWarp = false)
    {
        //Debug.Log($"cameraType {cameraType}");
        _currentCameraType = cameraType;
        _currentFollowTarget = followT;
        _currentLookAtTarget = lookAtT;

        _cameraSceneData.Cameras[cameraType].Follow = _currentFollowTarget;
        _cameraSceneData.Cameras[cameraType].LookAt = _currentLookAtTarget;
        if (isWarp && _currentFollowTarget)
            _cameraSceneData.Cameras[cameraType].OnTargetObjectWarped(_currentFollowTarget,
                _currentFollowTarget.position - _cameraSceneData.Cameras[cameraType].transform.position);

        foreach (var camera in _cameraSceneData.Cameras)
            camera.Value.gameObject.SetActive(camera.Key == cameraType);
    }

    public void SetDefaultTarget(Transform target)
    {
        _defaultTarget = target;
    }

    public void SetCameraAtTime(CameraType cameraType, Transform followT, Transform lookAtT, float time)
    {
        _coroutineRunner.StartCoroutine(SetCameraAtTimeCoroutine(cameraType, followT, lookAtT, time));
    }
    
    public void ShowPointWithTime(Transform point, float time)
    {
        SetCamera(_currentCameraType, point, point);
        _coroutineRunner.StartCoroutine(BackToDefaultTargetCoroutine(time));
    }

    public void Shake() => _cameraSceneData.ShakeSource.GenerateImpulse(_cameraSceneData.MainCamera.transform.forward);

    public Camera GetCamera() => _cameraSceneData.MainCamera;
    public CameraType GetCurrentCameraType() => _currentCameraType;

    public CinemachineVirtualCameraBase GetCurrentVC() => _cameraSceneData.Cameras[_currentCameraType];

    private IEnumerator SetCameraAtTimeCoroutine(CameraType cameraType, Transform followT, Transform lookAtT, float time)
    {
        yield return new WaitForSeconds(time);
        SetCamera(cameraType, followT, lookAtT);
    }
    
    private IEnumerator BackToDefaultTargetCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        SetCamera(_currentCameraType, _defaultTarget, _defaultTarget);
    }
}