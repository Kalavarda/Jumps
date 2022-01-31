using System;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;

namespace Kalavarda.Jumps.Models.GameObjects
{
    public class Brick : IHasBounds
    {
        public BoundsF Bounds { get; }

        public event Action<IHasBounds> PositionChanged;

        public Brick(RectBounds bounds)
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
