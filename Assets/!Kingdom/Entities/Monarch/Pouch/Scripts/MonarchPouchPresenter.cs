using System;
using Kingdom.Entities;
using R3;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom.Monarch
{
	public class MonarchPouchPresenter : MonoBehaviour
	{
		[FormerlySerializedAs("basicCoinDroPickupController")] [SerializeField] private BasicCoinDropPickupController basicCoinDropPickupController;
		[SerializeField] private MonarchPouchView view;

		private void Awake()
		{
			basicCoinDropPickupController.Model.currentCoinCount.Subscribe(count =>
				{
					view.SetCoinAmount(count);
				}).
				AddTo(this);
		}

		public bool AddCoins(int amount = 1)
		{
			return basicCoinDropPickupController.Model.TryAddCoins(amount);
			
			//add dropping coins into water if pouch capacity exceeded
		}
		
		public bool TryRemoveCoins(int amount = 1)
		{
			return basicCoinDropPickupController.Model.TryRemoveCoins(amount);
		}
		
	}
}