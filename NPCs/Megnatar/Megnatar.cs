﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Annihilation.NPCs.Megnatar
{
    [AutoloadBossHead]
    class Megnatar : ModNPC
    {
        private float MoveCool
        {
            get => npc.ai[0];
            set => npc.ai[0] = value;
        }
        private int moveTimer = 300;
        private int moveTime = 60;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Megnatar, Chaos Bringer");
            Main.npcFrameCount[npc.type] = 12;
        }
        public override void SetDefaults()
        {
            npc.width = 142;
            npc.height = 128;
            npc.aiStyle = -1;
            npc.damage = 26;
            npc.defense = 15;
            npc.lifeMax = 3000;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit42;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.Confused] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Megnatar");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }
        private int TheUltimateMemeBec = 5;
        public override void FindFrame(int frameHeight)
        {
            TheUltimateMemeBec--;
            if (TheUltimateMemeBec == 0)
            {
                npc.frame.Y += 128;
                if (npc.frame.Y == 1536)
                {
                    npc.frame.Y = 0;
                }
                TheUltimateMemeBec = 5;
            }
        }

        // = ---------
        // Glowmask
        // = ---------
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector12 = new Vector2((float)(Main.npcTexture[npc.type].Width / 2), (float)(Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2));
            Main.spriteBatch.Draw(mod.GetTexture("NPCs/Megnatar/Megnatar_Glow"), npc.Bottom - Main.screenPosition + new Vector2((0f - (float)Main.npcTexture[npc.type].Width) * npc.scale / 2f + vector12.X * npc.scale, (0f - (float)Main.npcTexture[npc.type].Height) * npc.scale / (float)Main.npcFrameCount[npc.type] + 4f + vector12.Y * npc.scale + 0f + npc.gfxOffY), npc.frame, new Microsoft.Xna.Framework.Color(255 - npc.alpha, 255 - npc.alpha, 255 - npc.alpha, 255 - npc.alpha), npc.rotation, vector12, npc.scale, spriteEffects, 0f);
        }
        private int Firelaser = 0;
        private int Teleports = 0;
        private bool DEARLORDFREAKOUTREMOVER = false; //This can break the code... Do not edit.
        private bool BulletHell = false; //Change to true; for a hard fight.
        private bool BulletHellHealthSet = false; //Once again... Do not edit.
        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Player player = Main.player[npc.target];
                if (!player.active || player.dead || Main.dayTime)
                {
                    npc.TargetClosest(false);
                    player = Main.player[npc.target];
                    if (!player.active || player.dead)
                    {
                        npc.velocity = new Vector2(0f, 10f);
                        if (npc.timeLeft > 10)
                        {
                            npc.timeLeft = 10;
                        }
                        return;
                    }
                }
                if (BulletHell)
                {
                    npc.lifeMax = 65000;
                    npc.damage = 160;
                    npc.defense = 32;
                    if (!BulletHellHealthSet)
                    {
                        npc.life = npc.lifeMax;
                        BulletHellHealthSet = true;
                    }
                }
                if (npc.life >= npc.lifeMax / 2)
                {
                    npc.localAI[0] = 0f;
                }
                else if (npc.life < npc.lifeMax / 2 && !DEARLORDFREAKOUTREMOVER)
                {
                    npc.localAI[0] = 1f;
                    NPC.NewNPC((int)npc.position.X - 100, (int)npc.position.Y, ModContent.NPCType<Chaosblade1>());
                    NPC.NewNPC((int)npc.position.X + 100, (int)npc.position.Y, ModContent.NPCType<Chaosblade2>());
                    DEARLORDFREAKOUTREMOVER = true;
                }
                if (Firelaser > 0)
                {
                    Firelaser--;
                    if (Firelaser == 18 && npc.localAI[0] == 0f)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X + 50f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X - 50f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                    }
                    if (Firelaser == 12 && npc.localAI[0] == 0f)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X + 50f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X - 50f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                    }
                    if (Firelaser == 6 && npc.localAI[0] == 0f)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X + 50f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X - 50f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                    }
                    if (Firelaser == 23 && npc.localAI[0] == 1f)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X + 100f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X + 50f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X - 50f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X - 100f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                    }
                    if (Firelaser == 17 && npc.localAI[0] == 1f)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X + 100f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X + 50f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X - 50f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X - 100f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                    }
                    if (Firelaser == 11 && npc.localAI[0] == 1f)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X + 100f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X + 50f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X - 50f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(npc.Center, new Vector2(player.Center.X - (npc.Center.X - 100f), player.Center.Y - npc.Center.Y) / 50f, ModContent.ProjectileType<Firelaser>(), npc.damage / 3, 1);
                    }
                }
                MoveCool -= 1f;
                if (Main.netMode != NetmodeID.MultiplayerClient && MoveCool <= 0f)
                {
                    npc.TargetClosest(false);
                    player = Main.player[npc.target];
                    MoveCool = (float)moveTime + (float)Main.rand.Next(50);
                    npc.velocity.X = (player.Center.X - (float)(Main.rand.Next(-100, 100)) - npc.Center.X) / MoveCool;
                    npc.velocity.Y = (player.Center.Y - (float)(Main.rand.Next(290, 310)) - npc.Center.Y) / MoveCool;
                    npc.netUpdate = true;
                }
                if ((!BulletHell && Main.rand.Next(150) == 0) || (BulletHell && Main.rand.Next(15) == 0))
                {
                    Projectile.NewProjectile(new Vector2(npc.Center.X + 40f, npc.Center.Y + 40f), new Vector2(10f, 10f), ModContent.ProjectileType<Darkflame>(), npc.damage / 3, 1);
                    Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y + 40f), new Vector2(0, 10f), ModContent.ProjectileType<Darkflame>(), npc.damage / 3, 1);
                    Projectile.NewProjectile(new Vector2(npc.Center.X - 40f, npc.Center.Y + 40f), new Vector2(-10f, 10f), ModContent.ProjectileType<Darkflame>(), npc.damage / 3, 1);
                    Projectile.NewProjectile(new Vector2(npc.Center.X - 40f, npc.Center.Y), new Vector2(-10f, 0), ModContent.ProjectileType<Darkflame>(), npc.damage / 3, 1);
                    Projectile.NewProjectile(new Vector2(npc.Center.X - 40f, npc.Center.Y - 40f), new Vector2(-10f, -10f), ModContent.ProjectileType<Darkflame>(), npc.damage / 3, 1);
                    Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 40f), new Vector2(0, -10f), ModContent.ProjectileType<Darkflame>(), npc.damage / 3, 1);
                    Projectile.NewProjectile(new Vector2(npc.Center.X + 40f, npc.Center.Y - 40f), new Vector2(10f, -10f), ModContent.ProjectileType<Darkflame>(), npc.damage / 3, 1);
                    Projectile.NewProjectile(new Vector2(npc.Center.X + 40f, npc.Center.Y), new Vector2(10f, 0), ModContent.ProjectileType<Darkflame>(), npc.damage / 3, 1);
                    npc.position.X = player.Center.X - (float)(Main.rand.Next(-100, 100)) - 64;
                    npc.position.Y = player.Center.Y - (float)(Main.rand.Next(290, 310)) - 71;
                    Main.PlaySound(SoundID.Item6, npc.Center);
                    Firelaser = 24;
                    if (npc.localAI[0] == 1f)
                    {
                        Teleports++;
                    }
                }
                if (npc.localAI[0] == 1f)
                {
                    if ((!BulletHell && Main.rand.Next(150) == 0) || (BulletHell && Main.rand.Next(15) == 0))
                    {
                        Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 20f), new Vector2((float)(Main.rand.Next(-10, 10)), -10f), ModContent.ProjectileType<Darkflame2>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 20f), new Vector2((float)(Main.rand.Next(-10, 10)), -10f), ModContent.ProjectileType<Darkflame2>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 20f), new Vector2((float)(Main.rand.Next(-10, 10)), -10f), ModContent.ProjectileType<Darkflame2>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 20f), new Vector2((float)(Main.rand.Next(-10, 10)), -10f), ModContent.ProjectileType<Darkflame2>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 20f), new Vector2((float)(Main.rand.Next(-10, 10)), -10f), ModContent.ProjectileType<Darkflame2>(), npc.damage / 3, 1);
                        Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 20f), new Vector2((float)(Main.rand.Next(-10, 10)), -10f), ModContent.ProjectileType<Darkflame2>(), npc.damage / 3, 1);
                        if (Main.rand.Next(1) == 0)
                        {
                            Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 20f), new Vector2((float)(Main.rand.Next(-10, 10)), -10f), ModContent.ProjectileType<Darkflame>(), npc.damage / 3, 1);
                            if (Main.rand.Next(1) == 0)
                            {
                                Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 20f), new Vector2((float)(Main.rand.Next(-10, 10)), -10f), ModContent.ProjectileType<Darkflame>(), npc.damage / 3, 1);
                            }
                        }
                    }
                    if (Teleports >= 2)
                    {
                        Teleports = 0;
                        npc.velocity.X = (player.Center.X - npc.Center.X) / 40f;
                        npc.velocity.Y = (player.Center.Y - npc.Center.Y) / 40f;
                    }
                }
                if (Vector2.Distance(Main.player[npc.target].position, npc.position) > 150f)
                {
                    if (Vector2.Distance(Main.player[npc.target].position, npc.position) > 2400f)
                    {
                        if (npc.timeLeft > 10)
                        {
                            npc.timeLeft = 10;
                        }
                    }
                    moveTimer--;
                }
                else
                {
                    moveTimer += 10;
                    if (moveTime >= 150 && moveTimer > 60)
                    {
                        moveTimer = 60;
                    }
                }
                if (moveTimer <= 0)
                {
                    moveTimer += 60;
                    moveTime -= 10;
                    if (moveTime < 50)
                    {
                        moveTime = 50;
                        moveTimer = 0;
                    }
                    npc.netUpdate = true;
                }
                else if (moveTimer > 60)
                {
                    moveTimer -= 60;
                    moveTime += 10;
                    npc.netUpdate = true;
                }
            }
        }
    }
}
