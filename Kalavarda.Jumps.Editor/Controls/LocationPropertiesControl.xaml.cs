using System;
using System.Windows;
using Kalavarda.Jumps.Models;

namespace Kalavarda.Jumps.Editor.Controls
{
    public partial class LocationPropertiesControl
    {
        private Location _location;

        public LocationPropertiesControl()
        {
            InitializeComponent();
        }

        public Location Location
        {
            get => _location;
            set
            {
                if (_location == value)
                    return;

                _location = value;

                if (_location != null)
                {
                    _tbWidth.Text = MathF.Round(_location.Size.Width).ToString();
                    _tbHeight.Text = MathF.Round(_location.Size.Height).ToString();
                }
                else
                {
                    _tbWidth.Text = null;
                    _tbHeight.Text = null;
                }
            }
        }

        private void _tbWidth_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(_tbWidth.Text, out var w))
                Location.Size.Width = w;
            else
                _tbWidth.Text = MathF.Round(Location.Size.Width).ToString();
        }

        private void _tbHeight_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(_tbHeight.Text, out var h))
                Location.Size.Height = h;
            else
                _tbHeight.Text = MathF.Round(Location.Size.Height).ToString();
        }
    }
}
