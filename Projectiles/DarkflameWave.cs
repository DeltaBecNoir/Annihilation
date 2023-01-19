using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Annihilation.Projectiles
{
    class DarkflameWave : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.damage = 16;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 200;
            Projectile.penetrate = 2;
            Projectile.knockBack = 1;
        }
        public override void AI()
        {
            Projectile.rotation += 0.9f * (float)Projectile.direction;
            Projectile.velocity = Projectile.velocity * 0.9f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(5) == 0)
            {
                target.AddBuff(BuffID.OnFire, 90);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 200, new Color(255, 255, 255), 2f);
                Main.dust[dust].velocity = Main.rand.NextVector2Unit() * 0.5f;
                Main.dust[dust].noGravity = true;
            }
	SoundEngine.PlaySound(SoundID.Item20, Projectile.Center);
        }

        public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, 255);

    }
}
