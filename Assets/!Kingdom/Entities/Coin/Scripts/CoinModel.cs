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
			Reset = 0,
			Dropping = 10,
			Lying = 20,
			Picking = 30,
			Disappearing = 40,
			FallingIntoWater = 50
		}
	}
}