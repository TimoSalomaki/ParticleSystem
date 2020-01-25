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
        private double _particleMinSpeed = -2;
        private double _particleMaxSpeed = 2;
        private bool _active = false;

        public Emitter()
        {
            Particles = new List<Particle>();
            _rnd = new Random();
        }

        public int EmissionRate { get; set; } = 5; // How many particles are emitted per frame update (1/60s)
        public List<Particle> Particles { get; }
        public Vector2 Location { get; set; }
        public int Lifetime { get; set; } = 1;
        public int Age { get; set; }
        public bool Loop { get; set; } = true;

        public void Trigger()
        {
            Age = 0;
            _active = true;
        }

        public void Stop()
        {
            _active = false;
        }

        public void Update()
        {
            if(Age == Lifetime && !Loop)
            {
                _active = false;
            }

            else
            {
                Age++;
            }

            if (_active)
            {
                EmitParticles();
            }

            UpdateParticles();
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
            particle.Position += particle.Velocity;
            particle.Age++;
        }

        public void RenderParticle(Particle particle, SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, particle.Position, particle.Color);
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
            return (float)(_rnd.NextDouble() * (_particleMaxSpeed - _particleMinSpeed) + _particleMinSpeed);
        }
    }
}
