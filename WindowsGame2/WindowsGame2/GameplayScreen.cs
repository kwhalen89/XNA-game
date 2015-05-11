using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    public class GameplayScreen : GameScreen
    {
        EntityManager player, enemies;
        Map map;
        Song illusions;
        bool songStart = false;

        public override void LoadContent(ContentManager content, InputManager input)
        {
            base.LoadContent(content, input);
            player = new EntityManager();
            enemies = new EntityManager();
            map = new Map();
            map.LoadContent(content, map, "Map1");
            player.LoadContent("Player", content, "Load/Player.k", "", input);
            enemies.LoadContent("Enemy", content, "Load/Enemies.k", "Level1", input);
            illusions = content.Load<Song>("Audio/Illusions");
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            map.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();
            player.Update(gameTime, map);
            enemies.Update(gameTime, map);
            //entities update first intentionally, important for player activating map changes
            map.Update(gameTime);
            if(!songStart)
            {
                MediaPlayer.Play(illusions);
                songStart = true;
            }
            //creates ref for updates below
            Entity e;

            for(int i = 0; i < player.Entities.Count; i++)
            { 
                //updates all players
                e = player.Entities[i];
                map.UpdateCollision(ref e);
                player.Entities[i] = e;
            }

            for(int i = 0; i < enemies.Entities.Count; i++)
            {
                //updates enemies same way
                e = enemies.Entities[i];
                map.UpdateCollision(ref e);
                enemies.Entities[i] = e;
            }
            
            player.EntityCollision(enemies);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            map.Draw(spriteBatch);
            //map needs to be drawn before player now due to player needing to be over top of map
            player.Draw(spriteBatch);
            enemies.Draw(spriteBatch);
        }
    }
}
