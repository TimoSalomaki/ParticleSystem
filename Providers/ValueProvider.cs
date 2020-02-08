using System;
using System.Collections.Generic;
using System.Linq;
using ParticleSystem.Interpolators;

namespace ParticleSystem.Providers
{
    public class ValueProvider<T, U> : IValueProvider<T, U>
        where U : IInterpolator<T>, new()
    {
        U _interpolator;

        public ValueProvider() : this(ProviderType.Dynamic) { }

        public ValueProvider(ProviderType providerType)
            : this(providerType, default(T)) { }

        public ValueProvider(ProviderType providerType, T startValue)
            : this(providerType, startValue, default(T)) { }

        public ValueProvider(ProviderType providerType, T startValue, T endValue)
        {
            Values = new List<T>();
            Percentages = new List<float>();

            if (startValue != null)
            {
                Values.Add(startValue);
                Percentages.Add(0);
            }

            if (endValue != null)
            {
                Values.Add(endValue);
                Percentages.Add(1);
            }

            ProviderType = providerType;
            _interpolator = new U();
        }

        public ValueProvider(ProviderType providerType, IEnumerable<T> values,
            IEnumerable<float> percentages)
        {
            Values = new List<T>(values);
            Percentages = new List<float>(percentages);
            ProviderType = providerType;
            _interpolator = new U();
        }

        public List<T> Values { get; set; }
        public List<float> Percentages { get; set; }
        public ProviderType ProviderType { get; set; }

        public void AddValuePoint(float percentage, T value)
        {
            if (percentage < 0 || percentage > 1)
                throw new ArgumentOutOfRangeException();

            if (!Percentages.Any())
            {
                Values.Add(value);
                Percentages.Add(percentage);
            }

            else
            {
                for (int i = 0; i < Percentages.Count; i++)
                {
                    if (Percentages[i] == percentage)
                    {
                        Values[i] = value;
                    }

                    else if (Percentages[i] > percentage)
                    {
                        Percentages.Insert(i, percentage);
                        Values.Insert(i, value);
                        break;
                    }

                    else if (i == Percentages.Count - 1)
                    {
                        Percentages.Insert(i + 1, percentage);
                        Values.Insert(i + 1, value);
                    }
                }
            }
        }

        public void ClearValues()
        {
            Values.Clear();
            Percentages.Clear();
        }

        public T GetValue(float percentage)
        {
            if (ProviderType == ProviderType.Dynamic)
            {
                var nearestValues = FindNearestValues(percentage);

                var relativePercentage = (percentage - nearestValues.fromPercentage) /
                    (nearestValues.toPercentage - nearestValues.fromPercentage);

                return _interpolator.Interpolate(nearestValues.fromValue, nearestValues.toValue, relativePercentage);
            }

            // Static provider always returns the first value from the Values list without interpolation
            return Values.First();
        }

        private (float fromPercentage, float toPercentage, T fromValue, T toValue) FindNearestValues(float percentage)
        {
            T from = Values.First();
            T to = default;
            float fromPercentage = 0;
            float toPercentage = 1;

            for(int i = 0; i < Percentages.Count; i++)
            {
                if (Percentages[i] > percentage)
                {
                    to = Values[i];
                    toPercentage = Percentages[i];
                    break;
                }

                from = Values[i];
                fromPercentage = Percentages[i];
            }

            if (to.Equals(default(T)))
            {
                to = Values.Last();
                toPercentage = Percentages.Last();
            }

            return (fromPercentage, toPercentage, from, to);
        }
    }

    public enum ProviderType
    {
        Static,
        Dynamic
    }
}
