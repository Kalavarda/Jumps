using System;
using System.Collections.Generic;
using Kalavarda.Jumps.Models;
using Kalavarda.Jumps.Models.GameObjects;
using Kalavarda.Jumps.Models.Interfaces;
using Kalavarda.Primitives.Geometry;

namespace Kalavarda.Jumps.Impl
{
    public class LocationFactory: ILocationFactory
    {
        private readonly Game _game;

        public LocationFactory(Game game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public Location Create()
        {
            var size = new SizeF { Width = 20 * _game.BlockSize, Height = 40 * _game.BlockSize };
            var heroStartPosition = new PointF(
                _game.BlockSize * 2.5f,
                size.Height - _game.BlockSize - 1.5f *_game.Hero.Bounds.Height);
            return new Location(
                size,
                new [] { GenerateBricks(size) },
                heroStartPosition);
        }

        private Location.Layer GenerateBricks(SizeF size)
        {
            var bricks = new List<Brick>
            {
                new Brick(new RectBounds(
                    new PointF(size.Width / 2, _game.BlockSize / 2),
                    new SizeF {Width = size.Width, Height = _game.BlockSize})),
                new Brick(new RectBounds(
                    new PointF(size.Width / 2, size.Height - _game.BlockSize / 2),
                    new SizeF {Width = size.Width, Height = _game.BlockSize})),
                new Brick(new RectBounds(
                    new PointF(_game.BlockSize / 2, size.Height / 2),
                    new SizeF {Width = _game.BlockSize, Height = size.Height - 2 * _game.BlockSize})),
                new Brick(new RectBounds(
                    new PointF(size.Width - _game.BlockSize / 2, size.Height / 2),
                    new SizeF {Width = _game.BlockSize, Height = size.Height - 2 * _game.BlockSize}))
            };

            var wCount = (int)(size.Width / _game.BlockSize);
            var hCount = (int)(size.Height / _game.BlockSize);

            bricks.Add(CreateBrick(4, hCount - 2));
            bricks.Add(CreateBrick(5, hCount - 2));
            bricks.Add(CreateBrick(5, hCount - 3));
            
            bricks.Add(new MovingBrick(
                new SizeF(3 * _game.BlockSize, _game.BlockSize),
                new []
                {
                    new MovingBrick.MovePhase(new PointF(8.5f * _game.BlockSize, size.Height - 2.5f * _game.BlockSize), 50, TimeSpan.Zero),
                    new MovingBrick.MovePhase(new PointF(10.5f * _game.BlockSize, size.Height - 2.5f * _game.BlockSize), 100, TimeSpan.Zero)
                }
            ));
            
            bricks.Add(CreateBrick(13, hCount - 2));
            bricks.Add(CreateBrick(13, hCount - 3));
            
            bricks.Add(new MovingBrick(
                new SizeF(3 * _game.BlockSize, _game.BlockSize),
                new[]
                {
                    new MovingBrick.MovePhase(new PointF(16.5f * _game.BlockSize, size.Height - 2.5f * _game.BlockSize), 75, TimeSpan.FromSeconds(1)),
                    new MovingBrick.MovePhase(new PointF(16.5f * _game.BlockSize, size.Height - 5.5f * _game.BlockSize), 125, TimeSpan.FromSeconds(2))
                }
            ));
            
            return new Location.Layer(bricks);
        }

        private Brick CreateBrick(int i, int j, int w = 1, int h = 1)
        {
            var x = i * _game.BlockSize + _game.BlockSize / 2;
            var y = j * _game.BlockSize + _game.BlockSize / 2;
            return new Brick(new RectBounds(
                new PointF(x, y),
                new SizeF { Width = w * _game.BlockSize, Height = h * _game.BlockSize }));
        }
    }
}
