using Terraria.ModLoader;

namespace RedeadRequip {
	public class RedeadRequip : Mod {
		public static RRClientConfig clientConfig;

		public override void Unload() {
			clientConfig = null;
		}
	}
}