using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    public class Background
    {
        public Vector2 Position = new Vector2(0, 0);
        private Texture2D mSpriteTexture;
        public Rectangle size;
        public float scale = 2.0f;

        public void LoadContent(ContentManager Content, string theAssetName)
        {
            mSpriteTexture = Content.Load<Texture2D>(theAssetName);
            size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * scale), (int)(mSpriteTexture.Height * scale));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mSpriteTexture, Position,
                new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White,
                0.0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
