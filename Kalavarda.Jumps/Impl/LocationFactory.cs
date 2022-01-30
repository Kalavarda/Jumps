using System;
using System.Collections.Generic;
using Kalavarda.Jumps.Models;
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
                size.Height - _game.BlockSize - _game.Hero.Bounds.Height / 2);
            return new Location(
                size,
                new [] { GenerateBricks(size) },
                heroStartPosition);
        }

        private Location.Layer GenerateBricks(SizeF size)
        {
            var bricks = new List<Brick>();

            var wCount = (int)(size.Width / _game.BlockSize);
            var hCount = (int)(size.Height / _game.BlockSize);

            for (var i = 0; i < wCount; i++)
            {
                bricks.Add(CreateBrick(i, 0));
                bricks.Add(CreateBrick(i, hCount - 1));
            }

            for (var j = 1; j < hCount - 1; j++)
            {
                bricks.Add(CreateBrick(0, j));
                bricks.Add(CreateBrick(wCount - 1, j));
            }


            bricks.Add(CreateBrick(3, hCount - 3));
            bricks.Add(CreateBrick(4, hCount - 3));

            bricks.Add(CreateBrick(10, hCount - 2));
            bricks.Add(CreateBrick(11, hCount - 2));
            bricks.Add(CreateBrick(11, hCount - 3));

            bricks.Add(CreateBrick(12, hCount - 5));
            bricks.Add(CreateBrick(13, hCount - 5));
            bricks.Add(CreateBrick(14, hCount - 5));
            bricks.Add(CreateBrick(15, hCount - 5));

            bricks.Add(CreateBrick(14, hCount - 8));
            bricks.Add(CreateBrick(15, hCount - 8));

            return new Location.Layer(bricks);
        }

        private Brick CreateBrick(int i, int j)
        {
            var x = i * _game.BlockSize + _game.BlockSize / 2;
            var y = j * _game.BlockSize + _game.BlockSize / 2;
            return new Brick(new RectBounds(
                new PointF(x, y),
                new SizeF {Width = _game.BlockSize, Height = _game.BlockSize }));
        }
    }
}
