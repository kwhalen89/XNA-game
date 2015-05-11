using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame2
{
    public class SplashScreen : GameScreen
    {
        SpriteFont font;
        List<Animation> animation;
        List<Texture2D> images;
        FileManager fileManager;
        int imageNumber;
        FadeAnimation fAnimation;

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("Font1");
            imageNumber = 0;
            fileManager = new FileManager();
            animation = new List<Animation>();
            fAnimation = new FadeAnimation();
            images = new List<Texture2D>();
            
            fileManager.LoadContent("Load/Splash.k", attributes, contents);

            for(int i = 0; i < attributes.Count; i++)
            {
                for(int j = 0; j < attributes[i].Count; j++)
                {
                    switch(attributes[i][j])
                    {
                        case "Image":
                            images.Add(this.content.Load<Texture2D>(contents[i][j]));
                            animation.Add(new FadeAnimation());
                            break;
                    }
                }

            }

            for (int i = 0; i < animation.Count; i++)
            {
                //imageWidth / 2 * scale - imageWidth rescaled screen so now no scaling is needed
                //imageHeight / 2 * scale - imageHeight
                animation[i].LoadContent(content, images[i], "", new Vector2(0, 0));
                animation[i].Scale = 1.25f;
                animation[i].IsActive = true;
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            fileManager = null;
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();
            if (imageNumber < animation.Count)
            {
                Animation a = animation[imageNumber];
                fAnimation.Update(gameTime, ref a);
                animation[imageNumber] = a;
                if (animation[imageNumber].Alpha == 0.0f)
                        imageNumber++;
            }
                if (imageNumber >= animation.Count - 1 || inputManager.KeyPressed(Keys.Z))
                {
                    ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager);
                }           
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(imageNumber < animation.Count)
                animation[imageNumber].Draw(spriteBatch);
        }
    }
}
