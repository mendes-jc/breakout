using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Breakout
{
    public class PlayerStats : UnitySingleton<PlayerStats>
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _remainingBallsText;

        private int _score = 0;
        private string FormattedScoreText => $"Score: {_score}";

        private int _remainingBalls = 0;
        private string FormattedRemainingBallsText => $"Balls: {_remainingBalls}";

        public int Score => _score;

        public UnityEvent OnFinishRemainingBalls;

        override protected void Awake()
        {
            base.Awake();
            OnFinishRemainingBalls = new UnityEvent();
        }

        public void ResetScore()
        {
            _score = 0;
            _scoreText.text = FormattedScoreText;
        }

        public void ResetRemainingBalls(int initialRemainingBalls)
        {
            _remainingBalls = initialRemainingBalls;
            _remainingBallsText.text = FormattedRemainingBallsText;
        }

        public void AddScore(int score)
        {
            _score += score;
            _scoreText.text = FormattedScoreText;
        }

        public void LoseBall()
        {
            _remainingBalls -= 1;
            _remainingBallsText.text = FormattedRemainingBallsText;
            if (_remainingBalls <= 0)
            {
                OnFinishRemainingBalls.Invoke();
            }
        }
    }
}