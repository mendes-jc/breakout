using UnityEngine;
using UnityEngine.UI;

namespace Breakout
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private Text _finalScoreText;

        public void Display(int finalScore)
        {
            _finalScoreText.text = $"FINAL SCORE: {finalScore}";
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}