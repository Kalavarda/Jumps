using System;
using System.Collections.Generic;
using System.Linq;
using Kalavarda.Primitives.Geometry;

namespace Kalavarda.Jumps.Models.GameObjects
{
    public class MovingBrick: Brick
    {
        private float _prevX;
        private float _prevY;

        public IReadOnlyList<MovePhase> MovePhases { get; }

        public SizeF Speed { get; } = new SizeF();

        public event Action<MovingBrick> SpeedChanged;

        public event Action<MovingBrick, float, float> PositionChanged;

        public MovingBrick(SizeF size, IReadOnlyList<MovePhase> movePhases)
            : base(new RectBounds(movePhases[^1].TargetPosition.DeepClone(), size))
        {
            MovePhases = movePhases.ToArray();
            Speed.Changed += Speed_Changed;
            Bounds.Position.Changed += Position_Changed;
        }

        private void Position_Changed(PointF pos)
        {
            var dx = pos.X - _prevX;
            var dy = pos.Y - _prevY;
            PositionChanged?.Invoke(this, dx, dy);
            _prevX = pos.X;
            _prevY = pos.Y;
        }

        private void Speed_Changed(SizeF speed)
        {
            SpeedChanged?.Invoke(this);
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
