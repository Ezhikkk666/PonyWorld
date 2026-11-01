using UnityEngine;

public class DinoObstacle : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private float _despawnMargin = 200f;

    private float _speed;

    public RectTransform Rect => _rect;

    public void Setup(float speed)
    {
        _speed = speed;
    }

    private void Update()
    {
        Vector2 pos = _rect.anchoredPosition;
        pos.x -= _speed * Time.deltaTime;
        _rect.anchoredPosition = pos;

        var parentRect = (RectTransform)_rect.parent;
        float despawnX = -parentRect.rect.width * 0.5f - _despawnMargin;
        if (_rect.anchoredPosition.x < despawnX)
        {
            Destroy(gameObject);
        }
    }
}
