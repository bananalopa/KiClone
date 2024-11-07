using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kingdom
{
	[Serializable]
	public class BuildingUpgradableSetting : BuildingSetting
	{
		[SerializeField] List<Upgrade> possibleUpgrades;
		public List<Upgrade> PossibleUpgrades => possibleUpgrades;
	}
}