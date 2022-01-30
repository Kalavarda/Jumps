using System.Threading;
using Kalavarda.Jumps.Impl;
using Kalavarda.Jumps.Models;
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
            Processor.Add(new HeroMoveProcess(Game));
        }
    }
}
