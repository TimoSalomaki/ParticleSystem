using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace ParticleSystem.Providers
{
    public class GradientColorProvider : IGradientColorProvider
    {
        private List<ColorStop> _colorStops;

        public GradientColorProvider()
        {
            _colorStops = new List<ColorStop>();
        }

        public void AddColorStop(Color color, float percentage)
        {
            if (percentage < 0 || percentage > 1)
                throw new ArgumentOutOfRangeException();

            var newItem = new ColorStop(color, percentage);

            if (!_colorStops.Any())
            {
                _colorStops.Add(newItem);
            }

            else
            {
                for (int i = 0; i < _colorStops.Count; i++)
                {
                    if (_colorStops[i].Percentage == percentage) // Replace existing color stop
                        _colorStops[i].Color = color;

                    else if (_colorStops[i].Percentage > percentage)
                    {
                        _colorStops.Insert(i, newItem);
                        break;
                    }

                    else if (i == _colorStops.Count - 1) // Last item
                    {
                        _colorStops.Insert(i + 1, newItem);
                    }
                }
            }
        }

        public void ClearColorStops()
        {
            _colorStops.Clear();
        }

        public Color GetValue(float percentage)
        {
            var activeColorStops = FindColorStops(percentage);

            var relativePercentage = (percentage - activeColorStops.from.Percentage) /
                (activeColorStops.to.Percentage - activeColorStops.from.Percentage);

            return Color.Lerp(activeColorStops.from.Color, activeColorStops.to.Color, relativePercentage);
        }

        private (ColorStop from, ColorStop to) FindColorStops(float percentage)
        {
            ColorStop from = _colorStops.First();
            ColorStop to = null;

            foreach (var stop in _colorStops)
            {
                if (stop.Percentage > percentage)
                {
                    to = stop;
                    break;
                }

                from = stop;
            }

            if (to == null)
                to = _colorStops.Last();

            return (from, to);
        }
    }
}
