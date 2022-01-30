using System;
using Kalavarda.Jumps.Models;
using Kalavarda.Primitives.WPF.Controllers;

namespace Kalavarda.Jumps.Controls
{
    public partial class GameControl
    {
        private readonly Game _game;
        private readonly IUiElementFactory _uiElementFactory;
        private readonly SinglePositionsController _positionsController = new SinglePositionsController();

        public GameControl()
        {
            InitializeComponent();
        }

        public GameControl(Game game, IUiElementFactory uiElementFactory): this()
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _uiElementFactory = uiElementFactory ?? throw new ArgumentNullException(nameof(uiElementFactory));
            _game.LocationChanged += Game_LocationChanged;
            Unloaded += GameControl_Unloaded;
            Game_LocationChanged();

            var heroControl = _uiElementFactory.Create(_game.Hero);
            _canvas.Children.Add(heroControl);
            _positionsController.Add(_game.Hero, heroControl);
        }

        private void GameControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _game.LocationChanged -= Game_LocationChanged;
        }

        private void Game_LocationChanged()
        {
            if (_game.Location != null)
            {
                var locationControl = _uiElementFactory.Create(_game.Location);
                _locationContainer.Child = locationControl;
            }
            else
                _locationContainer.Child = null;
        }
    }
}
