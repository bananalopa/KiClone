﻿using System;
using UnityEngine;

namespace Kingdom
{
	[Serializable]
	public class Upgrade
	{
		[SerializeField] private IntReference upgradeCost;
		[SerializeField] private TechAge minTechAge;
		[SerializeField] private TownCenterLevel minTownCenterLevel;
		[SerializeField] private BuildingUpgradableSettingSo upgradedBuildingSettingSo;
	}
}