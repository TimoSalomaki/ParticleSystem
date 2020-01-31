namespace ParticleSystem.Providers
{
    public interface IValueProvider<T, U>
    {
        void AddValuePoint(float percentage, T value);
        T GetValue(float percentage);
        void ClearValues();
    }
}
