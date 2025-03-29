using System;
using R3;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom.Entities
{
	[Serializable]
	public class CoinModel
	{
		[SerializeField] SerializableReactiveProperty<CoinStateEnum> state;
		[SerializeField] private float dropTime;

		public float DropTime
		{
			get => dropTime;
			set => dropTime = value;
		}

		public SerializableReactiveProperty<CoinStateEnum> State => state;


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