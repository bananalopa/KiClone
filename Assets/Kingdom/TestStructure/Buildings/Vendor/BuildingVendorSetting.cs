using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom
{
	[Serializable]
	public class BuildingVendorSetting : BuildingSetting
	{
		[SerializeField] private int vendorPrice;
		[SerializeField] private int maxQuantity;

		public int VendorPrice => vendorPrice;
		public int MaxQuantity => maxQuantity;
	}
}