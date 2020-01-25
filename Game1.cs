using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParticleSystem
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Texture2D _particle;
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
            _emitter.Location = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2, _graphics.GraphicsDevice.Viewport.Height / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _particle = Content.Load<Texture2D>("ParticleSolid");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _emitter.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _emitter.Render(_spriteBatch, _particle);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
