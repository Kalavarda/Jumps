using System;
using Kalavarda.Primitives.Geometry;

namespace Kalavarda.Jumps.Models
{
    public class Game
    {
        private Location _location;

        public float BlockSize { get; } = 50;

        public float Gravity { get; } = 250;

        public Hero Hero { get; }

        public event Action LocationChanged;

        public Location Location
        {
            get => _location;
            set
            {
                if (_location == value)
                    return;

                _location = value;

                LocationChanged?.Invoke();
            }
        }

        public Game()
        {
            var size = new SizeF
            {
                Width = BlockSize / 2,
                Height = BlockSize * 0.9f
            };
            Hero = new Hero(new RectBounds(new PointF(), size));
        }
    }
}
