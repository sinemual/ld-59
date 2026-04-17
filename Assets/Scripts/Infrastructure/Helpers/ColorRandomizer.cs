using UnityEngine;

namespace Client
{
    public class ColorRandomizer : MonoBehaviour
    {
        public Color[] colors;

        void Start()
        {
            if (colors == null || colors.Length == 0)
                return;

            var rendererComponent = GetComponent<Renderer>();
            var block = new MaterialPropertyBlock();

            rendererComponent.GetPropertyBlock(block);

            Color randomColor = colors[Random.Range(0, colors.Length)];
            block.SetColor("_Color", randomColor);

            rendererComponent.SetPropertyBlock(block);
        }
    }
}