using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class GoLinks : MonoBehaviour
    {
        public List<GameObject> gos;

        private void OnEnable()
        {
            foreach (var go in gos)
                go.SetActive(true);
        }

        private void OnDisable()
        {
            foreach (var go in gos)
                go.SetActive(false);
        }
    }
}
