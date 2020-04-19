using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace RedeadRequip {
	public class RRClientConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ClientSide;

		public override void OnLoaded() {
			RedeadRequip.clientConfig = this;
		}

		[Header("Item Replacement Rules")]
		[DefaultValue(true)]
		[Label("Allow item replacement")]
		[Tooltip("If an item exists in a slot when something tries to auto-equip there, the old item will be moved out of its spot, into a similar spot if possible, or otherwise, the main inventory")]
		public bool allowItemReplacement;

		[DefaultValue(true)]
		[Label("Allow item removal")]
		[Tooltip("If item replacement is true, and there isn't room in the main inventory for the replaced item, it will be dropped on the ground")]
		public bool allowItemDrop;

		[Header("Allowed Auto-Equip Sections")]
		[DefaultValue(true)]
		[Label("Allow auto-equipping to the hotbar")]
		public bool allowHotbarEquip;

		[DefaultValue(false)]
		[Label("Allow auto-equipping to the main inventory")]
		public bool allowInventoryEquip;

		[DefaultValue(true)]
		[Label("Allow auto-equipping ammo")]
		public bool allowAmmoEquip;

		[DefaultValue(true)]
		[Label("Allow auto-equipping non-vanity armor")]
		public bool allowArmorEquip;

		[DefaultValue(true)]
		[Label("Allow auto-equipping vanity armor")]
		public bool allowVanityArmorEquip;

		[DefaultValue(true)]
		[Label("Allow auto-equipping non-vanity accessories")]
		public bool allowAccessoryEquip;

		[DefaultValue(true)]
		[Label("Allow auto-equipping vanity accessories")]
		public bool allowVanityAccessoryEquip;

		[DefaultValue(true)]
		[Label("Allow auto-equipping dyes")]
		public bool allowDyeEquip;

		[DefaultValue(true)]
		[Label("Allow auto-equipping pets, minecarts, mounts, and grapple hooks")]
		public bool allowMiscEquip;
	}
}