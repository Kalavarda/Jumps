using System;
using System.Collections.Generic;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;

namespace Kalavarda.Jumps.Models
{
    public class Location
    {
        private List<Layer> _layers = new List<Layer>();

        public SizeF Size { get; }

        public PointF HeroStartPosition { get; }

        public IReadOnlyCollection<Layer> Layers => _layers;

        public event Action<Location, Layer> LayerAdded;

        public Location(SizeF size, IReadOnlyCollection<Layer> layers, PointF heroStartPosition)
        {
            Size = size ?? throw new ArgumentNullException(nameof(size));
            foreach (var layer in layers)
                Add(layer);
            HeroStartPosition = heroStartPosition ?? throw new ArgumentNullException(nameof(heroStartPosition));
        }

        public class Layer
        {
            private readonly List<IHasBounds> _objects = new List<IHasBounds>();

            public IReadOnlyCollection<IHasBounds> Objects => _objects;

            public event Action<Layer, IHasBounds> Added;
            public event Action<Layer, IHasBounds> Removed;

            public Layer(IReadOnlyCollection<IHasBounds> objects)
            {
                foreach (var obj in objects)
                    Add(obj);
            }

            public void Add(IHasBounds obj)
            {
                _objects.Add(obj);
                Added?.Invoke(this, obj);
            }
        }

        public void Add(Layer layer)
        {
            if (layer == null) throw new ArgumentNullException(nameof(layer));
            _layers.Add(layer);
            LayerAdded?.Invoke(this, layer);
        }
    }
}
