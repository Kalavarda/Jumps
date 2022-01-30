using System;

namespace Kalavarda.Jumps.Models.Interfaces
{
    public interface IInputController
    {
        event Action<InputCommand> Activated;

        event Action<InputCommand> Deactivated;
    }

    public enum InputCommand
    {
        Left,
        Right,
        Jump
    }
}
