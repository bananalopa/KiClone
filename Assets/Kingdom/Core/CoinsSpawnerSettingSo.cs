using System;
using UnityEngine;

namespace Kingdom
{
	public class CoinsSpawnerSettingSo : ScriptableObject
	{
		[SerializeField] CoinsSpawnerSetting coinsSpawnerSetting;
	}
	
	[Serializable]
	public class CoinsSpawnerSetting
	{
		[SerializeField] private int amountToDrop;

		public int AmountToDrop => amountToDrop;
	}
}