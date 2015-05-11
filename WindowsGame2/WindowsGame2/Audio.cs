using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame2
{
    public class Audio
    {
        SoundEffect walking;
        SoundEffect realPunch;
        SoundEffect strongPunch;
        SoundEffect neckSnap;
        SoundEffect jump;
        static int effectCue;
        SoundEffectInstance walkingInstance;

        public int EffectCue
        {
            get { return effectCue; }
            set { effectCue = value; }
        }
        public SoundEffect Walking
        {
            get { return walking; }
        }

        public SoundEffect RealPunch
        {
            get { return realPunch; }
        }

        public SoundEffect StrongPunch
        {
            get { return strongPunch; }
        }

        public SoundEffect NeckSnap
        {
            get { return neckSnap; }
        }

        public SoundEffect Jump
        {
            get { return jump; }
        }

        public SoundEffectInstance WalkingInstance
        {
            get { return walkingInstance; }
        }

        public void LoadContent(ContentManager Content)
        {
            effectCue = 0;
            walking = Content.Load<SoundEffect>("Audio/walking");
            walkingInstance = walking.CreateInstance();
            realPunch = Content.Load<SoundEffect>("Audio/realPunch");
            strongPunch = Content.Load<SoundEffect>("Audio/strongPunch");
            jump = Content.Load<SoundEffect>("Audio/jump");
            neckSnap = Content.Load<SoundEffect>("Audio/neckSnap");
        }
    }
}
