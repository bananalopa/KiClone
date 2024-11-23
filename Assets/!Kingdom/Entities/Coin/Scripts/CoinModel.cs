using System;
using UnityEngine;

namespace Kingdom.Entities
{
	[Serializable]
	public class CoinModel
	{
		[SerializeField] private float dropTime;

		public float DropTime
		{
			get => dropTime;
			set => dropTime = value;
		}
	}
}