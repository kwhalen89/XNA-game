using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    public class Map
    {
        public Layer layer1, layer2;
        public Collision collision;
        string id;

        public string ID
        {
            get { return id; }
        }

        public void LoadContent(ContentManager content, Map map, string mapID)
        {
            layer1 = new Layer();
            collision = new Collision();
            id = mapID;
            layer1.LoadContent(map, "Layer1");
            //layer2.LoadContent(map, "Layer2");
            collision.LoadContent(content, mapID);
        }

        public void UnloadContent()
        {
            //layer.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            layer1.Update(gameTime);
            //layer2.Update(gameTime);
        }

        public void UpdateCollision(ref Entity e)
        {
            layer1.UpdateCollision(ref e);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //layer2.Draw(spriteBatch);
            layer1.Draw(spriteBatch);
        }
    }
}
