using System;
using System.Windows;
using Kalavarda.Jumps.Models;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.WPF.Controllers;

namespace Kalavarda.Jumps.Controls
{
    public partial class LocationControl
    {
        private readonly IUiElementFactory _uiElementFactory;
        private readonly SinglePositionsController _positionsController = new SinglePositionsController();

        public Location Location { get; }

        public LocationControl()
        {
            InitializeComponent();
        }

        public LocationControl(Location location, IUiElementFactory uiElementFactory): this()
        {
            _uiElementFactory = uiElementFactory ?? throw new ArgumentNullException(nameof(uiElementFactory));
            Location = location ?? throw new ArgumentNullException(nameof(location));

            Width = location.Size.Width;
            Height = location.Size.Height;

            foreach (var layer in Location.Layers)
            {
                layer.Added += ObjectAdded;
                foreach (var obj in layer.Objects)
                    ObjectAdded(layer, obj);
            }

            Unloaded += LocationControl_Unloaded;
        }

        private void LocationControl_Unloaded(object sender, RoutedEventArgs e)
        {
            foreach (var layer in Location.Layers)
                layer.Added -= ObjectAdded;
        }

        private void ObjectAdded(Location.Layer layer, IHasBounds obj)
        {
            var objControl = _uiElementFactory.Create(obj);
            _canvas.Children.Add(objControl);
            _positionsController.Add(obj, objControl);
        }
    }
}
