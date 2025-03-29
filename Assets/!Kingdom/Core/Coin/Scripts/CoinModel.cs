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

		public bool IsPickable => (state.Value == CoinStateEnum.Idle);
		
		public SerializableReactiveProperty<CoinStateEnum> State => state;


		public enum CoinStateEnum
		{
			Deactivated = 0,
			Idle = 20,
			Picking = 40
		}
	}
}