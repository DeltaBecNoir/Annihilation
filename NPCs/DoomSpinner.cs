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
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 2;
            NPC.noGravity = true;
            NPC.boss = false;
            NPC.netAlways = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 vector2, Color lightColor)
        {
            //3hi31mg
            var drawPos = NPC.Center - Main.screenPosition;
            var origTexture = TextureAssets.Npc[NPC.type].Value;
            var texture = ModContent.Request<Texture2D>("Annihilation/NPCs/DoomSpinner_Glow").Value; 
            var eyetexture = ModContent.Request<Texture2D>("Annihilation/NPCs/DoomSpinner_Eye").Value;
            var orig = NPC.frame.Size() / 2f;

            Main.spriteBatch.Draw(origTexture, drawPos, NPC.frame, lightColor, NPC.rotation, orig, NPC.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, drawPos, NPC.frame, Color.White, NPC.rotation, orig, NPC.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(eyetexture, drawPos, NPC.frame, Color.White, 0, orig, NPC.scale, SpriteEffects.None, 0f);
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
            if (NPC.life <= 0)
            {

                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(NPC.GetSource_OnHit(NPC), NPC.position, NPC.velocity, GoreID.Smoke1);
                    Gore.NewGore(NPC.GetSource_OnHit(NPC), NPC.position, NPC.velocity, GoreID.Smoke2);
                    Gore.NewGore(NPC.GetSource_OnHit(NPC), NPC.position, NPC.velocity, GoreID.Smoke3);
                }

                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position + NPC.velocity, NPC.frame.Width, NPC.frame.Height, DustID.Torch, NPC.velocity.X, NPC.velocity.Y, 0, default, 1.5f);
                    Dust.NewDust(NPC.position + NPC.velocity, NPC.frame.Width, NPC.frame.Height, DustID.Smoke, NPC.velocity.X, NPC.velocity.Y, 0, default, 1.5f);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(NPC.position + NPC.velocity, NPC.frame.Width, NPC.frame.Height, DustID.Torch, NPC.velocity.X, NPC.velocity.Y, 0, default, 1.5f);
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
            dust = Main.dust[Terraria.Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, 0f, 0f, 0, default, 2.5f)];
            dust.noGravity = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 150);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosFragment>(), 1, 3, 5));
        }
    }
}