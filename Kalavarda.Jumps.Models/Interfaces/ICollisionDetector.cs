using Kalavarda.Primitives.Abstract;

namespace Kalavarda.Jumps.Models.Interfaces
{
    public interface ICollisionDetector
    {
        /// <summary>
        /// Имеет ли объект опору под ногами
        /// </summary>
        bool HasSupport(IHasBounds obj);
    }
}
