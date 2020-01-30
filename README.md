# ParticleSystem

This is an ongoing research project into developing a particle system from ground up, by iterating through simple CPU accelerated 2D versions before getting into the GPU accelerated 3D system.

## Step 1 - Simple particle system ([V1-Initial](https://github.com/HankiDesign/ParticleSystem/tree/V1-Initial))
![V1 Image](https://github.com/HankiDesign/ParticleSystem/blob/master/v1.png?raw=true)
- 2D only
- Particle age determines how long it lives (simulates and renders) as part of the emitter
- Random direction (velocity) is given to the particle at birth
- Single texture is used for all particles
- Single continuous emitter, no looping of any kind
- Emitter's control for particle emission defines how many particles are emitted per frame. This would be more intuitive to deal with in seconds, but that will come in a future step.

### Step 1.1 ([V2-TriggerStopLoop](https://github.com/HankiDesign/ParticleSystem/tree/V2-TriggerStopLoop))
- Trigger/Stop allows the particle emission to start and stop. If emission is stopped, already existing particles will continue to simulate and render until they're dead.
- Loop allows the emission to run through n cycles. Looping is useful for effects that are not static and behave differently over the lifetime of the emitter.

### Step 1.2
- When start delay is more than 0, each loop of the emitter is delayed by that many frames.
- Prewarming enables the simulation to run one full lifecycle of the emitter before rendering anything.

### Step 1.3 ([V3](https://github.com/HankiDesign/ParticleSystem/tree/V3))
![V3 Image](https://github.com/HankiDesign/ParticleSystem/blob/master/v3.png)
- Gradient color provider gives linearly interpolated gradient colors to the particle over its lifetime or emitters lifetime.
