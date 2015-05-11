using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame2
{
    public class Player : Entity
    {
        float jumpSpeed = 1500f, bulletSpeed = 400f;
        Audio sound;
        List<Vector2> bullets1, bullets2;
        Texture2D bulletImage;
        bool keyPressed;
        SpriteFont font;

        public Texture2D BulletImage
        {
            get { return bulletImage; }
        }
        public List<Vector2> Bullets1
        {
            get { return bullets1; }
        }

        public List<Vector2> Bullets2
        {
            get { return bullets2; }
        }

        public override void LoadContent(ContentManager content, List<string> attributes, List<string> contents, InputManager input)
        {
 	        base.LoadContent(content, attributes, contents, input);
            font = content.Load<SpriteFont>("Font1");
            sound = new Audio();
            sound.LoadContent(content);
            bullets1 = new List<Vector2>();
            bullets2 = new List<Vector2>();
            bulletImage = content.Load<Texture2D>("bullet");
            keyPressed = false;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            moveAnimation.UnloadContent();
        }

        public override void Update(GameTime gameTime, InputManager input, Collision col, Layer layer)
        {
            base.Update(gameTime, input, col, layer);
            moveAnimation.DrawColor = Color.White;
            moveAnimation.IsActive = true; //will be true and makes player move through row of animations as long as the the last else isn't true which turns animation back off
            if (health > 0)
            {
                if (input.KeyDown(Keys.Right, Keys.D))
                {
                    moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 0); //moves to row 0 the row looking right on sprite sheet
                    velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = 0;
                    if (sound.WalkingInstance.State == SoundState.Stopped)
                    {
                        sound.WalkingInstance.Volume = 0.75f;
                        sound.WalkingInstance.IsLooped = true;
                        sound.WalkingInstance.Play();
                    }
                    else
                        sound.WalkingInstance.Resume();

                }
                else if (input.KeyDown(Keys.Left, Keys.A))
                {
                    moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 1); //moves to row 1 the row looking left on sprite sheet
                    velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Direction = 1;
                    if (sound.WalkingInstance.State == SoundState.Stopped)
                    {
                        sound.WalkingInstance.Volume = 0.75f;
                        sound.WalkingInstance.IsLooped = true;
                        sound.WalkingInstance.Play();
                    }
                    else
                        sound.WalkingInstance.Resume();
                }
                else
                {
                    moveAnimation.IsActive = false; //if key isn't pressed turn off X axis scrolling on sprite sheet  
                    velocity.X = 0;
                    if (sound.WalkingInstance.State == SoundState.Playing)
                        sound.WalkingInstance.Pause();
                }
                if (input.KeyDown(Keys.Up, Keys.W) && !activateGravity)
                {
                    velocity.Y = -jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    activateGravity = true;
                    sound.Jump.Play();
                }
                if (activateGravity)
                    velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    velocity.Y = 0;

                if (input.KeyDown(Keys.R) && keyPressed == false) //if R is pressed add a bullet as long as key isn't already pressed
                {
                    if (Direction == 0)
                        bullets1.Add(position);
                    else
                        bullets2.Add(position);
                    keyPressed = true;
                }
                if (Keyboard.GetState().IsKeyUp(Keys.R)) //only way to fire again is to unpress key
                    keyPressed = false;

                for (int i = 0; i < bullets1.Count; i++)
                {
                    float x = bullets1[i].X;
                    x += bulletSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    bullets1[i] = new Vector2(x, bullets1[i].Y);
                }

                for (int i = 0; i < bullets2.Count; i++)
                {
                    float x = bullets2[i].X;
                    x -= bulletSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    bullets2[i] = new Vector2(x, bullets2[i].Y);
                }
                position += velocity;
                moveAnimation.Position = position;
                ssAnimation.Update(gameTime, ref moveAnimation);
                Camera.Instance.SetFocalPoint(new Vector2(position.X, ScreenManager.Instance.Dimensions.Y / 2));
            }
        }

        public override void BulletCollision(Entity e)
        {
            Type type = e.GetType();
            if (type == typeof(Enemy))
            {
                for (int i = 0; i < bullets1.Count; i++)
                {
                    if (bullets1[i].X > e.Rect.Left && bullets1[i].X < e.Rect.Right
                        && bullets1[i].Y > e.Rect.Top && bullets1[i].Y < e.Rect.Bottom)
                    {
                        bullets1.RemoveAt(i);
                        e.Animation.DrawColor = Color.Red;
                        e.Health--;
                    }
                }
                for (int i = 0; i < bullets2.Count; i++)
                {
                    if (bullets2[i].X > e.Rect.Left && bullets2[i].X < e.Rect.Right
                        && bullets2[i].Y > e.Rect.Top && bullets2[i].Y < e.Rect.Bottom)
                    {
                        bullets2.RemoveAt(i);
                        e.Animation.DrawColor = Color.Red;
                        e.Health--;
                    }
                }
            }
        }

        public override void OnCollision(Entity e)
        {
            Type type = e.GetType();
            if(type == typeof(Enemy))
            {
                    health--;
                    if (e.Direction == 1)
                        e.Direction = 2;
                    else if (e.Direction == 2)
                        e.Direction = 1;
                    if (sound.EffectCue == 0)
                    {
                        sound.RealPunch.Play();
                        sound.EffectCue++;
                    }
                    else if (sound.EffectCue == 1)
                    {
                        sound.StrongPunch.Play();
                        sound.EffectCue++;
                    }
                    else if (sound.EffectCue == 2)
                    {
                        sound.NeckSnap.Play();
                        sound.EffectCue = 0;
                    }
                    moveAnimation.DrawColor = Color.Red;
                }
                velocity.Y = -velocity.Y;
                position.Y += velocity.Y;
                position.X -= velocity.X * 5;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font,"" + health, new Vector2(5, 5) + Camera.Instance.Position, Color.Red);
            moveAnimation.Draw(spriteBatch);
            for(int i = 0; i < bullets1.Count; i++)
            {
                spriteBatch.Draw(bulletImage, bullets1[i], Color.White);
            }
            for (int i = 0; i < bullets2.Count; i++)
            {
                spriteBatch.Draw(bulletImage, bullets2[i], Color.White);
            }
        }
    }
}
