using System;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;

namespace Kalavarda.Jumps.Impl
{
    public class Brick: IHasBounds
    {
        public BoundsF Bounds { get; }

        public event Action<IHasBounds> PositionChanged;

        public Brick(BoundsF bounds)
        {
            Bounds = bounds ?? throw new ArgumentNullException(nameof(bounds));
            Bounds.Position.Changed += Position_Changed;
        }

        private void Position_Changed(PointF pos)
        {
            PositionChanged?.Invoke(this);
        }
    }
}
