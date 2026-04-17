using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace Client.Data.Equip
{
    [System.Serializable]
    public class BalanceCurve
    {
        [Title("BalanceCurve")]

        [SerializeField]
        private GrowthType growthType = GrowthType.Linear;

        [SerializeField]
        private float maxValue = 100;

        [SerializeField]
        private float minValue = 0;  // Минимальное значение

        [SerializeField]
        private bool isRounding = true;
        
        [SerializeField]
        private int roundingMultiple = 1;

        [SerializeField]
        private int maxLevel = 20;

        private AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

        public List<float> customValues = new List<float>();

        [SerializeField]
        private int testLevel = 0;

        [ShowInInspector, ReadOnly]
        private float currentValue;

        public float MaxValue => maxValue;

        public float MinValue => minValue;

        public int MaxLevel => maxLevel;

        [Button("Update Current Value")]
        public void UpdateCurrentValue() => currentValue = GetValueByLevel(testLevel);

        [Button("Copy Curve to Custom Values")]
        public void CopyCurveToCustomValues()
        {
            customValues.Clear();

            for (int i = 0; i <= maxLevel; i++)
            {
                float value = GetValueByLevel(i);
                customValues.Add(isRounding ? Mathf.Round(value) : value); 
            }
        }

        public void SetRoundingMultiple(int multiple)
        {
            roundingMultiple = Mathf.Max(1, multiple);
        }

        public float GetValueByLevel(int level)
        {
            float rawValue = CalculateValue(level);
            return isRounding ? RoundToMultiple(rawValue, roundingMultiple) : rawValue;
        }

        public float[] GetValuesForLevels()
        {
            float[] values = new float[MaxLevel];
            for (int i = 0; i < MaxLevel; i++)
            {
                values[i] = GetValueByLevel(i);
            }
            return values;
        }

        private float CalculateValue(int level)
        {
            float normalizedLevel = (float)level / maxLevel; // Нормализуем уровень в диапазон [0, 1]
            float rawValue = 0;

            switch (growthType)
            {
                case GrowthType.Linear:
                    // Линейная интерполяция между MinValue и MaxValue
                    rawValue = Mathf.Lerp(minValue, maxValue, normalizedLevel);
                    break;

                case GrowthType.Exponential:
                    // Экспоненциальный рост в диапазоне MinValue-MaxValue
                    float exponentialValue = Mathf.Pow(normalizedLevel, 2); // Пример экспоненциального роста
                    rawValue = Mathf.Lerp(minValue, maxValue, exponentialValue);
                    break;

                case GrowthType.Logarithmic:
                    // Логарифмический рост в диапазоне MinValue-MaxValue
                    float logarithmicValue = Mathf.Log10(1 + 9 * normalizedLevel); // Логарифмическая функция
                    rawValue = Mathf.Lerp(minValue, maxValue, logarithmicValue);
                    break;

                case GrowthType.Custom:
                    // Пользовательские значения
                    if (level < customValues.Count)
                        rawValue = customValues[level];
                    else if (customValues.Count > 0)
                        rawValue = customValues[customValues.Count - 1];
                    else
                        rawValue = minValue; // Если список пуст, используем минимальное значение
                    break;

                default:
                    // Используем анимационную кривую
                    rawValue = Mathf.Lerp(minValue, maxValue, curve.Evaluate(normalizedLevel));
                    break;
            }

            return rawValue; // Уже в диапазоне [MinValue, MaxValue]
        }

        private float RoundToMultiple(float value, int multiple)
        {
            
            return Mathf.Round(value / multiple) * multiple;
        }

        [Button("Set Curve")]
        public void SetCurve(AnimationCurve newCurve)
        {
            if (growthType != GrowthType.Custom)
                curve = newCurve;
        }
    }
    
    public enum GrowthType
    {
        Linear,
        Exponential,
        Logarithmic,
        Custom
    }
}