using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameThing
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D playerTexture;
        Vector2 playerPosition;
        float playerSpeed;

        Texture2D bulletTexture;
        Vector2 bulletPosition;
        float bulletSpeed;
        string bulletDirection;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            playerPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            playerSpeed = 150f;

            bulletPosition = playerPosition;
            bulletSpeed = 450f;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loads in textures
            playerTexture = Content.Load<Texture2D>("player");
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

            // Player Movement
            if (kstate.IsKeyDown(Keys.W))
                playerPosition.Y -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.S))
                playerPosition.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.A))
                playerPosition.X -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.D))
                playerPosition.X += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Player screen wrap
            if (playerPosition.X > graphics.PreferredBackBufferWidth)
                playerPosition.X = 0;

            if (playerPosition.X < 0)
                playerPosition.X = graphics.PreferredBackBufferWidth;

            if (playerPosition.Y > graphics.PreferredBackBufferHeight)
                playerPosition.Y = 0;

            if (playerPosition.Y < 0)
                playerPosition.Y = graphics.PreferredBackBufferHeight;

            // Bullet Movement
            if (kstate.IsKeyDown(Keys.Up))
                bulletDirection = "UP";

            if (kstate.IsKeyDown(Keys.Down))
                bulletDirection = "DOWN";

            if (kstate.IsKeyDown(Keys.Left))
                bulletDirection = "LEFT";

            if (kstate.IsKeyDown(Keys.Right))
                bulletDirection = "RIGHT";

            // Resets bullet if it has gone off screen
            if (bulletPosition.Y < 0 ||
                bulletPosition.Y > graphics.PreferredBackBufferHeight ||
                bulletPosition.X < 0 ||
                bulletPosition.X > graphics.PreferredBackBufferWidth)
                bulletDirection = "";

            // Sets Bullet Direction
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
                    bulletPosition = playerPosition;
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black); // Frame Reset

            // Draws Player and Bullet
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
                playerTexture,
                playerPosition,
                null,
                Color.White,
                0f,
                new Vector2(playerTexture.Width / 2, playerTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
