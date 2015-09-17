using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

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

        Texture2D testGroundSprite;
        Body testGround;
        Vector2 testGroundOrigin;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;

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

            world = new World(new Vector2(0.0f, 9.82f));
            //world = new World(new Vector2(0.0f, 1.5f));
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

            // Load content for line
            Texture2D lineTexture = Content.Load<Texture2D>("Sprites\\linesegment");
            line.Initialize(world, lineTexture);

            // Test ground
            testGroundSprite = Content.Load<Texture2D>("Sprites\\testgroundsprite");
            testGroundOrigin = new Vector2(testGroundSprite.Width / 2f, testGroundSprite.Height / 2f);

            Vector2 groundPosition = ConvertUnits.ToSimUnits(screenCenter) + new Vector2(0, 5.0f);
            testGround = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(512f), ConvertUnits.ToSimUnits(64f), 1f, groundPosition);
            testGround.IsStatic = true;
            testGround.Restitution = 0.3f;
            testGround.Friction = 0.5f;
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
            HandleMouse();
            HandleKeyboard();
            HandleGamePad();
            world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);

            base.Update(gameTime);
        }

        private void HandleMouse()
        {
            MouseState mouseState = Mouse.GetState();

            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                line.BeginDrawing(mouseState.Position);
            }

            else if(mouseState.LeftButton == ButtonState.Released)
            {
                line.EndDrawing(mouseState.Position);
            }
        }

        private void HandleKeyboard()
        {
            // See Farseer HelloWorld project for ideas
            KeyboardState keyState = Keyboard.GetState();
        }

        private void HandleGamePad()
        {
            // See Farseer HelloWorld project for ideas
            GamePadState padState = GamePad.GetState(0);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            line.Draw(spriteBatch);
            orbitBall.Draw(spriteBatch);

            spriteBatch.Draw(testGroundSprite, ConvertUnits.ToDisplayUnits(testGround.Position), null, Color.White, 0f, testGroundOrigin, 1f, SpriteEffects.None, 0f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
