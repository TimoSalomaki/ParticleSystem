namespace ParticleSystem.Providers
{
    public interface IValueProvider<T, U>
    {
        ProviderType ProviderType { get; set; }

        void AddValuePoint(float percentage, T value);
        T GetValue(float percentage);
        void ClearValues();
    }
}
