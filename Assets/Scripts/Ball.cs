using UnityEngine;
using UnityEngine.Events;

namespace Breakout
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D Rigidbody2D;

        public UnityEvent OnBallTouchDeadZone;

        public void Launch(Vector2 direction, float ballSpeed)
        {
            Rigidbody2D.AddForce(direction * ballSpeed, ForceMode2D.Impulse);
        }

        public void Stop()
        {
            Rigidbody2D.velocity = Vector2.zero;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("DeadZone"))
            {
                OnBallTouchDeadZone.Invoke();
            }
        }
    }
}