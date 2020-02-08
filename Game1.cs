using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParticleSystem.Helpers;
using ParticleSystem.Emitters;
using ParticleSystem.Providers;
using ParticleSystem.Interpolators;

namespace ParticleSystem
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IEmitter _emitter;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //_graphics.PreferredBackBufferWidth = 1920;
            //_graphics.PreferredBackBufferHeight = 1080;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
         }

        protected override void Initialize()
        {
            var colorProvider = new ValueProvider<Color, ColorInterpolator>(ProviderType.Dynamic,
                new Color[]{ Color.White, Color.Yellow, Color.Red}, new float[] { 0, 0.5f, 1 });
            var scaleProvider = new ValueProvider<float, FloatInterpolator>(ProviderType.Dynamic, 2, 0);
            var opacityProvider = new ValueProvider<float, FloatInterpolator>(ProviderType.Static, 1);

            _emitter = new PointEmitter2D(colorProvider, scaleProvider, opacityProvider);
            _emitter.ParticleMaxSpeed = 3;
            _emitter.Lifetime = 120;

            _emitter.Location = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2,
                _graphics.GraphicsDevice.Viewport.Height / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _emitter.Texture = Content.Load<Texture2D>("Arrow");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardHelper.UpdateState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (KeyboardHelper.IsPressed(Keys.Space))
            {
                _emitter.Trigger();
            }

            if (KeyboardHelper.IsPressed(Keys.S))
            {
                _emitter.Stop();
            }

            /*_emitter.Location = new Vector2(Mouse.GetState().Position.X,
                Mouse.GetState().Position.Y);

            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                _emitter.Trigger();*/

            _emitter.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            _spriteBatch.Begin();
            _emitter.Render(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
