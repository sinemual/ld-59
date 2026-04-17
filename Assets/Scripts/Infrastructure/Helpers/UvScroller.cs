using UnityEngine;

public class UvScroller : MonoBehaviour
{

    [SerializeField] private Material _targetMaterial;
    [SerializeField] private float _speedX;
    [SerializeField] private float _speedY;

    private Vector2 _offset;
    private Vector2 _initOffset;
    private bool _isWorking;

    private void Start()
    {
        _offset = _targetMaterial.mainTextureOffset;
        _initOffset = _targetMaterial.mainTextureOffset;
        SetState(true);
    }
    
    public void SetState(bool state)
    {
        _isWorking = state;
    }

    private void OnDisable() =>
        _targetMaterial.mainTextureOffset = _initOffset;

    private void Update()
    {
        if (!_isWorking)
            return;
        _offset.y += _speedY * Time.deltaTime;
        _targetMaterial.mainTextureOffset = _offset;
    }

    public void SetYSpeed(float speed)
    {
        _speedY = speed;
    }
}