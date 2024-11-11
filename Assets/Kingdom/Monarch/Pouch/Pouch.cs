using System;
using Kingdom.DataStructures;
using UnityEngine;

namespace Kingdom.Monarch
{
	[Serializable]
	public class Pouch
	{
		[SerializeField] private IntReference maxCoinsCapacity;
		[SerializeField] private int currentCoinCount;

		//public IReactiveProperty<int> CurrentCoinCount => currentCoinCount;

		public bool IsEnoughCoins(int amount=1)
		{
			return currentCoinCount >= amount;
		}

		public bool IsEnoughSpace(int amount=1)
		{
			return maxCoinsCapacity.Value - currentCoinCount >= amount;
		}
		
		public bool AddCoins(int amount=1)
		{
			if (!IsEnoughSpace(amount))
				return false;
			currentCoinCount += amount;
			return true;
		}
		
		public bool RemoveCoins(int amount=1)
		{
			if (!IsEnoughCoins(amount))
				return false;
			currentCoinCount -= amount;
			return true;
		}
		
		
	}
}