using ParticleSystem.Providers.Color;
using ParticleSystem.Providers.Numeric;

namespace ParticleSystem.Emitters
{
    public class PointEmitter2D : Emitter2D
    {
        public PointEmitter2D(GradientProvider colorProvider,
            FloatProvider scaleProvider, FloatProvider opacityProvider)
            : base(colorProvider, scaleProvider, opacityProvider) { }
    }
}
