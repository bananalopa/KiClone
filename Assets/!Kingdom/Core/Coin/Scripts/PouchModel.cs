using System;
using UnityEngine;
using R3;

namespace Kingdom.Entities
{
	[Serializable]
	public class PouchModel
	{
		[SerializeField] private IntReference maxCoinsCapacity;
		public SerializableReactiveProperty<int> currentCoinCount;

		public PouchModel(int currentCoinCount)
		{
			this.currentCoinCount = new SerializableReactiveProperty<int>(currentCoinCount);
		}
		
		public int AvailableSpace()
		{
			return maxCoinsCapacity.Value - currentCoinCount.Value;
		}
		
		public bool IsEnoughCoins(int amount=1)
		{
			return currentCoinCount.Value >= amount;
		}

		public bool IsEnoughSpace(int amount=1)
		{
			return maxCoinsCapacity.Value - currentCoinCount.Value >= amount;
		}
		
		public bool TryAddCoins(int amount=1)
		{
			if (!IsEnoughSpace(amount))
				return false;
			currentCoinCount.Value += amount;
			return true;
		}
		
		public bool TryRemoveCoins(int amount=1)
		{
			if (!IsEnoughCoins(amount))
				return false;
			currentCoinCount.Value -= amount;
			return true;
		}
	}
}