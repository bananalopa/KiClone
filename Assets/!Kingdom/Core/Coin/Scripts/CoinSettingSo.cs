using UnityEngine;

namespace Kingdom.Entities
{
	[CreateAssetMenu(fileName = "CoinSettingSo", menuName = "Kingdom/Entities/CoinSettingSo", order = 1)]
	public class CoinSettingSo : ScriptableObject
	{
		[SerializeField] CoinSetting coinSetting;
		
		public CoinSetting Setting => coinSetting;
	}
}