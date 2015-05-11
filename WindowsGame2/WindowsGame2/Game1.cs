using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 cameraPosition = Vector2.Zero;
        const float cameraSpeed = 1.0f;
        Point TestPoint { get; set; }
        Background mBackgroundOne;
        Background mBackgroundTwo;
        Background mBackgroundThree;
        Background mBackgroundFour;
        Background mBackgroundFive;
        Vector2 startPos1 = new Vector2(0, 0);
        Vector2 startPos2 = new Vector2(100, 0);
        Vector2 startPos3 = new Vector2(400, 0);
        Vector2 startPos4 = new Vector2(675, 0);
        Vector2 startPos5 = new Vector2(900, 0);
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
            // TODO: Add your initialization logic here
            ScreenManager.Instance.Initialize();
            ScreenManager.Instance.Dimensions = new Vector2(640, 480);
            graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            graphics.ApplyChanges();
            mBackgroundOne = new Background();
            mBackgroundOne.scale = 2.25f;

            mBackgroundTwo = new Background();
            mBackgroundTwo.scale = 2.0f;

            mBackgroundThree = new Background();
            mBackgroundThree.scale = 2.0f;

            mBackgroundFour = new Background();
            mBackgroundFour.scale = 2.0f;

            mBackgroundFive = new Background();
            mBackgroundFive.scale = 2.0f;
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
            mBackgroundOne.LoadContent(this.Content, "TileSets/SkyBackground");
            mBackgroundOne.Position = startPos1;

            mBackgroundTwo.LoadContent(this.Content, "TileSets/Background02");
            mBackgroundTwo.Position = startPos2;

            mBackgroundThree.LoadContent(this.Content, "TileSets/Background03");
            mBackgroundThree.Position = startPos3;

            mBackgroundFour.LoadContent(this.Content, "TileSets/Background04");
            mBackgroundFour.Position = startPos4;

            mBackgroundFive.LoadContent(this.Content, "TileSets/Background05");
            mBackgroundFive.Position = startPos5;
            ScreenManager.Instance.LoadContent(Content);
         
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (mBackgroundOne.Position.X < -mBackgroundOne.size.Width)
            {
                mBackgroundOne.Position.X = mBackgroundOne.Position.X + mBackgroundOne.size.Width;
            }

            if (mBackgroundTwo.Position.X < -mBackgroundTwo.size.Width)
            {
                mBackgroundTwo.Position.X = mBackgroundOne.Position.X + mBackgroundOne.size.Width;
            }

            if (mBackgroundThree.Position.X < -mBackgroundThree.size.Width)
            {
                mBackgroundThree.Position.X = mBackgroundTwo.Position.X + mBackgroundTwo.size.Width;
            }

            if (mBackgroundFour.Position.X < -mBackgroundFour.size.Width)
            {
                mBackgroundFour.Position.X = mBackgroundThree.Position.X + mBackgroundThree.size.Width;
            }

            if (mBackgroundFive.Position.X < -mBackgroundFive.size.Width)
            {
                mBackgroundFive.Position.X = mBackgroundFour.Position.X + mBackgroundFour.size.Width;
            }

            Vector2 aDirection = new Vector2(-1, 0);
            Vector2 aSpeed = new Vector2(0, 0);
            ScreenManager.Instance.Update(gameTime);
            mBackgroundOne.Position = startPos1 + (aDirection * Camera.Instance.Position * (float)0.2);
            mBackgroundTwo.Position = startPos2 + (aDirection * Camera.Instance.Position * (float)0.4);
            mBackgroundThree.Position = startPos3 + (aDirection * Camera.Instance.Position * (float)0.6);
            mBackgroundFour.Position = startPos4 + (aDirection * Camera.Instance.Position * (float)0.7);
            mBackgroundFive.Position = startPos5 + (aDirection * Camera.Instance.Position * (float)0.9);            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.Instance.ViewMatrix);
            mBackgroundOne.Draw(this.spriteBatch);
            mBackgroundTwo.Draw(this.spriteBatch);
            mBackgroundThree.Draw(this.spriteBatch);
            mBackgroundFour.Draw(this.spriteBatch);
            mBackgroundFive.Draw(this.spriteBatch);
            ScreenManager.Instance.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
