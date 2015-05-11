using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    public class FadeAnimation : Animation
    {
        bool increase;
        bool stopUpdating;
        TimeSpan defaultTime;
        TimeSpan timer;
        float fadeSpeed;
        float activateValue;
        float defaultAlpha;

        public TimeSpan Timer
        {
            get { return timer; }
            set { defaultTime = value; timer = defaultTime; }
        }

        public float FadeSpeed
        {
            get { return fadeSpeed; }
            set { fadeSpeed = value; }
        }

        public float ActivateValue
        {
            get { return activateValue; }
            set { activateValue = value; }
        }

        public bool Increase
        {
            get { return increase; }
            set { increase = value; }
        }

        public float DefaultAlpha
        {
            set { defaultAlpha = value; }
        }

        public FadeAnimation()
        {
            increase = false;
            fadeSpeed = 1.0f;
            defaultTime = new TimeSpan(0, 0, 1);
            timer = defaultTime;
            activateValue = 0.0f;
            stopUpdating = false;
            defaultAlpha = 1.0f;
        }

        public override void Update(GameTime gameTime, ref Animation a)
        {
            if (a.IsActive)
            {
                if (!stopUpdating)
                {
                    if (!increase)
                        a.Alpha -= fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else
                        a.Alpha += fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (a.Alpha <= 0.0f)
                    {
                        a.Alpha = 0.0f;
                        increase = !increase;
                    }
                    else if (a.Alpha >= 1.0f)
                    {
                        a.Alpha = 1.0f;
                        increase = false;
                    } 
               }

                if (a.Alpha == activateValue)
                {
                    stopUpdating = true;
                    timer -= gameTime.ElapsedGameTime;
                    if (timer.TotalSeconds <= 0)
                    {
                        timer = defaultTime;
                        stopUpdating = false;

                    }
                }
            }
            else
            {
                a.Alpha = defaultAlpha;
                stopUpdating = false;
            }
        }
    }
}
