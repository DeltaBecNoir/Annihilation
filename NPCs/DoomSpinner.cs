using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Annihilation.NPCs
{
    public class DoomSpinner : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Spinner");
        }

        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 48;
            NPC.damage = 25;
            NPC.lifeMax = 80;
            NPC.defense = 11;
            NPC.HitSound = SoundID.NPCHit42;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = Item.buyPrice(0, 0, 1, 5);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 2;
            NPC.noGravity = true;
            NPC.boss = false;
            NPC.netAlways = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //3hi31mg
            var clr = new Color(255, 255, 255, 255); // full white
            var drawPos = NPC.Center - Main.screenPosition;
            var origTexture = Main.NPCTexture[NPC.type];
            var texture = Mod.GetTexture("NPCs/DoomSpinner_Glow");
            var eyetexture = Mod.GetTexture("NPCs/DoomSpinner_Eye");
            var orig = NPC.frame.Size() / 2f;

            Main.spriteBatch.Draw(origTexture, drawPos, NPC.frame, lightColor, NPC.rotation, orig, NPC.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, drawPos, NPC.frame, clr, NPC.rotation, orig, NPC.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(eyetexture, drawPos, NPC.frame, clr, 0, orig, NPC.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedBoss1)
            {
                return SpawnCondition.OverworldNight.Chance * 0.05f;
            }
            return 0;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            base.HitEffect(hitDirection, damage);
            if (NPC.life <= 0)
            {
                for (int i = 0; i < Main.rand.Next(5, 8); i++)
                {
                    var gore = Terraria.Main.gore[Gore.NewGore(NPC.Center, Vector2.Zero, 61, 1)];
                }

                for (int i = 0; i < Main.rand.Next(7, 10); i++)
                {
                        var dust = Terraria.Main.dust[Dust.NewDust(NPC.position + Utils.NextVector2Square(Main.rand, -1, 1) * NPC.frame.Size(), NPC.frame.Width, NPC.frame.Height, DustID.Torch, 0, 0, 0, default, 2)];
                }
            }
        }

        public void Aim()
        {
            NPC.rotation += NPC.velocity.Length() * 0.11f;
        }

        public enum State
        {
            Default,
            Dash,
            Return,
            Bounce
        };
        State pstate = State.Default, lastState = State.Default;
        State state
        {
            get => pstate;
            set
            {
                lastState = pstate;
                pstate = value;
            }
        }

        public override void AI()
        {
            Aim();
            NPC.TargetClosest();
            var target = Main.player[NPC.target];

            switch (state)
            {
                case State.Default:
                    NPC.velocity = Vector2.Lerp(NPC.velocity, -(NPC.Center - target.Center).SafeNormalize(Vector2.UnitX) * 2, 0.05f);
                    if (Vector2.Distance(NPC.Center, target.Center) < 128)
                    {
                        NPC.velocity = -(NPC.Center - target.Center).SafeNormalize(Vector2.UnitX) * 6;
                        NPC.oldVelocity = -(NPC.Center - target.Center).SafeNormalize(Vector2.UnitX) * 6;
                        state = State.Dash;
                    }
                    break;
                case State.Dash:
                    NPC.velocity = NPC.oldVelocity;
                    if (Vector2.Distance(NPC.Center, target.Center) > 128)
                    {
                        state = State.Return;
                    }
                    break;
                case State.Return:
                    NPC.velocity = Vector2.Lerp(NPC.velocity, (NPC.Center - target.Center).SafeNormalize(Vector2.UnitX) * 2, 0.05f);
                    if (Vector2.Distance(NPC.Center, target.Center) > 256)
                    {
                        state = State.Default;
                    }
                    break;
                case State.Bounce:
                    NPC.velocity = -NPC.velocity;
                    if (NPC.frameCounter++ >= 30)
                    {
                        NPC.frameCounter = 0;
                        state = lastState;
                    }
                    break;
            }

            Dust dust;
            dust = Main.dust[Terraria.Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, 0f, 0f, 0, new Color(255, 255, 255), 3f)];
            dust.noGravity = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 150);
        }
    }
}