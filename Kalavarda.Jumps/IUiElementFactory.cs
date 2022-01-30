using System.Windows;

namespace Kalavarda.Jumps
{
    public interface IUiElementFactory
    {
        UIElement Create(object model);
    }
}
