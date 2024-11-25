using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom.Entities
{
	[Serializable]
	public class CoinModel
	{
		[SerializeField] CoinStateEnum state;
		[SerializeField] private float dropTime;

		public float DropTime
		{
			get => dropTime;
			set => dropTime = value;
		}

		public CoinStateEnum State
		{
			get => state;
			set => state = value;
		}


		public enum CoinStateEnum
		{
			Deactivated = 0,
			Dropping = 20,
			Lying = 30,
			Picking = 40,
			Disappearing = 50,
			FallingIntoWater = 60
		}
	}
}