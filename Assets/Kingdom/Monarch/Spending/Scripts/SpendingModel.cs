using System;
using R3;
using UnityEngine;

namespace Kingdom.Monarch
{
	[Serializable]
	public class SpendingModel
	{
		[SerializeField] private int coinsReserve;
		
		private IInteractable interactable;
		public Subject<IInteractable> OnSpend;

		public SpendingModel(IInteractable interactable)
		{
			this.interactable = interactable;
			OnSpend = new();
		}
		
		public void AddCoinsToReserve(int amount = 1)
		{
			coinsReserve += amount;
			if (coinsReserve >= interactable.InteractionPrice())
			{
				coinsReserve -= interactable.InteractionPrice();
				OnSpend.OnNext(interactable);
			}
		}

		public int GetRefund()
		{
			int amount = coinsReserve;
			coinsReserve = 0;
			return amount;
		}

		public void Dispose()
		{
			OnSpend.Dispose();
		}
	}
}