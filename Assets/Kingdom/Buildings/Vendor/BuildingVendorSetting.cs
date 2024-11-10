using System;
using Kingdom.DataStructures;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom
{
	[Serializable]
	public class BuildingVendorSetting : BuildingSetting
	{
		[SerializeField] private IntReference vendorPrice;
		[SerializeField] private IntReference maxQuantity;

		public IntReference VendorPrice => vendorPrice;
		public IntReference MaxQuantity => maxQuantity;
	}
}