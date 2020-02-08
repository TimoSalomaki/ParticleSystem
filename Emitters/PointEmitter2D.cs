using Microsoft.Xna.Framework;
using ParticleSystem.Interpolators;
using ParticleSystem.Providers;

namespace ParticleSystem.Emitters
{
    public class PointEmitter2D : Emitter2D
    {
        public PointEmitter2D(ValueProvider<Color, ColorInterpolator> colorProvider,
            ValueProvider<float, FloatInterpolator> scaleProvider,
            ValueProvider<float, FloatInterpolator> opacityProvider)
            : base(colorProvider, scaleProvider, opacityProvider) { }
    }
}
