using Annihilation.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Creative;

namespace Annihilation.Items.Melee
{
	public class DarkflameSickle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Darkflame Sickle");
			Tooltip.SetDefault("Cut enemies with flaming waves");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 17;
			Item.DamageType = DamageClass.Melee;
			Item.width = 50;
			Item.height = 44;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 11000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item71;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<DarkflameWave>();
			Item.shootSpeed = 30f;
		}
	}
}
	