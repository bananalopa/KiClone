using System;
using UnityEngine;

namespace Kingdom
{
	[Serializable]
	public class TownCenterSetting
	{
		[SerializeField] private int taxChestDailyIncome;
		
		public int TaxChestDailyIncome => taxChestDailyIncome;
	}
}