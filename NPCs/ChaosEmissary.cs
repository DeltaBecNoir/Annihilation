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
using Terraria.Audio;

namespace Annihilation.NPCs
{
    public class ChaosEmissary : ModNPC
    {
        private const float moveSpeed = 3f;
        private const float attackInterval = 3f; // in seconds
        private int attackTimer = 0;
        private float maxDistanceToPlayer = 800f;
        private float attackDistance = 50f;
        private float shootSpeed = 5f;
        private float dashSpeed = 6f;

        private float distanceToPlayer;

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
            NPC.frameCounter += 1 / frameInterval;
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
            NPC.rotation = NPC.velocity.ToRotation() + MathHelper.Pi / 2;
        }

        public override void AI()
        {
            Animate();
            Move();
            Aim();
            Attack();
        }

        private void Move()
        {
            Player player = Main.player[NPC.target];

            // calculate the direction towards the player
            int direction = NPC.Center.X < player.Center.X ? 1 : -1;
            distanceToPlayer = Math.Abs(NPC.Center.X - player.Center.X);

            // move towards the player if too far away
            if (distanceToPlayer > maxDistanceToPlayer)
            {
                NPC.velocity.X = moveSpeed * direction;
                NPC.spriteDirection = direction;
            }
            // move to the other side after shooting
            else if (attackTimer > (attackInterval * 60) / 2 && NPC.velocity.X == 0)
            {
                NPC.velocity.X = moveSpeed * -direction * 0.5f;
                NPC.spriteDirection = -direction;
            }
            // gradually decrease velocity when reaching the other side
            else if (Math.Abs(NPC.Center.X - player.Center.X) < 2f && NPC.velocity.X != 0)
            {
                NPC.velocity.X *= 0.9f;
                NPC.spriteDirection = -direction;
            }
        }

        private void Attack()
        {
            attackTimer++;
            if (attackTimer >= (attackInterval * 60))
            {
                if (Main.player[NPC.target].dead || distanceToPlayer > attackDistance)
                {
                    return;
                }

                SoundEngine.PlaySound(SoundID.Item45, NPC.Center);

                Player player = Main.player[NPC.target];
                Vector2 directionToPlayer = player.Center - NPC.Center;

                directionToPlayer.Normalize();
                // pointed towards the player
                NPC.velocity = directionToPlayer * dashSpeed;

                Vector2 direction = NPC.velocity;
                direction.Normalize();

                Vector2 projectileVelocity = direction * shootSpeed;

                // summons the flamethrower projectile and makes it hostile
                Projectile projectile = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, projectileVelocity, ProjectileID.Flames, NPC.damage, 0f, Main.myPlayer);
                projectile.friendly = false;
                projectile.hostile = true;
                projectile.damage = NPC.damage;

                // set projectile rotation to match NPC's rotation
                projectile.rotation = projectileVelocity.ToRotation();

                attackTimer = 0;
            }
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