using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kingdom
{
	[Serializable]
	public class Upgrade
	{
		[SerializeField] private int upgradeCost;
		[SerializeField] private TechAge minTechAge;
		[SerializeField] private TownCenterLevel minTownCenterLevel;
		[SerializeField] private BuildingUpgradableSettingSo upgradedBuildingSettingSo;

		
	}
	
}