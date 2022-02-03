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
        private Location _location;

        public Location Location
        {
            get => _location;
            set
            {
                if (value == _location)
                    return;

                if (_location != null)
                {
                    _location.LayerAdded -= Location_LayerAdded;
                    foreach (var layer in _location.Layers)
                    {
                        layer.Added -= ObjectAdded;
                    }
                }

                _location = value;

                if (_location != null)
                {
                    _location.Size.Changed += Size_Changed;
                    Size_Changed(_location.Size);

                    _location.LayerAdded += Location_LayerAdded;
                    foreach (var layer in _location.Layers)
                        Location_LayerAdded(_location, layer);
                }
            }
        }

        private void Size_Changed(Primitives.Geometry.SizeF size)
        {
            Width = _location.Size.Width;
            Height = _location.Size.Height;
        }

        private void Location_LayerAdded(Location location, Location.Layer layer)
        {
            layer.Added += ObjectAdded;
            foreach (var obj in layer.Objects)
                ObjectAdded(layer, obj);
        }

        public LocationControl()
        {
            InitializeComponent();
        }

        public LocationControl(Location location, IUiElementFactory uiElementFactory) : this()
        {
            _uiElementFactory = uiElementFactory ?? throw new ArgumentNullException(nameof(uiElementFactory));
            Location = location ?? throw new ArgumentNullException(nameof(location));

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
