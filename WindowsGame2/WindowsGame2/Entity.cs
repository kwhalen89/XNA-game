using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    public class Entity
    {
        protected int health;
        protected Animation moveAnimation;
        protected SpriteSheetAnimation ssAnimation;
        protected float moveSpeed;
        protected ContentManager content;
        protected Texture2D image;
        protected List<List<string>> attributes, contents;
        protected Vector2 position;
        protected float gravity;
        protected Vector2 velocity;
        protected Vector2 prevPosition;
        protected bool activateGravity;
        protected bool syncTilePostion;
        protected bool onTile;
        protected int range;
        protected int direction;
        protected Vector2 destPosition, origPosition;

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Direction
        {
            get { return direction; }
            set 
            { 
                direction = value;
                destPosition.X = (direction == 2) ? destPosition.X = origPosition.X - range :
                    destPosition.X = origPosition.X + range;
            }
        }

        public bool OnTile
        {
            get { return onTile; }
            set { onTile = value; }
        }

        public FloatRect Rect
        {
            get { return new FloatRect(position.X, position.Y, moveAnimation.FrameWidth, moveAnimation.FrameHeight); }
        }

        public Vector2 PrevPosition
        {
            get { return prevPosition;}
        }

        public Animation Animation
        {
            get { return moveAnimation; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool ActivateGravity
        {
            set { activateGravity = value; }
        }

        public bool SyncTilePosition
        {
            get { return syncTilePostion; }
            set { syncTilePostion = value; }
        }

        public virtual void LoadContent(ContentManager content, List<string> attributes, List<string> contents, InputManager input)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
            moveAnimation = new Animation();
            ssAnimation = new SpriteSheetAnimation();
            for (int i = 0; i < attributes.Count; i++)
            {
                switch (attributes[i])
                {
                    case "Health":
                        health = int.Parse(contents[i]);
                        break;
                    case "Frames":
                        string[] frames = contents[i].Split(',');
                        moveAnimation.Frames = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
                        break;
                    case "Image":
                        image = this.content.Load<Texture2D>(contents[i]);
                        break;
                    case "Position":
                        frames = contents[i].Split(',');
                        position = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
                        break;
                    case "MoveSpeed":
                        moveSpeed = float.Parse(contents[i]);
                        break;
                    case "Range":
                        range = int.Parse(contents[i]);
                        break;
                }
            }

            gravity = 100f;
            velocity = Vector2.Zero;
            syncTilePostion = false;
            activateGravity = true;
            moveAnimation.LoadContent(content, image, "", position);
        }

        public virtual void UnloadContent()
        {
            content.Unload();
            moveAnimation = null;
            gravity = 0.0f;
            syncTilePostion = false;
            activateGravity = false;
            velocity = Vector2.Zero;
            ssAnimation = null;
        }

        public virtual void Update(GameTime gameTime, InputManager input, Collision col, Layer layer)
        {
            syncTilePostion = false;
            prevPosition = position;
        }

        public virtual void OnCollision(Entity e)
        {
        }

        public virtual void BulletCollision(Entity e)
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        { 
        
        }
    }
}
