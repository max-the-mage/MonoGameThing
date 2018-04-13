using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;

namespace MonoGameThing
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D playerTexture;
        Vector2 playerPosition;
        CollisionManager playerCollsion;
        float playerSpeed;

        Texture2D bulletTexture;
        Vector2 bulletPosition;
        CollisionManager bulletCollision;
        float bulletSpeed;
        string bulletDirection;

        Texture2D enemyTexture;
        Vector2 enemyPosition;
        CollisionManager enemyCollision;

        Random rnd = new Random();

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
            playerCollsion = new CollisionManager(playerTexture,  playerPosition);

            bulletPosition = playerPosition;
            bulletSpeed = 450f;
            bulletDirection = "";
            bulletCollision = new CollisionManager(bulletTexture, bulletPosition);

            enemyPosition.X = rnd.Next(1, graphics.PreferredBackBufferWidth);
            enemyPosition.Y = rnd.Next(1, graphics.PreferredBackBufferHeight);
            enemyCollision = new CollisionManager(enemyTexture, enemyPosition);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loads in textures
            playerTexture = Content.Load<Texture2D>("player");
            bulletTexture = Content.Load<Texture2D>("bullet");
            enemyTexture  = Content.Load<Texture2D>("enemy");
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

            // Checks collision between the enemy and bullet

            bulletCollision.UpdatePosition(bulletPosition);
            enemyCollision.UpdatePosition(enemyPosition);
            playerCollsion.UpdatePosition(playerPosition);

            if (bulletCollision.CheckCollision(enemyCollision))
            {
                bulletDirection = "";
                enemyPosition.X = rnd.Next(1, graphics.PreferredBackBufferWidth - enemyTexture.Width);
                enemyPosition.Y = rnd.Next(1, graphics.PreferredBackBufferHeight - enemyTexture.Height);
            }

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
                    bulletPosition.X = playerPosition.X + playerTexture.Width / 2 - bulletTexture.Width / 2;
                    bulletPosition.Y = playerPosition.Y + playerTexture.Height / 2 - bulletTexture.Height / 2;
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
                Color.White
            );
            spriteBatch.Draw(
                playerTexture,
                playerPosition,
                Color.White
            );
            spriteBatch.Draw(
                enemyTexture,
                enemyPosition,
                Color.White
            );
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
