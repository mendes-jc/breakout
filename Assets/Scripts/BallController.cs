using UnityEngine;

namespace Breakout
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private Ball _ball;

        private Transform _paddle;
        private bool _ballLaunched;
        private float _ballPositionOffsetFromPaddle;
        private float _ballSpeed;

        private void Update()
        {
            if (!_ballLaunched)
            {
                _ball.transform.position = new Vector2(_paddle.position.x, _paddle.position.y + _ballPositionOffsetFromPaddle);
                if (InputManager.Instance.LaunchBallInput)
                {
                    Vector2 randomBallDirection = GenerateRandomDirection();
                    _ball.Launch(randomBallDirection, _ballSpeed);
                    _ballLaunched = true;
                }
            }
        }

        private Vector2 GenerateRandomDirection()
        {
            float minAngle = -60f;
            float maxAngle = 60f;

            float randomAngle = Random.Range(minAngle, maxAngle);

            float randomAngleRad = randomAngle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Sin(randomAngleRad), Mathf.Cos(randomAngleRad));

            return direction;
        }

        public void ResetBall()
        {
            _ball.Stop();
            _ballLaunched = false;
        }

        public void Initialize(float ballPositionOffsetFromPaddle, Transform paddle, float ballSpeed)
        {
            _ballPositionOffsetFromPaddle = ballPositionOffsetFromPaddle;
            _paddle = paddle;
            _ballSpeed = ballSpeed;
        }
    }
}