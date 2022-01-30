using System.ComponentModel;
using System.Windows;
using Kalavarda.Jumps.Controllers;
using Kalavarda.Jumps.Controls;
using Kalavarda.Jumps.Impl;
using Kalavarda.Jumps.Models;

namespace Kalavarda.Jumps
{
    public partial class MainWindow
    {
        private readonly Game _game;
        private readonly IUiElementFactory _uiElementFactory;
        private readonly InputController _inputController;
        private readonly HeroController _heroController;

        public MainWindow()
        {
            InitializeComponent();

            var app = (App)Application.Current;

            _uiElementFactory = app.UiElementFactory;
            _game = app.Game;

            _gameContainer.Child = new GameControl(_game, _uiElementFactory);

            _inputController = new InputController(this);
            _heroController = new HeroController(_game.Hero, _inputController, app.CollisionDetector);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _heroController.Dispose();
            _inputController.Dispose();

            base.OnClosing(e);
        }
    }
}
