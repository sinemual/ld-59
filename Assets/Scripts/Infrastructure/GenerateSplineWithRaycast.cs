using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace Client
{
    public class GenerateSplineWithRaycast : MonoBehaviour
    {
        public SplineContainer splineContainer;
        public Collider roadCollider;
        public int pointsCount = 20; // Количество точек на сплайне
        public float roadWidth = 5f; // Примерная ширина дороги
        public float rayHeight = 10f; // Высота, с которой выпускаем лучи вниз

        private void Start()
        {
            //if (splineContainer == null || roadCollider == null) return;

            Spline spline = splineContainer.Spline;
            spline.Clear();

            Bounds bounds = roadCollider.bounds;
            float startZ = bounds.min.z;
            float endZ = bounds.max.z;
            float step = (endZ - startZ) / (pointsCount - 1);

            for (int i = 0; i < pointsCount; i++)
            {
                Vector3 originLeft = new Vector3(bounds.center.x - roadWidth / 2, bounds.max.y + rayHeight, startZ + i * step);
                Vector3 originRight = new Vector3(bounds.center.x + roadWidth / 2, bounds.max.y + rayHeight, startZ + i * step);

                bool hitLeft = Physics.Raycast(originLeft, Vector3.down, out RaycastHit hitLeftInfo);
                bool hitRight = Physics.Raycast(originRight, Vector3.down, out RaycastHit hitRightInfo);

                if (hitLeft && hitRight)
                {
                    // Находим центр между двумя точками
                    Vector3 center = (hitLeftInfo.point + hitRightInfo.point) / 2;

                    // Определяем направление движения (тангенс) для сплайна
                    Vector3 tangent = hitRightInfo.point - hitLeftInfo.point;
                    Vector3 up = Vector3.Cross(tangent.normalized, Vector3.forward);

                    // Добавляем точку в сплайн
                    spline.Add(new BezierKnot(center, tangent.normalized * 0.5f, -tangent.normalized * 0.5f, Quaternion.LookRotation(tangent, up)));
                }
                
                Debug.Log($"Make Wow");
            }

            Debug.Log($"Make");
        }
    }
}