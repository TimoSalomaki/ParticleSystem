using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ParticleSystem.Providers;

namespace ParticleSystem
{
    public class Emitter
    {
        private Random _rnd;
        
        public Emitter()
        {
            Particles = new List<Particle>();
            _rnd = new Random();
            ColorProvider = new GradientColorProvider();
            ColorProvider.AddColorStop(Color.Blue, 0f);
            /*ColorProvider.AddColorStop(Color.Green, 0.2f);
            ColorProvider.AddColorStop(Color.Red, 0.4f);
            ColorProvider.AddColorStop(Color.Blue, 0.6f);*/
            ColorProvider.AddColorStop(Color.Yellow, 0.9f);
            ColorProvider.AddColorStop(Color.Red, 1f);
        }

        // How many particles are emitted per frame update (1/60s)
        public int EmissionRate { get; set; } = 2;
        public List<Particle> Particles { get; }
        public Vector2 Location { get; set; }
        public int Lifetime { get; set; } = 180;
        public int ParticleMaximumLife { get; set; } = 60;
        public int StartDelay { get; set; } = 60;
        public double ParticleMaxSpeed { get; set; } = 3;
        public int Age { get; set; }
        public bool Loop { get; set; } = true;
        public bool Active { get; set; } = false;
        public bool Prewarm { get; set; } = false;
        public Color StartColor { get; set; } = new Color(255, 0, 0, 255);
        public Color EndColor { get; set; } = new Color(0, 0, 255, 0);
        public float StartScale { get; set; } = 3f;
        public float EndScale { get; set; } = 0f;
        public Texture2D Texture { get; set; }
        public IGradientColorProvider ColorProvider { get; set; }

        public void Trigger()
        {
            Active = true;

            if (Loop)
            {
                Particles.Clear();
            }

            if (Loop && Prewarm)
            {
                ResetToStart();

                for (int i = 0; i < Lifetime; i++)
                {
                    Update();
                }
            }

            ResetToStart();
        }

        public void Stop()
        {
            Active = false;
        }

        public void Update()
        {
            if(Age == Lifetime)
            {
                if (!Loop)
                    Active = false;

                else
                    ResetToStart();
            }

            else
            {
                Age++;
            }

            if (Age > 0 && Active)
            {
                EmitParticles();
            }

            UpdateParticles();
        }

        public void ResetToStart()
        {
            if(!Prewarm)
                Age = 0 - StartDelay;

            else
                Age = 0;
        }

        public void EmitParticles()
        {
            for (int i = 0; i < EmissionRate; i++)
            {
                Particles.Add(CreateNewParticle());
            }
        }

        public void UpdateParticles()
        {
            foreach (var particle in Particles.ToList())
            {
                // If the particle is alive, update it
                if (particle.Age < ParticleMaximumLife)
                {
                    UpdateParticle(particle);
                }

                // The particle is dead, remove it
                else
                {
                    Particles.Remove(particle);
                }
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (var particle in Particles)
            {
                RenderParticle(particle, spriteBatch);
            }
        }

        public void UpdateParticle(Particle particle)
        {
            particle.Age++;
            var particlePercent = (float)particle.Age / ParticleMaximumLife;
            particle.PreviousPosition = particle.Position;
            particle.Position += particle.Velocity;
            particle.Scale = LerpFloat(StartScale, EndScale, particlePercent);
            //particle.Color = Color.Lerp(StartColor, EndColor, particlePercent);
            //particle.Color = ColorProvider.GetValue(particlePercent);
        }

        public void RenderParticle(Particle particle, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, particle.Position, Texture.Bounds,
                particle.Color, 0, Vector2.Zero, particle.Scale,
                SpriteEffects.None, 0);
        }

        public Particle CreateNewParticle()
        {
            var emitterPercent = (float)Age / Lifetime;

            Particle particle = new Particle();
            particle.Velocity = GetRandomVelocity();
            particle.Position = new Vector2(
                Location.X - Texture.Width * StartScale * 0.5f,
                Location.Y - Texture.Height * StartScale * 0.5f);
            particle.Color = ColorProvider.GetValue(emitterPercent);
            return particle;
        }

        public Vector2 GetRandomVelocity()
        {            
            var r = ParticleMaxSpeed * Math.Sqrt(_rnd.NextDouble());
            var theta = _rnd.NextDouble() * 2 * Math.PI;

            var x = r * Math.Cos(theta);
            var y = r * Math.Sin(theta);

            return new Vector2((float)x, (float)y);
        }

        public float LerpFloat(float from, float to, float percentage)
        {
            return Math.Abs(to - ((to - from) * (1 - percentage)));
        }
    }
}
