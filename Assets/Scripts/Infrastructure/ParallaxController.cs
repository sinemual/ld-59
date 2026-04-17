using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public Transform[] layers;
    public float[] parallaxScales;
    public float backgroundWidth;

    private Transform _moveObject;
    private Vector3 _previousPlayerPosition;
    private float[] _parallaxPosY;
    private bool _isInited = false;
    
    public void Init(Transform moveObject)
    {
        _moveObject = moveObject;
        _previousPlayerPosition = _moveObject.position;
        _parallaxPosY = new float[layers.Length];
        for (int i = 0; i < layers.Length; i++)
            _parallaxPosY[i] = layers[i].position.y;
        _isInited = true;
    }

    private void Update()
    {
        if (_isInited)
        {
            float parallax = (_moveObject.position.x - _previousPlayerPosition.x);

            for (int i = 0; i < layers.Length; i++)
            {
                Vector3 layerPosition = layers[i].position;
                layerPosition.x += parallax * parallaxScales[i];
                layers[i].position = layerPosition;

                if (layers[i].position.x < -backgroundWidth)
                    layers[i].position = new Vector3(backgroundWidth, _parallaxPosY[i], 0);
            }

            _previousPlayerPosition = _moveObject.position;
        }
    }
}