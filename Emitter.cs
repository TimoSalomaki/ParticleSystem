using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ParticleSystem
{
    public class Emitter
    {
        private Random _rnd;
        
        public Emitter()
        {
            Particles = new List<Particle>();
            _rnd = new Random();
        }

        public int EmissionRate { get; set; } = 50; // How many particles are emitted per frame update (1/60s)
        public List<Particle> Particles { get; }
        public Vector2 Location { get; set; }
        public int Lifetime { get; set; } = 1;
        public int StartDelay { get; set; } = 30;
        public double ParticleMinSpeed { get; set; } = -2;
        public double ParticleMaxSpeed { get; set; } = 2;
        public int Age { get; set; }
        public bool Loop { get; set; } = true;
        public bool Active { get; set; } = false;
        public Color StartColor { get; set; } = Color.White;
        public Color EndColor { get; set; } = Color.Red;
        public float StartScale { get; set; } = 3;
        public float EndScale { get; set; } = 0;

        public void Trigger()
        {
            ResetToStart();
            Active = true;
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
            Age = 0 - StartDelay;
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
                if (particle.Age < particle.MaximumLife)
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

        public void Render(SpriteBatch spriteBatch, Texture2D texture)
        {
            foreach (var particle in Particles)
            {
                RenderParticle(particle, spriteBatch, texture);
            }
        }

        public void UpdateParticle(Particle particle)
        {
            particle.Age++;
            var particlePercent = (float)particle.Age / particle.MaximumLife;
            particle.PreviousPosition = particle.Position;
            particle.Position += particle.Velocity;
            particle.Color = Color.Lerp(StartColor, EndColor, particlePercent);
            particle.Scale = Math.Abs(EndScale - ((EndScale - StartScale) * (1-particlePercent)));
        }

        public void RenderParticle(Particle particle, SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, particle.Position, texture.Bounds, particle.Color, 0, Vector2.Zero, particle.Scale, SpriteEffects.None, 0);
        }

        public Particle CreateNewParticle()
        {
            Particle particle = new Particle();
            particle.Velocity = new Vector2(GetRandomVelocity(), GetRandomVelocity());
            particle.Position = Location;

            return particle;
        }

        public float GetRandomVelocity()
        {
            return (float)(_rnd.NextDouble() * (ParticleMaxSpeed - ParticleMinSpeed) + ParticleMinSpeed);
        }
    }
}
