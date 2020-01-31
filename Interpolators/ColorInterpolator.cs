using Microsoft.Xna.Framework;

namespace ParticleSystem.Interpolators
{
    public class ColorInterpolator : IInterpolator<Color>
    {
        public Color Interpolate(Color from, Color to, float percentage)
        {
            return Color.Lerp(from, to, percentage);
        }
    }
}
