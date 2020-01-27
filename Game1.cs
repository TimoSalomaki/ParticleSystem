using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParticleSystem
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Emitter _emitter;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
         }

        protected override void Initialize()
        {
            _emitter = new Emitter();
            _emitter.Location = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2,
                _graphics.GraphicsDevice.Viewport.Height / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _emitter.Texture = Content.Load<Texture2D>("ParticleSolid");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardHelper.UpdateState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(KeyboardHelper.IsPressed(Keys.Space))
                _emitter.Trigger();

            if (KeyboardHelper.IsPressed(Keys.S))
                _emitter.Stop();

            /*_emitter.Location = new Vector2(Mouse.GetState().Position.X,
                Mouse.GetState().Position.Y);

            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                _emitter.Trigger();*/

            _emitter.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _emitter.Render(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
