using Annihilation.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Annihilation.Items.Ranged
{
    public class CrystalSpiker : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Spiker");
            Tooltip.SetDefault("Better then your average Spiker");
        }
        public override void SetDefaults()
        {
            item.damage = 23;
            item.ranged = true;
            item.width = 36;
            item.height = 42;
            item.useTime = 14;
            item.useAnimation = 14;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<CrystalBullet>();
            item.shootSpeed = 10f;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= player.bulletDamage;
        }
    }
}