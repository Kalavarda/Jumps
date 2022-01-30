using System;
using Kalavarda.Jumps.Models;
using Kalavarda.Jumps.Models.Interfaces;
using Kalavarda.Primitives.Abstract;

namespace Kalavarda.Jumps.Impl
{
    public class CollisionDetector: ICollisionDetector
    {
        private readonly Game _game;

        public CollisionDetector(Game game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public bool HasSupport(IHasBounds obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var test = _game.Hero.Bounds.DeepClone();
            var dy = _game.Hero.Bounds.Height / 10;
            test.Position.Set(test.Position.X, test.Position.Y + dy);
            foreach (var locationLayer in _game.Location.Layers)
            foreach (var o in locationLayer.Objects)
                if (o.Bounds.DoesIntersect(test))
                    return true;
            return false;
        }
    }
}
