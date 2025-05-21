using System;
using System.Collections.Generic;

namespace ParticleSystem.Pools
{
    /// <summary>
    /// Manages a pool of reusable Particle instances to reduce garbage collection.
    /// </summary>
    public class ParticlePool
    {
        private readonly Stack<Particle> _availableParticles;
        private readonly int _growthFactor;
        
        /// <summary>
        /// Gets the current number of available particles in the pool.
        /// </summary>
        public int AvailableCount => _availableParticles.Count;

        /// <summary>
        /// Creates a new particle pool with the specified initial capacity.
        /// </summary>
        /// <param name="initialCapacity">The initial number of particles to pre-allocate.</param>
        /// <param name="growthFactor">How many particles to add when the pool is empty.</param>
        public ParticlePool(int initialCapacity = 100, int growthFactor = 20)
        {
            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity), "Initial capacity cannot be negative.");
            
            if (growthFactor <= 0)
                throw new ArgumentOutOfRangeException(nameof(growthFactor), "Growth factor must be positive.");
            
            _availableParticles = new Stack<Particle>(initialCapacity);
            _growthFactor = growthFactor;
            
            // Pre-populate the pool with particles
            GrowPool(initialCapacity);
        }

        /// <summary>
        /// Creates new particles and adds them to the pool.
        /// </summary>
        /// <param name="count">The number of particles to add.</param>
        private void GrowPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _availableParticles.Push(new Particle());
            }
        }

        /// <summary>
        /// Retrieves a particle from the pool, or creates a new one if the pool is empty.
        /// </summary>
        /// <returns>A Particle instance.</returns>
        public Particle GetParticle()
        {
            if (_availableParticles.Count == 0)
            {
                GrowPool(_growthFactor);
            }

            return _availableParticles.Pop();
        }

        /// <summary>
        /// Returns a particle to the pool for reuse.
        /// </summary>
        /// <param name="particle">The particle to return to the pool.</param>
        public void ReleaseParticle(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException(nameof(particle));
            
            // Reset particle to default state before returning to the pool
            ResetParticle(particle);
            
            // Add it back to the available pool
            _availableParticles.Push(particle);
        }

        /// <summary>
        /// Resets a particle to its default state.
        /// </summary>
        /// <param name="particle">The particle to reset.</param>
        private void ResetParticle(Particle particle)
        {
            particle.Opacity = 1f;
            particle.Scale = 10f;
            particle.Angle = 0f;
            particle.Age = 0;
            particle.Color = Microsoft.Xna.Framework.Color.White;
            particle.PreviousPosition = Microsoft.Xna.Framework.Vector2.Zero;
            particle.Position = Microsoft.Xna.Framework.Vector2.Zero;
            particle.Velocity = Microsoft.Xna.Framework.Vector2.Zero;
        }
    }
}