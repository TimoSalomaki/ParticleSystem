using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSystem.Emitters
{
    public interface IEmitter
    {
        List<Particle> Particles { get; }
        bool Loop { get; set; }
        bool Active { get; set; }
        Texture2D Texture { get; set; }
        Vector2 Location { get; set; }
        int Lifetime { get; set; }
        int StartDelay { get; set; }
        int ParticleMaximumLife { get; set; }
        double ParticleMaxSpeed { get; set; }
        int Age { get; set; }
        bool Prewarm { get; set; } 

        void Render(SpriteBatch spriteBatch);
        void ResetToStart();
        void Stop();
        void Trigger();
        void Update();
    }
}