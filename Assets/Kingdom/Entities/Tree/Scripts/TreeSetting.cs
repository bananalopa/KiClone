using System;
using UnityEngine;

namespace Kingdom.Entities
{
	[Serializable]
	public class TreeSetting
	{
		[SerializeField] private IntReference coinsIncome;
		[SerializeField] private IntReference cutPrice;

		public IntReference CoinsIncome => coinsIncome;
		public IntReference CutPrice => cutPrice;
	}
}