using System;
using System.Collections.Generic;
using Kalavarda.Jumps.Models.GameObjects;
using Kalavarda.Primitives.Process;

namespace Kalavarda.Jumps.Processes
{
    public class MovingBrickProcess: IProcess
    {
        private readonly MovingBrick _movingBrick;
        private MovingBrick.MovePhase _currentPhase;
        private DateTime _phaseStartTime;

        public event Action<IProcess> Completed;

        public MovingBrickProcess(MovingBrick movingBrick)
        {
            _movingBrick = movingBrick ?? throw new ArgumentNullException(nameof(movingBrick));
            _currentPhase = _movingBrick.MovePhases[^1];
            _phaseStartTime = DateTime.Now;
        }

        public void Process(TimeSpan delta)
        {
            if (_phaseStartTime + _currentPhase.DelayBefore > DateTime.Now)
                return;

            var dt = (float)delta.TotalSeconds;

            var direction = _movingBrick.Bounds.Position.AngleTo(_currentPhase.TargetPosition);
            _movingBrick.Speed.Set(
                MathF.Cos(direction) * _currentPhase.MoveSpeed,
                MathF.Sin(direction) * _currentPhase.MoveSpeed);

            var dx = _movingBrick.Speed.Width * dt;
            var dy = _movingBrick.Speed.Height * dt;

            var distance = _movingBrick.Bounds.Position.DistanceTo(_currentPhase.TargetPosition);
            if (MathF.Sqrt(dx*dx + dy*dy) > distance)
            {
                GoToNextPhase();
                return;
            }

            _movingBrick.Bounds.Position.Set(
                _movingBrick.Bounds.Position.X + dx,
                _movingBrick.Bounds.Position.Y + dy);
        }

        public void Stop()
        {
            _movingBrick.Speed.Set(0, 0);
            Completed?.Invoke(this);
        }

        private void GoToNextPhase()
        {
            var i = IndexOf(_currentPhase, _movingBrick.MovePhases) + 1;
            if (i >= _movingBrick.MovePhases.Count)
                i = 0;
            _currentPhase = _movingBrick.MovePhases[i];
            _phaseStartTime = DateTime.Now;
        }

        private static int IndexOf<T>(T obj, IReadOnlyList<T> list)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (list == null) throw new ArgumentNullException(nameof(list));

            for (var i = 0; i < list.Count; i++)
                if (list[i].Equals(obj))
                    return i;

            return -1;
        }
    }
}
