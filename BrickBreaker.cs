using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BrickBreaker
{
    public class Game1 : Game
    {
       #region Declarations
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //sets the size of the window
        const int WINDOW_HEIGHT = 700;
        const int WINDOW_WIDTH = 600;

        //declare objects as vectors
        Texture2D paddle;
        Vector2 paddlePos;

        Vector2 brickPos;
        Brick brick = new Brick();
        List<Brick> bricks = new List<Brick>();

        Texture2D ball;
        Vector2 ballPos;
        Vector2 ballSpd = new Vector2(400, 100);

        #endregion

       #region Content
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ball = Content.Load<Texture2D>("ball");
            ballPos = new Vector2((WINDOW_WIDTH / 2) - ball.Width / 2, (WINDOW_HEIGHT / 2) - ball.Height / 2);

            paddle = Content.Load<Texture2D>("paddle");
            paddlePos = new Vector2((WINDOW_WIDTH / 2) - paddle.Width / 2, 550f);

            
        }
    
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        #endregion

       #region Ball Movement and Collision

            ballPos += ballSpd * (float)gameTime.ElapsedGameTime.TotalSeconds;

            int maxX = GraphicsDevice.Viewport.Width - ball.Width;
            int maxY = GraphicsDevice.Viewport.Height - ball.Height;

            //Check for bounce
            if (ballPos.Y < 0)
                ballSpd.Y *= -1;

            if (ballPos.X > maxX || ballPos.X < 0)
                ballSpd.X *= -1;

            //Collision
            Rectangle ballRect =
                new Rectangle((int)ballPos.X, (int)ballPos.Y,
                    ball.Width, ball.Height);

            #endregion

       #region Respawn


            if (ballPos.Y > maxY)
            {
                ballPos = new Vector2((WINDOW_WIDTH / 2) - ball.Width / 2, (WINDOW_HEIGHT / 2) - ball.Height / 2);
            }

            #endregion

       #region Paddle Movement and Collision
            //Add Paddle Movement

            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Right))
                paddlePos.X += 10;
            else if (keyState.IsKeyDown(Keys.Left))
                paddlePos.X -= 10;

            //Collision
            Rectangle paddleRect =
                new Rectangle((int)paddlePos.X, (int)paddlePos.Y,
                    paddle.Width, paddle.Height);

            if (ballRect.Intersects(paddleRect) && ballSpd.Y > 0)
            {
                ballSpd.Y += 10;
                if (ballSpd.Y < 0)
                    ballSpd.Y -= 10;
                else
                    ballSpd.Y += 10;

                ballSpd.Y *= -1;
            }

            //collision with border

            if (paddlePos.X < 0)
            {
                paddlePos.X = 0;
            }

            else if (paddlePos.X >= WINDOW_WIDTH - paddle.Width)
            {
                paddlePos.X = WINDOW_WIDTH - paddle.Width;
            }

            

            base.Update(gameTime);
        }
            #endregion

       #region Draw
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(paddle, paddlePos, Color.White);
            spriteBatch.Draw(ball, ballPos, Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
       #endregion

