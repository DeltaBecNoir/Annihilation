using System;
using Annihilation.Items.Materials;
//using Annihilation.Items.Materials;
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
    public class ChaosEmissary : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Emissary");
            Main.npcFrameCount[NPC.type] = 3;
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
            NPC.knockBackResist = 0.2f;
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
            var drawPos = NPC.Center - Main.screenPosition;
            var origTexture = TextureAssets.Npc[NPC.type].Value;
            var texture = ModContent.Request<Texture2D>("Annihilation/NPCs/ChaosEmissary_Glow").Value;           
            var orig = NPC.frame.Size() / 2f;

            Main.spriteBatch.Draw(origTexture, drawPos, NPC.frame, lightColor, NPC.rotation, orig, NPC.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, drawPos, NPC.frame, Color.White, NPC.rotation, orig, NPC.scale, SpriteEffects.None, 0f);
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

        int ToInt(bool inp) => inp ? 1 : -1;
        const float frameInterval = 10;
        public void Animate()
        {
            var frames = Main.npcFrameCount[NPC.type];
            NPC.frameCounter += 1/frameInterval;
            int finalFrame = (int)(NPC.frameCounter % frames);
            NPC.frame.Y = finalFrame * NPC.height;
            
            // dust
            Dust dust;
            for (int i = 0; i < 4; i++)
            {
                if (Main.rand.NextFloat() < .25f)
                {
                    dust = Dust.NewDustPerfect(NPC.Center + new Vector2(-16, 0).RotatedBy(NPC.velocity.ToRotation()) + new Vector2(0, 4) + Utils.NextVector2Unit(Main.rand) * new Vector2(8, 16), DustID.Torch, null, 0, default, 2.5f);
                    dust.noGravity = true;
                }
            }
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
            NPC.spriteDirection = ToInt(NPC.velocity.X > 0);
            NPC.rotation = NPC.velocity.ToRotation() + MathHelper.Pi/2;
        }
        public override void AI()
        {
            Animate();
            Aim();
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