using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Kalavarda.Jumps.Editor.Windows;
using Kalavarda.Jumps.Models;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;

namespace Kalavarda.Jumps.Editor
{
    public partial class MainWindow
    {
        private Location _location;
        private bool _locationChanged;

        public Location.Layer SelectedLayer => _cbLayer.SelectedItem as Location.Layer;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += (sender, e) => TuneControls();
        }

        private void TuneControls()
        {
            _cbLayer.IsEnabled = _location?.Layers != null && _location.Layers.Any();
            _miProperties.IsEnabled = _location != null;
        }

        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnNewClick(object sender, RoutedEventArgs e)
        {
            if (CloseLocation())
                NewLocation();
        }

        private void NewLocation()
        {
            _location = new Location(new SizeF(500, 500), new Location.Layer[0], new PointF());
            _location.LayerAdded += Location_LayerAdded;
            _locationControl.Location = _location;
            _location.Add(new Location.Layer(new IHasBounds[0]));
        }

        private void Location_LayerAdded(Location location, Location.Layer layer)
        {
            _cbLayer.ItemsSource = location.Layers;
            _cbLayer.SelectedItem = layer;
            _locationChanged = true;
        }

        private bool SaveLocation()
        {
            _locationChanged = false;
            return true;
        }

        private bool CloseLocation()
        {
            while (_location != null && _locationChanged)
                switch (MessageBox.Show("Данные изменены. Сохранить?", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning))
                {
                    case MessageBoxResult.Cancel:
                        return false;
                    case MessageBoxResult.Yes:
                        if (SaveLocation())
                        {
                            _location = null;
                            return true;
                        }
                        break;
                    case MessageBoxResult.No:
                        _location = null;
                        return true;
                }

            return true;
        }

        private void OnLayerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TuneControls();
        }

        private void OnPropertiesClick(object sender, RoutedEventArgs e)
        {
            var window = new LocationPropertiesWindow(_location) { Owner = this };
            window.ShowDialog();
        }
    }
}
