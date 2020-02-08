# ParticleSystem
![.NET Core](https://github.com/HankiDesign/ParticleSystem/workflows/.NET%20Core/badge.svg)

This is an ongoing research project into developing a particle system from ground up, by iterating through simple CPU accelerated 2D versions before getting into the GPU accelerated 3D system.

The first version of the system is extremely naive, and it doesn't contain any optimizations that are required for pretty much any well functioning particle system. For example, `Particle` is a rather big class with properties, instead of a much more GC (Garbage Collection) friendly struct.

Throughout the process, I'll update the [todo](#todo) list and [progress](#progress) sections below. It's also entirely possible that I'll implement a certain feature, but still scrap it in a later version or change it drastically. As mentioned before, this is a research project and the real-world usage is not an immediate goal.

## Progress

### Step 1 - Simple particle system ([V1-Initial](https://github.com/HankiDesign/ParticleSystem/tree/V1-Initial))
![V1 Image](https://github.com/HankiDesign/ParticleSystem/blob/master/Images/v1.png?raw=true)
- 2D only
- Particle age determines how long it lives (simulates and renders) as part of the emitter
- Random direction (velocity) is given to the particle at birth
- Single texture is used for all particles
- Single continuous emitter, no looping of any kind
- Emitter's control for particle emission defines how many particles are emitted per frame. This would be more intuitive to deal with in seconds, but that will come in a future step.

#### Step 1.1 ([V2-TriggerStopLoop](https://github.com/HankiDesign/ParticleSystem/tree/V2-TriggerStopLoop))
- Trigger/Stop allows the particle emission to start and stop. If emission is stopped, already existing particles will continue to simulate and render until they're dead.
- Loop allows the emission to run through n cycles. Looping is useful for effects that are not static and behave differently over the lifetime of the emitter.

#### Step 1.2
- When start delay is more than 0, each loop of the emitter is delayed by that many frames.
- Prewarming enables the simulation to run one full lifecycle of the emitter before rendering anything.
- Color, scale and opacity interpolation from start to end of particle's life

#### Step 1.3 ([V3](https://github.com/HankiDesign/ParticleSystem/tree/V3))
![V3 Image](https://github.com/HankiDesign/ParticleSystem/blob/master/Images/v3.png)
- Gradient color provider gives linearly interpolated gradient colors to the particle over its lifetime or emitters lifetime.

### Step 2 - More dynamic system ([Dynamic_LinearInterpolation](https://github.com/HankiDesign/ParticleSystem/tree/Dynamic_LinearInterpolation))
![Dynamic_LinearInterpolation Image](https://github.com/HankiDesign/ParticleSystem/blob/master/Images/LinearInterpolationProviders.gif)
- Generic [`ValueProvider<T,U>`](https://github.com/HankiDesign/ParticleSystem/blob/Dynamic_LinearInterpolation/Providers/ValueProvider.cs) class can be used to provide colors, scales, opacities etc. linearly interpolated over the lifetime of the emitter or the particle. For example, each particle can go through a gradient of colors during their lifetime using the `ValueProvider`. They can also start with scale 0, then grow to 8 times the size of the texture by the halfpoint of their life and then shrink to the size of the texture (scale = 1) by the time their lifetime is 100% and they die. Whatever values can be interpolated, could be used with this mechanism.

#### Step 2.1

![Rectangular Emitters](https://github.com/HankiDesign/ParticleSystem/blob/master/Images/RectangleEmitters.gif)

- [`IEmitter`](https://github.com/HankiDesign/ParticleSystem/blob/master/Emitters/IEmitter.cs) interface and [`Emitter2D`](https://github.com/HankiDesign/ParticleSystem/blob/master/Emitters/Emitter2D.cs) abstract base class created so that emitters can more easily be created with different shapes and functionality.
 - `PointEmitter2D` is the original emitter with just a location and each particle is emitter from that single point
 - `RectangleEmitter2D` is a rectangular emitter with a location (top left corner), and width and height so that particles are emitted randomly at any point within the emitter. See example animation of two rectangular emitters side by side above.

## Todo

### Particle attributes

* [X] 2D
* [X] Age
* [X] Lifetime
* [X] Velocity
* [X] Texture
* [X] Scale
* [X] Opacity
* [X] Color

### Emitter
* [X] 2D
* [X] Age
* [X] Lifetime
* [X] Continuous emitter
* [X] Looping emitter
* [X] Start/Stop emitter
* [X] Start delay
* [X] Prewarming
* [ ] 3D

### Particle lifetime

* [X] Color interpolation
* [X] Scale interpolation
* [X] Opacity interpolation
* [X] Generic ValueProvider<T,U> for implementing easy linear interpolation of any values over the lifetime of the particle or emitter
 * [X] FloatProvider
 * [X] GradientProvider
* [ ] ~~IColorProvider for providing colors over particle's or emitter's lifetime~~
  * [ ] ~~GradientColorProvider~~
* [ ] ~~IVelocityProvider for providing velocities over particle's or emitter's lifetime~~
* [ ] ~~IScaleProvider for providing velocities over particle's or emitter's lifetime~~
* [ ] ~~IOpacityProvider for providing velocities over particle's or emitter's lifetime~~

### Optimizations

* [ ] Particle pool (object pool pattern)
* [ ] Particle as a struct instead of class, to avoid GC as the memory is allocated on stack instead of heap
* [ ] Color optimisations
* [ ] Particle struct memory alignment
* [ ] Vector2 optimizations (avoid boxing/unboxing)

## Using the particle system

This is an ongoing research project, thus it's not recommended to use this for anything serious. You're free to do so, as long as you respect the [license terms](LICENSE.md) but I'm not going to provide support, accept pull requests or work on fixing any possible issues.

## License

This project is published with the MIT License, you can read the terms [here](LICENSE.md).
