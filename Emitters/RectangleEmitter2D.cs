using Microsoft.Xna.Framework;
using ParticleSystem.Interpolators;
using ParticleSystem.Providers;

namespace ParticleSystem.Emitters
{
    public class RectangleEmitter2D : Emitter2D
    {
        public RectangleEmitter2D(float width, float height,
            ValueProvider<Color, ColorInterpolator> colorProvider,
            ValueProvider<float, FloatInterpolator> scaleProvider,
            ValueProvider<float, FloatInterpolator> opacityProvider)
            : base(colorProvider, scaleProvider, opacityProvider)
        {
            Width = width;
            Height = height;
        }

        public float Width { get; set; }
        public float Height { get; set; }

        public override Vector2 InitializePosition()
        {
            return new Vector2(Location.X + Width * (float)_rnd.NextDouble(),
                Location.Y + Height * (float)_rnd.NextDouble());
        }
    }
}