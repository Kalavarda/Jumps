using Kalavarda.Jumps.Models;

namespace Kalavarda.Jumps.Editor.Windows
{
    public partial class LocationPropertiesWindow
    {
        public LocationPropertiesWindow()
        {
            InitializeComponent();
        }

        public LocationPropertiesWindow(Location location): this()
        {
            _properties.Location = location;
        }
    }
}
