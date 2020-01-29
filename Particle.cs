using Microsoft.Xna.Framework;

namespace ParticleSystem
{
    public class Particle
    {
        public float Opacity { get; set; } = 1f;
        public float Scale { get; set; } = 10f;
        public int Age { get; set; }
        public Color Color { get; set; } = Color.White;
        public Vector2 PreviousPosition { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; } = Vector2.Zero;
    }
}