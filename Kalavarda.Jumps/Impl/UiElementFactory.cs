using System;
using System.Windows;
using Kalavarda.Jumps.Controls;
using Kalavarda.Jumps.Models;
using Kalavarda.Jumps.Models.GameObjects;

namespace Kalavarda.Jumps.Impl
{
    public class UiElementFactory: IUiElementFactory
    {
        public UIElement Create(object model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            if (model is Location location)
                return new LocationControl(location, this);

            if (model is Brick brick)
                return new BrickControl(brick);

            if (model is Hero hero)
                return new HeroControl(hero);

            throw new NotImplementedException();
        }
    }
}
