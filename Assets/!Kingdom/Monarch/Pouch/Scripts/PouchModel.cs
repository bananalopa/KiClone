using System;
using UnityEngine;
using R3;

namespace Kingdom.Monarch
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
		
		public bool IsEnoughCoins(int amount=1)
		{
			return currentCoinCount.Value >= amount;
		}

		public bool IsEnoughSpace(int amount=1)
		{
			return maxCoinsCapacity.Value - currentCoinCount.Value >= amount;
		}
		
		public bool AddCoins(int amount=1)
		{
			if (!IsEnoughSpace(amount))
				return false;
			currentCoinCount.Value += amount;
			return true;
		}
		
		public bool RemoveCoins(int amount=1)
		{
			if (!IsEnoughCoins(amount))
				return false;
			currentCoinCount.Value -= amount;
			return true;
		}
	}
}