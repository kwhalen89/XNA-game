using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    public class ScreenManager
    {
        #region Variables

        ContentManager content;
        GameScreen currentScreen;
        GameScreen newScreen;
        private static ScreenManager instance;
        Stack<GameScreen> screenStack = new Stack<GameScreen>();
        Vector2 dimensions;
        bool transition;
        Animation animation = new Animation();
        FadeAnimation fade = new FadeAnimation();
        Texture2D fadeTexture, nullImage;
        InputManager inputManager;

        #endregion

        #region Properties

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        public ContentManager Content
        {
            get { return content; }
        }

        public Vector2 Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        public Texture2D NullImage
        {
            get { return nullImage; }
        }
        
        #endregion

        #region Main Methods

        public void AddScreen(GameScreen screen, InputManager inputManager)
        {
            transition = true;
            newScreen = screen;
            fade.IsActive = true;
            fade.Alpha = 0.0f;
            fade.Increase = true;
            fade.ActivateValue = 1.0f;
            this.inputManager = inputManager;
        }
        public void AddScreen(GameScreen screen, InputManager inputManager, float alpha)
        {
            transition = true;
            newScreen = screen;
            fade.IsActive = true;
            fade.Alpha = alpha;
            fade.Increase = true;
            fade.ActivateValue = 1.0f;
            this.inputManager = inputManager;
        }
        public void Initialize() 
        {
            currentScreen = new GameplayScreen();
            fade = new FadeAnimation();
            inputManager = new InputManager();
        }
        public void LoadContent(ContentManager Content) 
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(Content, inputManager);

            nullImage = this.content.Load<Texture2D>("null");
            fadeTexture = this.content.Load<Texture2D>("fade");
            animation.LoadContent(content, fadeTexture, "", Vector2.Zero);
            animation.Scale = dimensions.X;
        }
        public void Update(GameTime gameTime) 
        {
            if (!transition)
                currentScreen.Update(gameTime);
            else
                Transition(gameTime);

            Camera.Instance.Update();
        }
        public void Draw(SpriteBatch spriteBatch) 
        {
            currentScreen.Draw(spriteBatch);
            if (transition)
               animation.Draw(spriteBatch);
        }

        #endregion

        #region Private Methods

        private void Transition(GameTime gameTime)
        {
            fade.Update(gameTime, ref animation);
            if (fade.Alpha == 1.0f && fade.Timer.TotalSeconds == 1.0f)
            {
                screenStack.Push(newScreen);
                currentScreen.UnloadContent();
                currentScreen = newScreen;
                currentScreen.LoadContent(content, this.inputManager); 
            }
            else if (fade.Alpha == 0.0f)
            {
                transition = false;
                fade.IsActive = false;
            }
        }
        #endregion
    }
}
