using System;
using Kalavarda.Jumps.Models;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Process;

namespace Kalavarda.Jumps.Processes
{
    public class HeroMoveProcess: IProcess
    {
        private readonly Game _game;
        private readonly BoundsF _test;

        public event Action<IProcess> Completed;

        public HeroMoveProcess(Game game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _test = _game.Hero.Bounds.DeepClone();
        }

        public void Process(TimeSpan delta)
        {
            var dt = (float)delta.TotalSeconds;
            var hero = _game.Hero;

            hero.Speed.Height += _game.Gravity * dt;

            var oldX = hero.Bounds.Position.X;
            var oldY = hero.Bounds.Position.Y;

            var x = oldX + hero.Speed.Width * dt;
            _test.Position.Set(x, oldY);
            var collision = false;
            foreach (var layer in _game.Location.Layers)
            foreach (var obj in layer.Objects)
            {
                if (obj.Bounds.DoesIntersect(_test))
                {
                    hero.Speed.Width = 0;
                    _test.Position.Set(oldX, oldY);
                    collision = true;
                    break;
                }

                if (collision)
                    break;
            }


            var y = oldY + hero.Speed.Height * dt;
            _test.Position.Set(_test.Position.X, y);

            collision = false;
            foreach (var layer in _game.Location.Layers)
            foreach (var obj in layer.Objects)
            {
                if (obj.Bounds.DoesIntersect(_test))
                {
                    hero.Speed.Height = 0;
                    _test.Position.Set(_test.Position.X, oldY);
                    collision = true;
                    break;
                }

                if (collision)
                    break;
            }

            hero.Bounds.Position.Set(_test.Position);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
