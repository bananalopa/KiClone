using System;
using Kingdom.Entities;
using R3;
using UnityEngine;

namespace Kingdom.Monarch
{
	public class MonarchPouchPresenter : MonoBehaviour
	{
		[SerializeField] private VillagerCoinDropController villagerCoinDropController;
		[SerializeField] private MonarchPouchView view;

		private void Awake()
		{
			villagerCoinDropController.Model.currentCoinCount.Subscribe(count =>
				{
					view.SetCoinAmount(count);
				}).
				AddTo(this);
		}

		public bool AddCoins(int amount = 1)
		{
			return villagerCoinDropController.Model.TryAddCoins(amount);
			
			//add dropping coins into water if pouch capacity exceeded
		}
		
		public bool TryRemoveCoins(int amount = 1)
		{
			return villagerCoinDropController.Model.TryRemoveCoins(amount);
		}
		
	}
}