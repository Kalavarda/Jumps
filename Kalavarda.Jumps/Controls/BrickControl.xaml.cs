using Kalavarda.Jumps.Impl;
using Kalavarda.Jumps.Models.GameObjects;

namespace Kalavarda.Jumps.Controls
{
    public partial class BrickControl 
    {
        public BrickControl()
        {
            InitializeComponent();
        }

        public BrickControl(Brick brick): this()
        {
            Width = brick.Bounds.Width;
            Height = brick.Bounds.Height;
        }
    }
}
