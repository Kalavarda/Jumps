using System;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;
using PointF = Kalavarda.Primitives.Geometry.PointF;
using SizeF = Kalavarda.Primitives.Geometry.SizeF;

namespace Kalavarda.Jumps.Models
{
    public class Hero: IHasBounds
    {
        private IHasBounds _support;

        public BoundsF Bounds { get; }
        
        public SizeF Speed { get; } = new SizeF();

        /// <summary>
        /// Опора
        /// </summary>
        public IHasBounds Support
        {
            get => _support;
            set
            {
                if (_support == value)
                    return;

                var oldSupport = _support;
                _support = value;
                SupportChanged?.Invoke(this, oldSupport, _support);
            }
        }

        public event Action<Hero, IHasBounds, IHasBounds> SupportChanged;

        public HeroParameters Parameters = new HeroParameters();

        public Hero(BoundsF bounds)
        {
            Bounds = bounds ?? throw new ArgumentNullException(nameof(bounds));
            Bounds.Position.Changed += Position_Changed;
        }

        private void Position_Changed(PointF pos)
        {
            PositionChanged?.Invoke(this);
        }

        public event Action<IHasBounds> PositionChanged;
    }

    public class HeroParameters
    {
        public SizeF Speed { get; } = new SizeF { Width = 100, Height = 100 };

        public float JumpSpeed { get; } = 200;
    }
}
