using System.Linq;
using System.Threading;
using System.Windows;
using Kalavarda.Jumps.Controls;
using Kalavarda.Jumps.Impl;
using Kalavarda.Jumps.Models;
using Kalavarda.Jumps.Models.GameObjects;
using Kalavarda.Jumps.Models.Interfaces;
using Kalavarda.Jumps.Processes;
using Kalavarda.Primitives.Process;

namespace Kalavarda.Jumps
{
    public partial class App
    {
        public Game Game { get; } = new Game();

        public IProcessor Processor { get; }

        public ILocationFactory LocationFactory { get; }

        public IUiElementFactory UiElementFactory { get; } = new UiElementFactory();

        public ICollisionDetector CollisionDetector { get; }

        public App()
        {
            LocationFactory = new LocationFactory(Game);
            Game.Location = LocationFactory.Create();
            Game.Hero.Bounds.Position.Set(Game.Location.HeroStartPosition);

            CollisionDetector = new CollisionDetector(Game);

            Processor = new MultiProcessor(60, CancellationToken.None);
            Processor.Add(new HeroMoveProcess(Game, CollisionDetector));
            // TODO: вынести в подписчик Game.LocationChanged
            foreach (var layer in Game.Location.Layers)
            foreach (var movingBrick in layer.Objects.OfType<MovingBrick>())
                Processor.Add(new MovingBrickProcess(movingBrick, Game));
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Processor.Paused = true; // TODO: Stop() and Dispose()

            base.OnExit(e);
        }
    }
}
