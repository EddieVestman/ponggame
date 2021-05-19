using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Template
{
    public enum GameState
    {
        Game,
        Ending
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        


        public static int ScreenWidht;
        public static int ScreenHeight;
        public static Random Random;

        private Texture2D background;
        private Vector2 backgroundpos;

        private Score score;
        private List<Basklass> sprites;

        GameState gameState;

        string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "gameover.txt");

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ScreenWidht = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;
            Random = new Random();
            gameState = GameState.Game;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("playarea");
            Texture2D Playertexture = Content.Load<Texture2D>("spelare");
            Texture2D Bolltexture = Content.Load<Texture2D>("boll");
            SpriteFont textFont = Content.Load<SpriteFont>("Font");


            score = new Score(textFont);

            sprites = new List<Basklass>() //lista på mina två sprites 
      {
        new Players(Playertexture)
        {
          Position = new Vector2(20, (ScreenHeight / 2) - (Playertexture.Height / 2)),
          Input = new Input()
          {
            Up = Keys.W,
            Down = Keys.S,
          }
        },
        new Players(Playertexture)
        {
          Position = new Vector2(ScreenWidht - 20 - Playertexture.Width, (ScreenHeight / 2) - (Playertexture.Height / 2)),
          Input = new Input()
          {
            Up = Keys.Up,
            Down = Keys.Down,
          }
        },
        new Boll(Bolltexture)
        {
          Position = new Vector2((ScreenWidht / 2) - (Bolltexture.Width / 2), (ScreenHeight / 2) - (Bolltexture.Height / 2)),
            Score = score
        }
      };


            // TODO: use this.Content to load your game content here 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameState = GameState.Ending;
                StreamWriter sw = new StreamWriter(filepath);
                sw.WriteLine("Gameover");
                sw.Close();
            }

                
            switch (gameState)
            {
                case GameState.Game:
                    UpdateGame(gameTime);
                    break;
                case GameState.Ending:
                     Exit();
                    break;
            }
           

            base.Update(gameTime);
        }

        public void UpdateGame(GameTime gameTime)
        {
            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime, sprites);
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Rectangle backgroundRec = new Rectangle();
            backgroundRec.Location = backgroundpos.ToPoint();
            backgroundRec.Size = new Point(ScreenWidht, ScreenHeight);
            spriteBatch.Draw(background, backgroundRec, Color.White);

            foreach (Basklass item in sprites)
            {
                item.Draw(spriteBatch);
            }
            score.Draw(spriteBatch);

            spriteBatch.End();

            // TODO: Add your drawing code here.

            base.Draw(gameTime);
        }
    }
}
