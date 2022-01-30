using System;
using Kalavarda.Jumps.Models;
using Kalavarda.Jumps.Models.Interfaces;

namespace Kalavarda.Jumps.Controllers
{
    public class HeroController: IDisposable
    {
        private const float CollisionSpeedRatio = 0.1f;

        private readonly Hero _hero;
        private readonly IInputController _inputController;
        private readonly ICollisionDetector _collisionDetector;

        public HeroController(Hero hero, IInputController inputController, ICollisionDetector collisionDetector)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _inputController = inputController ?? throw new ArgumentNullException(nameof(inputController));
            _collisionDetector = collisionDetector ?? throw new ArgumentNullException(nameof(collisionDetector));

            _inputController.Activated += InputController_Activated;
            _inputController.Deactivated += InputController_Deactivated;
        }

        private void InputController_Activated(InputCommand cmd)
        {
            switch (cmd)
            {
                case InputCommand.Left:
                    if (_collisionDetector.HasSupport(_hero))
                        _hero.Speed.Width = -_hero.Parameters.Speed.Width;
                    else
                        _hero.Speed.Width = -_hero.Parameters.Speed.Width * CollisionSpeedRatio;
                    break;
                case InputCommand.Right:
                    if (_collisionDetector.HasSupport(_hero))
                        _hero.Speed.Width = _hero.Parameters.Speed.Width;
                    else
                        _hero.Speed.Width = _hero.Parameters.Speed.Width * CollisionSpeedRatio;
                    break;
                case InputCommand.Jump:
                    if (_collisionDetector.HasSupport(_hero))
                        _hero.Speed.Height = -_hero.Parameters.JumpSpeed;
                    break;
            }
        }

        private void InputController_Deactivated(InputCommand cmd)
        {
            switch (cmd)
            {
                case InputCommand.Left:
                    if (_hero.Speed.Width < 0)
                        if (_collisionDetector.HasSupport(_hero))
                            _hero.Speed.Width = 0;
                    break;
                case InputCommand.Right:
                    if (_hero.Speed.Width > 0)
                        if (_collisionDetector.HasSupport(_hero))
                            _hero.Speed.Width = 0;
                    break;
            }
        }

        public void Dispose()
        {
            _inputController.Activated -= InputController_Activated;
            _inputController.Deactivated -= InputController_Deactivated;
        }
    }
}
