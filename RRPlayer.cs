using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RedeadRequip {
	public class RRPlayer : ModPlayer {

		public Item[] preDeathInv;
		public Item[] preDeathArmor;
		public Item[] preDeathMiscEquips;
		public Item[] preDeathDyes;
		public Item[] preDeathMiscDyes;
		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
			RRPlayer deadman = player.GetModPlayer<RRPlayer>();
			deadman.preDeathInv = (Item[]) player.inventory.Clone();
			deadman.preDeathArmor = (Item[]) player.armor.Clone();
			deadman.preDeathMiscEquips = (Item[]) player.miscEquips.Clone();
			deadman.preDeathDyes = (Item[]) player.dye.Clone();
			deadman.preDeathMiscDyes = (Item[]) player.miscDyes.Clone();
			return true;
		}
	}
}