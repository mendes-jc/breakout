using UnityEngine;

using System.Collections;

namespace Breakout
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Paddle _paddle;
        [SerializeField] private Ball _ball;
        [SerializeField] private BrickManager _brickManager;
        [SerializeField] private BallController _ballController;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private AudioSource _soundtrackAudioSource;
        [SerializeField] private GameObject _tutorialText;

        [Header("Stats Settings")]
        [SerializeField] private int _initialBalls = 5;

        [Header("Ball Settings")]
        [SerializeField] private float _ballPositionOffsetFromPaddle = -20f;
        [SerializeField] private float _ballSpeed = 1000f;
        
        [Header("Brick Settings")]
        [SerializeField] private Vector2Int _bricksMatrixSize;
        [SerializeField] private BrickAppearingChanceConfig[] _brickAppearingChanceConfigs;

        [Header("Other Settings")]
        [SerializeField] private int _tutorialDisplayTimeInSeconds = 5;

        private void Awake()
        {
            _ballController.Initialize(_ballPositionOffsetFromPaddle, _paddle.transform, _ballSpeed);
        }

        private void Start()
        {
            _ball.OnBallTouchDeadZone.AddListener(PlayerStats.Instance.LoseBall);
            _ball.OnBallTouchDeadZone.AddListener(_ballController.ResetBall);
            _brickManager.OnPlayerDestroyAllBricks.AddListener(OnGameOver);

            PlayerStats.Instance.OnFinishRemainingBalls.AddListener(OnGameOver);

            StartGame();
        }

        private void OnGameOver()
        {
            _ballController.ResetBall();
            InputManager.Instance.DisablePlayerInput();

            _gameOverScreen.Display(PlayerStats.Instance.Score);
            _soundtrackAudioSource.Stop();
        }

        public void StartGame()
        {
            _brickManager.InitializeBricks(_bricksMatrixSize, _brickAppearingChanceConfigs);
            _ballController.ResetBall();
            PlayerStats.Instance.ResetScore();
            PlayerStats.Instance.ResetRemainingBalls(_initialBalls);
            _soundtrackAudioSource.Play();

            InputManager.Instance.EnablePlayerInput();

            StartCoroutine(DisplayTutorialTextTemporarily());
        }

        private IEnumerator DisplayTutorialTextTemporarily()
        {
            _tutorialText.SetActive(true);
            yield return new WaitForSeconds(_tutorialDisplayTimeInSeconds);

            _tutorialText.SetActive(false);
        }
    }
}