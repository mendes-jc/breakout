using UnityEngine;

namespace Breakout
{
    [CreateAssetMenu(fileName = "New Brick Type", menuName = "Brick Type")]
    public class BrickType : ScriptableObject
    {
        public Color Color;
        public int ScorePoints;
    }
}