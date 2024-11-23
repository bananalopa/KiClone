using UnityEngine;

namespace Kingdom
{
	public class BuildingUpgradableSettingSo: ScriptableObject
	{
		[SerializeField] BuildingUpgradableSetting buildingUpgradableSetting;
		public BuildingUpgradableSetting BuildingUpgradableSetting => buildingUpgradableSetting;
	}
}