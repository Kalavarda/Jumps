using System;
using Kalavarda.Jumps.Models;
using Kalavarda.Jumps.Models.Interfaces;
using Kalavarda.Primitives.Abstract;

namespace Kalavarda.Jumps.Controllers
{
    public class HeroController: IDisposable
    {
        private const float CollisionSpeedRatio = 0.1f;

        private readonly Hero _hero;
        private readonly IInputController _inputController;
        private readonly ICollisionDetector _collisionDetector;
        private bool _stopAtSupport;

        public HeroController(Hero hero, IInputController inputController, ICollisionDetector collisionDetector)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _inputController = inputController ?? throw new ArgumentNullException(nameof(inputController));
            _collisionDetector = collisionDetector ?? throw new ArgumentNullException(nameof(collisionDetector));

            _inputController.Activated += InputController_Activated;
            _inputController.Deactivated += InputController_Deactivated;

            _hero.SupportChanged += Hero_SupportChanged;
        }

        private void Hero_SupportChanged(Hero hero, IHasBounds oldSupport, IHasBounds newSupport)
        {
            if (newSupport != null && _stopAtSupport)
            {
                _hero.Speed.Width = 0;
                _stopAtSupport = false;
            }
        }

        private void InputController_Activated(InputCommand cmd)
        {
            switch (cmd)
            {
                case InputCommand.Left:
                    if (_hero.Support != null)
                        _hero.Speed.Width = -_hero.Parameters.Speed.Width;
                    else
                    {
                        if (_hero.Speed.Width <= 0)
                            _hero.Speed.Width = MathF.Min(_hero.Speed.Width, -_hero.Parameters.Speed.Width * CollisionSpeedRatio); // костыль
                    }

                    break;
                case InputCommand.Right:
                    if (_hero.Support != null)
                        _hero.Speed.Width = _hero.Parameters.Speed.Width;
                    else
                    {
                        if (_hero.Speed.Width >= 0)
                            _hero.Speed.Width = MathF.Max(_hero.Speed.Width, _hero.Parameters.Speed.Width * CollisionSpeedRatio); // костыль
                    }

                    break;
                case InputCommand.Jump:
                    if (_hero.Support != null)
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
                    {
                        if (_hero.Support != null)
                            _hero.Speed.Width = 0;
                        else
                            _stopAtSupport = true;
                    }

                    break;
                case InputCommand.Right:
                    if (_hero.Speed.Width > 0)
                    {
                        if (_hero.Support != null)
                            _hero.Speed.Width = 0;
                        else
                            _stopAtSupport = true;
                    }

                    break;
            }
        }

        public void Dispose()
        {
            _hero.SupportChanged -= Hero_SupportChanged;
            _inputController.Activated -= InputController_Activated;
            _inputController.Deactivated -= InputController_Deactivated;
        }
    }
}
