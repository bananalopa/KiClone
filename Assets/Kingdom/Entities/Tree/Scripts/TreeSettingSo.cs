using UnityEngine;

namespace Kingdom.Entities
{
	[CreateAssetMenu(fileName = "TreeSettingSo", menuName = "Kingdom/Entities/TreeSettingSo", order = 1)]
	public class TreeSettingSo : ScriptableObject
	{
		[SerializeField] TreeSetting treeSetting;

		public TreeSetting Setting => treeSetting;
	}
}