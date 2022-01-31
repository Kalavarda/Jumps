using System;
using Kalavarda.Jumps.Models;
using Kalavarda.Jumps.Models.Interfaces;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;

namespace Kalavarda.Jumps.Impl
{
    public class CollisionDetector: ICollisionDetector
    {
        private readonly Game _game;

        public CollisionDetector(Game game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        /// <inheritdoc/>
        public bool HasSupport(IHasBounds obj)
        {
            return GetSupport(obj) != null;
        }


        /// <inheritdoc/>
        public bool HasSupport(BoundsF obj)
        {
            return GetSupport(obj) != null;
        }

        /// <inheritdoc/>
        public IHasBounds GetSupport(IHasBounds obj)
        {
            return GetSupport(obj.Bounds);
        }

        /// <inheritdoc/>
        public IHasBounds GetSupport(BoundsF obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var test = obj.DeepClone();
            var dy = obj.Height / 10;
            test.Position.Set(test.Position.X, test.Position.Y + test.Size.Height / 2 + dy / 2);
            test.Size.Height = dy;

            foreach (var locationLayer in _game.Location.Layers)
                foreach (var o in locationLayer.Objects)
                    if (o.Bounds.DoesIntersect(test))
                        return o;
            return null;
        }

        /// <inheritdoc/>
        public bool HasCollision(IHasBounds obj)
        {
            return HasCollision(obj.Bounds);
        }

        /// <inheritdoc/>
        public bool HasCollision(BoundsF obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            foreach (var layer in _game.Location.Layers)
                foreach (var o in layer.Objects)
                    if (o.Bounds.DoesIntersect(obj))
                        return true;

            return false;
        }
    }
}
