using UnityEngine;

public class DinoPlayer : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private float _gravity = 2500f;
    [SerializeField] private float _jumpForce = 1100f;
    [SerializeField] private float _groundY = 40f;
    [SerializeField] private float _startX = -300f;

    private float _velocityY;
    private bool _isGrounded = true;

    public RectTransform Rect => _rect;

    private void Update()
    {
        _velocityY -= _gravity * Time.deltaTime;
        float y = _rect.anchoredPosition.y + _velocityY * Time.deltaTime;

        if (y <= _groundY)
        {
            y = _groundY;
            _velocityY = 0f;
            _isGrounded = true;
        }

        _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x, y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (!_isGrounded)
            return;
        _velocityY = _jumpForce;
        _isGrounded = false;
    }

    public void ResetToStart()
    {
        _rect.anchoredPosition = new Vector2(_startX, _groundY);
        _velocityY = 0f;
        _isGrounded = true;
    }
}
