using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;

namespace Kalavarda.Jumps.Models.Interfaces
{
    public interface ICollisionDetector
    {
        /// <summary>
        /// Имеет ли объект опору под ногами
        /// </summary>
        bool HasSupport(IHasBounds obj);

        /// <summary>
        /// Имеет ли объект опору под ногами
        /// </summary>
        bool HasSupport(BoundsF obj);

        /// <summary>
        /// Возвращает опору под ногами для указанного объекта
        /// </summary>
        IHasBounds GetSupport(IHasBounds obj);

        /// <summary>
        /// Возвращает опору под ногами для указанного объекта
        /// </summary>
        IHasBounds GetSupport(BoundsF obj);

        /// <summary>
        /// Пересекается ли объект с чем-нибудь
        /// </summary>
        bool HasCollision(IHasBounds obj);

        /// <summary>
        /// Пересекается ли объект с чем-нибудь
        /// </summary>
        bool HasCollision(BoundsF obj);
    }
}
