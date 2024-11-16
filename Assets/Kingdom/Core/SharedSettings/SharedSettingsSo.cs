using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom
{
	
	[CreateAssetMenu(fileName = "SharedSettingsSo", menuName = "Kingdom/SharedSettingsSo", order = 1)]
	public class SharedSettingsSo: ScriptableObject
	{
		[SerializeField] SharedSettings sharedSettings;

		public SharedSettings Settings => sharedSettings;
	}
}