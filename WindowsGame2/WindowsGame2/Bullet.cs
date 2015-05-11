using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    public class Bullet
    {
        float bulletSpeed = 400f;
        float bulletRange = 500f;
        List<Vector2> bullets1, bullets2;
        Texture2D bulletImage;

        public void LoadContent(ContentManager Content)
        {
            bulletImage = Content.Load<Texture2D>("Bullet");
            bullets1 = new List<Vector2>();
            bullets2 = new List<Vector2>();
        }

        public void AddBullet(Player player)
        {
            if (player.Direction == 0)
                bullets1.Add(player.Position);
            else
                bullets2.Add(player.Position);
        }
        public void UpdateBullet()
        {

        }
    }
}
