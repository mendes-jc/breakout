using System;

namespace Breakout {
    [Serializable]
    public struct BrickAppearingChanceConfig
    {
        public int ChanceOfAppearing;
        public BrickType BrickType;
    }
}