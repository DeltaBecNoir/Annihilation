﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Annihilation.Projectiles
{
    class BlackoutShot : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 18;
            projectile.damage = 16;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = false;
            projectile.tileCollide = true;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(5) == 0)
            {
                target.AddBuff(BuffID.OnFire, 90);
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector12 = new Vector2((float)(Main.projectileTexture[projectile.type].Width / 2), (float)(Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] / 2));
            Main.spriteBatch.Draw(mod.GetTexture("NPCs/Megnatar/Firelaser_Glow"), projectile.position, null, new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha), projectile.rotation, vector12, projectile.scale, spriteEffects, 0f);
        }
    }
}
