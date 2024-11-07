using UnityEngine;

namespace Kingdom
{
	[CreateAssetMenu(fileName = "TownCenterSettingSo", menuName = "Kingdom/TownCenterSettingSo", order = 1)]
	public class TownCenterSettingSo : BuildingUpgradableSettingSo
	{
		[SerializeField] TownCenterSetting townCenterSetting;

		public TownCenterSetting TownCenterSetting => townCenterSetting;
	}
}