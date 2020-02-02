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

        void Render(SpriteBatch spriteBatch);
        void ResetToStart();
        void Stop();
        void Trigger();
        void Update();
    }
}