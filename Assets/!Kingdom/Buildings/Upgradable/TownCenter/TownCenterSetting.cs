﻿using System;
using UnityEngine;

namespace Kingdom
{
	[Serializable]
	public class TownCenterSetting
	{
		[SerializeField] private IntReference taxChestDailyIncome;
		public IntReference TaxChestDailyIncome => taxChestDailyIncome;
	}
}