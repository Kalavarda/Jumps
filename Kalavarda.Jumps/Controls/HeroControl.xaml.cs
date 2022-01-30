using System;
using Kalavarda.Jumps.Models;

namespace Kalavarda.Jumps.Controls
{
    public partial class HeroControl
    {
        private readonly Hero _hero;

        public HeroControl()
        {
            InitializeComponent();
        }

        public HeroControl(Hero hero): this()
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            Width = hero.Bounds.Width;
            Height = hero.Bounds.Height;
        }
    }
}
