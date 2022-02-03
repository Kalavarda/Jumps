using System.Windows;

namespace Kalavarda.Jumps.Controls
{
    public interface IUiElementFactory
    {
        UIElement Create(object model);
    }
}
