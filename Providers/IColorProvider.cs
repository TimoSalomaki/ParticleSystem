using Microsoft.Xna.Framework;

namespace ParticleSystem.Providers
{
    public interface IGradientColorProvider
    {
        void AddColorStop(Color color, float percentage);
        void ClearColorStops();
        Color GetValue(float percentage);
    }
}
