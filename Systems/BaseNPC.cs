using System;
using Annihilation.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Annihilation.Systems
{
    public abstract class BaseNPC : ModNPC
    {
        protected Texture2D currentAnimation = null;
        protected Texture2D currentFrame = null;
        protected int currentFrameIndex = 0;
        protected int framesPerAnimation = 0;
        protected int frameRate = 0;
        protected int frameCounter = 0;
        protected string animationPath = "";

        protected State currentState; // current state

        public override string Texture => "Annihilation/Textures/Null";

        public override void SetDefaults()
        {
            InitializeState();
        }

        public virtual void InitializeState()
        {
        }

        public void ChangeState(State newState)
        {
            if (currentState != null)
            {
                currentState.OnStateExit();
            }

            currentState = newState; // update the current state
            newState?.OnStateEnter(); // call the enter method of the new state
        }

        public void ChangeState(State newState, Animation animation)
        {
            if (currentState != null)
            {
                currentState.OnStateExit(); // call the exit method of the current state
            }

            SetAnimation(animation);

            currentState = newState; // update the current state
            newState?.OnStateEnter(); // call the enter method of the new state
        }

        public override void AI()
        {
            currentState?.OnStateUpdate(); // call the update method of the current state
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 vector2, Color lightColor)
        {
            UpdateFrames();

            currentAnimation = ModContent.Request<Texture2D>(animationPath).Value;

            var drawPos = NPC.Center - Main.screenPosition;
            var orig = NPC.frame.Size() / 2f;

            // if the current frame is null and it attempts to draw, this can cause issues
            if (currentFrame != null)
            {
                Main.spriteBatch.Draw(currentFrame, drawPos, NPC.frame, lightColor, NPC.rotation, orig, NPC.scale, SpriteEffects.None, 0f);
            }

            return false;
        }

        public virtual void SetAnimation(Animation animation)
        {
            animationPath = (GetType().Namespace + "." + animation.Name).Replace('.', '/');
            currentAnimation = ModContent.Request<Texture2D>(animationPath).Value;
            framesPerAnimation = animation.Frames;
            frameRate = animation.FrameRate;
            currentFrameIndex = 0;
            frameCounter = 0;
        }

        protected void UpdateFrames()
        {
            if (currentAnimation != null && !Main.gamePaused)
            {
                frameCounter++;
                if (frameCounter >= frameRate)
                {
                    frameCounter = 0;
                    currentFrameIndex++;
                    if (currentFrameIndex >= framesPerAnimation)
                    {
                        currentFrameIndex = 0;
                    }
                }

                int frameWidth = currentAnimation.Width / framesPerAnimation;
                int frameHeight = currentAnimation.Height;
                int frameX = frameWidth * currentFrameIndex;
                int frameY = 0;

                if (frameX >= currentAnimation.Width)
                {
                    // handle the case where the frame index exceeds the texture width
                    frameX = 0;
                    frameY += frameHeight;
                }

                Rectangle frameRectangle = new Rectangle(frameX, frameY, frameWidth, frameHeight);

                if (frameRectangle.Width > 0 && frameRectangle.Height > 0)
                {
                    Color[] data = new Color[frameRectangle.Width * frameRectangle.Height];
                    currentAnimation.GetData(0, frameRectangle, data, 0, data.Length);
                    currentFrame = new Texture2D(Main.graphics.GraphicsDevice, frameRectangle.Width, frameRectangle.Height);
                    currentFrame.SetData(data);
                }
                else
                {
                    // handle the case where the frame dimensions are invalid
                    currentFrame = null;
                }
            }
        }
    }
}