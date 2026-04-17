using UnityEngine;

namespace Extensions
{
    public static class IntExtensions
    {
        public static float Remap(this int value, float from1, float to1, float from2, float to2) =>
            (value - from1) / (to1 - from1) * (to2 - from2) + from2;

        public static LayerMask ToLayerMask(this int value) =>
            1 << value;
        
        public static int ToLayerMaskValue(this int value) =>
            1 << value;
    }
}