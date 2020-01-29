using System;
using Microsoft.Xna.Framework;

namespace ParticleSystem.Providers
{
    public class ColorStop
    {
        public ColorStop(Color color, float percentage)
        {
            this.Color = color;
            this.Percentage = percentage;
        }

        public Color Color { get; set; }
        public float Percentage { get; set; }
    }
}
