using UnityEngine;

namespace Kingdom
{
	
	[CreateAssetMenu(fileName = "BuildingVendorSettingSo", menuName = "Kingdom/BuildingVendorSettingSo", order = 1)]
	public class BuildingVendorSettingSo : ScriptableObject
	{
		[SerializeField] private BuildingVendorSetting buildingVendorSetting;
		
		public BuildingVendorSetting BuildingVendorSetting => buildingVendorSetting;
	}
}