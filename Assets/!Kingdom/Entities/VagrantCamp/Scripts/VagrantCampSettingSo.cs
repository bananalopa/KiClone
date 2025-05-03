using UnityEngine;

namespace Kingdom.Entities
{
	[CreateAssetMenu(fileName = "VagrantCampSettingSo", menuName = "Kingdom/Entities/VagrantCampSettingSo", order = 1)]
	public class VagrantCampSettingSo: ScriptableObject
	{
		[SerializeField] VagrantCampSetting vagrantCampSetting;

		public VagrantCampSetting Setting => vagrantCampSetting;
	}
}