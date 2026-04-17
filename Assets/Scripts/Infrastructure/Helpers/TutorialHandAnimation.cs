using System;
using System.Collections;
using PrimeTween;
using UnityEngine;

namespace Client
{
    public class TutorialHandAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform objectT;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        private void OnEnable()
        {
            //objectT.gameObject.SetActive(false);
            StartCoroutine(DelayEnable());
        }

        private void OnDisable()
        {
            Tween.StopAll(objectT);
        }

        private IEnumerator DelayEnable()
        {
            yield return new WaitForSeconds(0.5f);
            objectT.gameObject.SetActive(true);
            objectT.transform.position = startPoint.position;
            Tween.Position(objectT, endPoint.position, 0.5f, cycleMode: CycleMode.Yoyo, cycles: -1);
        }
    }
}