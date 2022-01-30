using System;
using System.Windows;
using System.Windows.Input;
using Kalavarda.Jumps.Models.Interfaces;

namespace Kalavarda.Jumps.Impl
{
    public class InputController: IInputController, IDisposable
    {
        private readonly IInputElement _inputElement;
        private bool _upPressed;
        private bool _downPressed;
        private bool _leftPressed;
        private bool _rightPressed;
        private bool _spacePressed;

        public event Action<InputCommand> Activated;

        public event Action<InputCommand> Deactivated;

        public InputController(IInputElement inputElement)
        {
            _inputElement = inputElement ?? throw new ArgumentNullException(nameof(inputElement));

            _inputElement.KeyDown += InputElement_KeyDown;
            _inputElement.KeyUp += InputElement_KeyUp;
        }

        private void InputElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;

            switch (e.Key)
            {
                /*
                case Key.Up:
                    _upPressed = true;
                    _downPressed = false;
                    e.Handled = true;
                    break;
                case Key.Down:
                    _downPressed = true;
                    _upPressed = false;
                    e.Handled = true;
                    break;
                */
                case Key.Left:
                    _leftPressed = true;
                    if (_rightPressed)
                    {
                        _rightPressed = false;
                        Deactivated?.Invoke(InputCommand.Right);
                    }
                    e.Handled = true;
                    Activated?.Invoke(InputCommand.Left);
                    break;
                case Key.Right:
                    _rightPressed = true;
                    if (_leftPressed)
                    {
                        _leftPressed = false;
                        Deactivated?.Invoke(InputCommand.Left);
                    }
                    e.Handled = true;
                    Activated?.Invoke(InputCommand.Right);
                    break;
                case Key.Space:
                    if (!_spacePressed)
                    {
                        _spacePressed = true;
                        Activated?.Invoke(InputCommand.Jump);
                        Deactivated?.Invoke(InputCommand.Jump);
                    }
                    e.Handled = true;
                    break;
            }
        }

        private void InputElement_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;

            switch (e.Key)
            {
                case Key.Left:
                    if (_leftPressed)
                    {
                        _leftPressed = false;
                        Deactivated?.Invoke(InputCommand.Left);
                    }
                    e.Handled = true;
                    break;
                case Key.Right:
                    if (_rightPressed)
                    {
                        _rightPressed = false;
                        Deactivated?.Invoke(InputCommand.Right);
                    }
                    e.Handled = true;
                    break;
                case Key.Space:
                    if (_spacePressed)
                        _spacePressed = false;
                    e.Handled = true;
                    break;
            }
        }

        public void Dispose()
        {
            _inputElement.KeyDown -= InputElement_KeyDown;
            _inputElement.KeyUp -= InputElement_KeyUp;
        }
    }
}
