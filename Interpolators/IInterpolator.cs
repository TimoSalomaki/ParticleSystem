namespace ParticleSystem.Interpolators
{
    public interface IInterpolator<T>
    {
        T Interpolate(T from, T to, float percentage);
    }
}
