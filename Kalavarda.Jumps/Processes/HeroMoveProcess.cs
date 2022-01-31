using System;
using Kalavarda.Jumps.Models;
using Kalavarda.Jumps.Models.GameObjects;
using Kalavarda.Jumps.Models.Interfaces;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Process;

namespace Kalavarda.Jumps.Processes
{
    public class HeroMoveProcess: IProcess
    {
        private readonly Game _game;
        private readonly ICollisionDetector _collisionDetector;
        private readonly BoundsF _test;

        public event Action<IProcess> Completed;

        public HeroMoveProcess(Game game, ICollisionDetector collisionDetector)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _collisionDetector = collisionDetector ?? throw new ArgumentNullException(nameof(collisionDetector));
            _test = _game.Hero.Bounds.DeepClone();

            _game.Hero.SupportChanged += Hero_SupportChanged;
        }

        private void Hero_SupportChanged(Hero hero, IHasBounds oldSupport, IHasBounds newSupport)
        {
            if (oldSupport is MovingBrick oldBrick)
                oldBrick.PositionChanged -= MovingSupport_PositionChanged;

            if (newSupport is MovingBrick newBrick)
                newBrick.PositionChanged += MovingSupport_PositionChanged;
        }

        private void MovingSupport_PositionChanged(MovingBrick movingSupport, float dx, float dy)
        {
            _game.Hero.Bounds.Position.Set(
                _game.Hero.Bounds.Position.X + dx,
                _game.Hero.Bounds.Position.Y + dy);
        }

        public void Process(TimeSpan delta)
        {
            var dt = (float)delta.TotalSeconds;
            var hero = _game.Hero;

            hero.Support = _collisionDetector.GetSupport(hero);

            var oldX = hero.Bounds.Position.X;
            var oldY = hero.Bounds.Position.Y;

            var x = oldX + hero.Speed.Width * dt;
            _test.Position.Set(x, oldY);
            if (_collisionDetector.HasCollision(_test))
            {
                hero.Speed.Width = 0;
                _test.Position.Set(oldX, oldY);
            }


            var y = oldY + hero.Speed.Height * dt;
            if (hero.Support == null)
                hero.Speed.Height += _game.Gravity * dt;
            _test.Position.Set(_test.Position.X, y);
            if (_collisionDetector.HasCollision(_test))
            {
                hero.Speed.Height = 0;
                _test.Position.Set(_test.Position.X, oldY);
            }

            hero.Bounds.Position.Set(_test.Position);
        }

        public void Stop()
        {
            Completed?.Invoke(this);
        }
    }
}
