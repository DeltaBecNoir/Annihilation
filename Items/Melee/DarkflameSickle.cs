using Annihilation.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent;

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
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			var drawPos = Item.Center - Main.screenPosition;
			var origTexture = TextureAssets.Item[Item.type].Value;
			var texture = ModContent.Request<Texture2D>("Annihilation/Items/Melee/DarkflameSickle_Glow").Value;
			var orig = texture.Size() / 2f;
			Rectangle frame;
			frame = texture.Frame();

			spriteBatch.Draw(origTexture, drawPos, frame, lightColor, rotation, orig, scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, drawPos, frame, Color.White, rotation, orig, scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
	