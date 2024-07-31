using UnityEngine;

namespace Breakout
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private float _paddleSpeed;
        [SerializeField] private RectTransform _canvas;

        private float _screenWidth;
        private float _paddleWidth;

        private void Start()
        {
            _screenWidth = _canvas.rect.width;
            _paddleWidth = GetComponent<RectTransform>().rect.width;
        }

        private void Update()
        {
            float horizontalInput = InputManager.Instance.HorizontalInput;

            float newPositionX = transform.localPosition.x + horizontalInput * Time.deltaTime * _paddleSpeed;
            // Limit the position of the paddle so it never leaves the screen
            float clampedPositionX = Mathf.Clamp(newPositionX, -_screenWidth / 2 + _paddleWidth/2, _screenWidth / 2 - _paddleWidth/2);

            transform.localPosition = new Vector2(clampedPositionX, transform.localPosition.y);
        }
    }
}