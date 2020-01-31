namespace ParticleSystem.Interpolators
{
    public class FloatInterpolator : IInterpolator<float>
    {
        public float Interpolate(float from, float to, float percentage)
        {
            return from + (to - from) * percentage;
        }
    }
}
