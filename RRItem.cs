using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace RedeadRequip {
	public class RRItem : GlobalItem {
		public override bool OnPickup(Item item, Player player) {
			RRPlayer person = player.GetModPlayer<RRPlayer>();
			RRClientConfig config = RedeadRequip.clientConfig;
			Item[] preDeathInv = person.preDeathInv;
			Item[] preDeathArmor = person.preDeathArmor;
			Item[] preDeathMiscEquips = person.preDeathMiscEquips;
			Item[] preDeathDyes = person.preDeathDyes;
			Item[] preDeathMiscDyes = person.preDeathMiscDyes;
			if (preDeathArmor != null) {
				if (item.headSlot + item.bodySlot + item.legSlot > 0) {
					if (config.allowArmorEquip && checkItem(item, true, player, preDeathArmor, player.armor, config, 3)) return true; // Non vanity armor slots are 0-2
					if (config.allowVanityArmorEquip && checkItem(item, true, player, preDeathArmor, player.armor, config, 13, 10)) return true; // vanity armor slots are 10-12
				}
				if (item.accessory) {
					if (config.allowAccessoryEquip && checkItem(item, false, player, preDeathArmor, player.armor, config, 10, 3)) return true; // non vanity accessory slots are 3-9
					if (config.allowVanityAccessoryEquip && checkItem(item, false, player, preDeathArmor, player.armor, config, 20, 13)) return true; // vanity accessory slots are 13-19
				}
			}
			if (item.dye > 0 && config.allowDyeEquip) {
				if (preDeathDyes != null && checkItem(item, true, player, preDeathDyes, player.dye, config)) return true;
				if (preDeathMiscDyes != null && checkItem(item, true, player, preDeathMiscDyes, player.miscDyes, config)) return true;
			}
			if (item.ammo > 0 && !item.notAmmo && config.allowAmmoEquip && preDeathInv != null && checkItem(item, false, player, preDeathInv, player.inventory, config, 58, 54)) return true; // Ammo slots are 54-57
			if ((item.mountType > 0 || item.shoot > 0) && config.allowMiscEquip && preDeathMiscEquips != null && checkItem(item, true, player, preDeathMiscEquips, player.miscEquips, config)) return true;
			if ((config.allowInventoryEquip || config.allowHotbarEquip) && preDeathInv != null && checkItem(item, true, player, preDeathInv, player.inventory, config, config.allowInventoryEquip ? player.inventory.Length : 10)) return true;
			return true;
		}

		private bool checkItem(Item itemIn, bool checkMainInvFirst, Player player, Item[] preDeathInv, Item[] currentInv, RRClientConfig config, int end = Int32.MaxValue, int start = 0) {
			end = Math.Min(preDeathInv.Length, end);
			for (int i = start; i < end; i++) {
				if (!itemIn.IsTheSameAs(preDeathInv[i])) continue;
				preDeathInv[i].TurnToAir();
				if (!currentInv[i].IsAir) {
					if (!config.allowItemReplacement) return false;
					int firstEmpty = -1;
					bool isMainInvSlot = false;
					if (!checkMainInvFirst) firstEmpty = Array.FindIndex(currentInv.Skip(start-1).Take(end - start).ToArray(), slot => slot.IsAir);
					if (firstEmpty == -1) {
						// 0-49 are main slots. The last 9 slots are the coin, ammo, and trash slots. We don't want to put replaced items there.
						firstEmpty = Array.FindIndex(player.hbLocked ? player.inventory.Skip(10).Take(40).ToArray() : player.inventory.Take(50).ToArray(), slot => slot.IsAir) + (player.hbLocked ? 10 : 0);
						isMainInvSlot = true;
					}
					if (firstEmpty == -1 && !config.allowItemDrop) return false;
					if (firstEmpty > -1) {
						if (isMainInvSlot) {
							player.inventory[firstEmpty] = currentInv[i];
						} else {
							currentInv[firstEmpty] = currentInv[i];
						}
					} else {
						Item.NewItem(player.getRect(), currentInv[i].type);
					}
				}
				if (itemIn.dye == 0) {
					currentInv[i] = itemIn.Clone();
					itemIn.TurnToAir();
				} else {
					Item singleDye = itemIn.Clone();
					singleDye.stack = 1;
					currentInv[i] = singleDye;
					itemIn.stack--;
					if (itemIn.stack > 0) return checkItem(itemIn, checkMainInvFirst, player, preDeathInv, currentInv, config, end, start);
				}
				return true;
			}
			if (itemIn.dye > 0 && currentInv == player.dye) return checkItem(itemIn, checkMainInvFirst, player, player.GetModPlayer<RRPlayer>().preDeathMiscDyes, player.miscDyes, config);
			return false;
		}
	}
}