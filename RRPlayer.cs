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
			Item[] invClone = (Item[])player.inventory.Clone();
			if (deadman.preDeathInv != null) {
				for (int i = 0; i < invClone.Length; i++) {
					if (!invClone[i].IsAir) deadman.preDeathInv[i] = invClone[i];
				}
			} else {
				deadman.preDeathInv = invClone;
			}

			Item[] armorClone = (Item[])player.armor.Clone();
			if (deadman.preDeathArmor != null) {
				for (int i = 0; i < armorClone.Length; i++) {
					if (!armorClone[i].IsAir) deadman.preDeathArmor[i] = armorClone[i];
				}
			} else {
				deadman.preDeathArmor = armorClone;
			}

			Item[] miscEquipsClone = (Item[])player.miscEquips.Clone();
			if (deadman.preDeathMiscEquips != null) {
				for (int i = 0; i < miscEquipsClone.Length; i++) {
					if (!miscEquipsClone[i].IsAir) deadman.preDeathMiscEquips[i] = miscEquipsClone[i];
				}
			} else {
				deadman.preDeathMiscEquips = miscEquipsClone;
			}

			Item[] dyesClone = (Item[])player.dye.Clone();
			if (deadman.preDeathDyes != null) {
				for (int i = 0; i < dyesClone.Length; i++) {
					if (!dyesClone[i].IsAir) deadman.preDeathDyes[i] = dyesClone[i];
				}
			} else {
				deadman.preDeathDyes = dyesClone;
			}

			Item[] miscDyesClone = (Item[])player.miscDyes.Clone();
			if (deadman.preDeathMiscDyes != null) {
				for (int i = 0; i < miscDyesClone.Length; i++) {
					if (!miscDyesClone[i].IsAir) deadman.preDeathMiscDyes[i] = miscDyesClone[i];
				}
			} else {
				deadman.preDeathMiscDyes = miscDyesClone;
			}

			return true;
		}
	}
}