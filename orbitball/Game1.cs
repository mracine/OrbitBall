using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using FarseerPhysics;
using FarseerPhysics.Dynamics;

namespace orbitball
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 screenCenter;

        World world;
        Ball orbitBall;
        Line line;

        Texture2D lineTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 400;

            this.Window.Title = "OrbitBall";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // Farseer expects objects to be scaled to MKS (meters, kilos, seconds)
            // 1 meters equals 64 pixels here
            ConvertUnits.SetDisplayUnitToSimUnitRatio(64.0f);

            screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2f, graphics.GraphicsDevice.Viewport.Height / 2f);

            //world = new World(new Vector2(0.0f, 9.82f));
            world = new World(new Vector2(0.0f, 1.5f));
            orbitBall = new Ball();
            line = new Line();

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

            // TODO: use this.Content to load your game content here
            // 100px x 100px => 1.5625m x 1.5625m
            Texture2D orbitBallTexture = Content.Load<Texture2D>("Sprites\\ball");
            orbitBall.Initialize(world, orbitBallTexture, ConvertUnits.ToSimUnits(screenCenter) + new Vector2(0, -1.0f * ConvertUnits.ToSimUnits(orbitBallTexture.Height)));
            line.Initialize(world);
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
                Exit();

            // TODO: Add your update logic here
            HandleGamePad();
            HandleKeyboard();
            world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);

            base.Update(gameTime);
        }

        private void HandleGamePad()
        {
            // See Farseer HelloWorld project for ideas
        }

        private void HandleKeyboard()
        {
            // See Farseer HelloWorld project for ideas
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Draw background objects first
            spriteBatch.Begin();
            spriteBatch.End();

            // Draw foreground objects
            spriteBatch.Begin();

            orbitBall.Draw(spriteBatch);
            line.Draw(spriteBatch);

            spriteBatch.End();

            // Draw debug
            spriteBatch.Begin();
            //spriteBatch.DrawString(_font, Text, new Vector2(14.0f, 14.0f), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
