using System;
using System.Collections.Generic;
using System.Linq;
using Kalavarda.Primitives.Geometry;

namespace Kalavarda.Jumps.Models.GameObjects
{
    public class MovingBrick: Brick
    {
        public IReadOnlyList<MovePhase> MovePhases { get; }

        public SizeF Speed { get; } = new SizeF();

        public MovingBrick(SizeF size, IReadOnlyList<MovePhase> movePhases)
            : base(new RectBounds(movePhases[^1].TargetPosition.DeepClone(), size))
        {
            MovePhases = movePhases.ToArray();
        }

        public class MovePhase
        {
            public PointF TargetPosition { get; }

            public TimeSpan DelayBefore { get; }

            public float MoveSpeed { get; }

            public MovePhase(PointF targetPosition, float moveSpeed, TimeSpan delayBefore)
            {
                TargetPosition = targetPosition ?? throw new ArgumentNullException(nameof(targetPosition));
                MoveSpeed = moveSpeed;
                DelayBefore = delayBefore;
            }
        }
    }
}
