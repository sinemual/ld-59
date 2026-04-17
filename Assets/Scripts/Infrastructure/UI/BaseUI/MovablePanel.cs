using System;
using PrimeTween;
using UnityEngine;

[Serializable]
public class MovablePanel
{
    public Transform Panel;
    public Transform StartPoint;
    public Transform EndPoint;

    public Action OnMoveToStartComplete;
    public Action OnMoveToEndComplete;

    private float _time = 0.5f;
    //private Tween _moveTween;

    public void SetTime(float time) => _time = time;

    public void MoveToStart()
    {
        StopTween();
        /*_moveTween = Tween.Position(Panel, StartPoint.position, _time)
            .OnComplete(() => OnMoveToStartComplete?.Invoke());*/
    }

    public void InstantMoveToStart()
    {
        StopTween();
        Panel.position = StartPoint.position;
    }

    public void MoveToEnd()
    {
        StopTween();
        /*_moveTween = Tween.Position(Panel, EndPoint.position, _time)
            .OnComplete(() => OnMoveToEndComplete?.Invoke());*/
    }

    private void StopTween()
    {
        //_moveTween.Stop();
    }
}