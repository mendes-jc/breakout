using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Breakout
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private Animator Animator;

        private int _score;

        public UnityEvent<int> OnBrickDestroyedWithScore;

        private void Awake()
        {
            OnBrickDestroyedWithScore = new UnityEvent<int>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                OnBrickDestroyedWithScore.Invoke(_score);
                Destroy(gameObject);
            }
        }

        public void Setup(BrickType brickType)
        {
            GetComponent<Image>().color = brickType.Color;
            _score = brickType.ScorePoints;
        }
    }
}