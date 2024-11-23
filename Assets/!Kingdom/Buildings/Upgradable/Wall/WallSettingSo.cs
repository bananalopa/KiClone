using UnityEngine;

namespace Kingdom
{
	[CreateAssetMenu(fileName = "WallSettingSo", menuName = "Kingdom/WallSettingSo", order = 1)]
	public class WallSettingSo : BuildingUpgradableSettingSo
	{
		[SerializeField] WallSetting wallSetting;

		public WallSetting WallSetting => wallSetting;
	}
}