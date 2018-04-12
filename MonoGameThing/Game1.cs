using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameThing
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D textureBall;
        Vector2 ballPosition;
        float ballSpeed;

        Texture2D bulletTexture;
        Vector2 bulletPosition;
        float bulletSpeed;
        string bulletDirection;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            ballPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 100f;

            bulletPosition = ballPosition;
            bulletSpeed = 500f;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textureBall = Content.Load<Texture2D>("ball");
            bulletTexture = Content.Load<Texture2D>("bullet");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W))
                ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.S))
                ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.A))
                ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.D))
                ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Up))
                bulletDirection = "UP";

            if (kstate.IsKeyDown(Keys.Down))
                bulletDirection = "DOWN";

            if (kstate.IsKeyDown(Keys.Left))
                bulletDirection = "LEFT";

            if (kstate.IsKeyDown(Keys.Right))
                bulletDirection = "RIGHT";

            if (bulletPosition.Y < 0 ||
                bulletPosition.Y > graphics.PreferredBackBufferHeight ||
                bulletPosition.X < 0 ||
                bulletPosition.X > graphics.PreferredBackBufferWidth)
                bulletDirection = "";

            switch (bulletDirection)
            {
                case "UP":
                    bulletPosition.Y -= bulletSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case "DOWN":
                    bulletPosition.Y += bulletSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case "LEFT":
                    bulletPosition.X -= bulletSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case "RIGHT":
                    bulletPosition.X += bulletSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                default:
                    bulletPosition = ballPosition;
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.Draw(
                bulletTexture,
                bulletPosition,
                null,
                Color.White,
                0f,
                new Vector2(bulletTexture.Width / 2, bulletTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            spriteBatch.Draw(
                textureBall,
                ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(textureBall.Width / 2, textureBall.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            spriteBatch.End();

            if (ballPosition.X > graphics.PreferredBackBufferWidth)
                ballPosition.X = 0;

            if (ballPosition.X < 0)
                ballPosition.X = graphics.PreferredBackBufferWidth;

            if (ballPosition.Y > graphics.PreferredBackBufferHeight)
                ballPosition.Y = 0;

            if (ballPosition.Y < 0)
                ballPosition.Y = graphics.PreferredBackBufferHeight;

            base.Draw(gameTime);
        }
    }
}
