using UnityEngine;
using UnityEngine.Events;

using System.Linq;

namespace Breakout
{
    public class BrickManager : MonoBehaviour
    {
        [SerializeField] private Brick _brick;
        [SerializeField] private Transform _bricksContainer;

        private int _brickCount = 0;

        public UnityEvent OnPlayerDestroyAllBricks;

        private void Awake()
        {
            OnPlayerDestroyAllBricks = new UnityEvent();
        }

        private BrickType GetRandomBrickType(BrickAppearingChanceConfig[] brickAppearingChanceConfigs)
        {
            if (brickAppearingChanceConfigs.Length == 0) return null;

            float totalChance = brickAppearingChanceConfigs.Sum(config => config.ChanceOfAppearing);
            float randomValue = Random.Range(0f, totalChance);
            float cumulativeChance = 0f;

            foreach (BrickAppearingChanceConfig config in brickAppearingChanceConfigs)
            {
                cumulativeChance += config.ChanceOfAppearing;
                if (randomValue <= cumulativeChance)
                {
                    return config.BrickType;
                }
            }

            return brickAppearingChanceConfigs[0].BrickType;
        }

        private void RemoveAllBricks()
        {
            foreach (Transform brick in _bricksContainer)
            {
                Destroy(brick.gameObject);
            }
        }

        // That's a simple implementation. In a more complex scenario, I would make the position of the bricks configurable
        // (adding more options than just a rectangle) to create more interesting levels.
        public void InitializeBricks(Vector2 bricksMatrixSize, BrickAppearingChanceConfig[] brickAppearingChanceConfigs)
        {
            RemoveAllBricks();
            _brickCount = 0;

            Vector2 brickSize = _brick.GetComponent<RectTransform>().rect.size;
            // Calculate the initial position to spawn the bricks in both X and Y axis, so the bricks are centralized in the Bricks Container
            float startSpawnPositionX = -((brickSize.x * bricksMatrixSize.x) / 2) + (brickSize.x / 2);
            float startSpawnPositionY = -((brickSize.y * bricksMatrixSize.y) / 2) + (brickSize.y / 2);

            for (float column = 0; column < bricksMatrixSize.x; column++)
            {
                for (float row = 0; row < bricksMatrixSize.y; row++)
                {
                    Brick newBrick = Instantiate(_brick, _bricksContainer);
                    BrickType brickType = GetRandomBrickType(brickAppearingChanceConfigs);

                    newBrick.OnBrickDestroyedWithScore.AddListener(OnBrickDestroyedWithScore);
                    newBrick.transform.localPosition = new Vector2(startSpawnPositionX + (column * brickSize.x), startSpawnPositionY + (row * brickSize.y));
                    newBrick.Setup(brickType);

                    _brickCount++;
                }
            }
        }

        public void OnBrickDestroyedWithScore(int brickScore)
        {
            _brickCount--;
            PlayerStats.Instance.AddScore(brickScore);

            if (_brickCount <= 0)
            {
                OnPlayerDestroyAllBricks.Invoke();
            }
        }
    }
}