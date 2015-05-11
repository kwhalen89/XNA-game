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
        float jumpSpeed = 1500f;
        Audio sound;

        public override void LoadContent(ContentManager content, List<string> attributes, List<string> contents, InputManager input)
        {
 	        base.LoadContent(content, attributes, contents, input);
            sound = new Audio();
            sound.LoadContent(content);
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
            
            if (input.KeyDown(Keys.Right, Keys.D))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 0); //moves to row 0 the row looking right on sprite sheet
                velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                if(sound.WalkingInstance.State == SoundState.Playing)
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

            position += velocity;
            moveAnimation.Position = position;
            ssAnimation.Update(gameTime, ref moveAnimation);
            Camera.Instance.SetFocalPoint(new Vector2(position.X, ScreenManager.Instance.Dimensions.Y / 2));
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
                velocity.Y = -velocity.Y;
                position.Y += velocity.Y;
                position.X -= velocity.X * 5;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            moveAnimation.Draw(spriteBatch);
        }
    }
}
