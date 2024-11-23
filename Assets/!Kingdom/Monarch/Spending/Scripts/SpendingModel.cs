using System;
using R3;
using UnityEngine;

namespace Kingdom.Monarch
{
	[Serializable]
	public class SpendingModel
	{
		[SerializeField] private SerializableReactiveProperty<int> coinsReserve = new();
		
		public Subject<IInteractable> OnSpend = new();
		
		public Subject<int> OnGetRefund = new();
		public SerializableReactiveProperty<int> CoinsReserve => coinsReserve;

		
		public void AddCoinsToReserve(IInteractable interactable, int amount = 1)
		{
			if (interactable == null)
				return;
			
			CoinsReserve.Value += amount;
			if (CoinsReserve.Value >= interactable.InteractionPrice())
			{
				CoinsReserve.Value -= interactable.InteractionPrice();
				OnSpend.OnNext(interactable);
			}
		}

		public int GetRefund()
		{
			OnGetRefund.OnNext(CoinsReserve.Value);
			var amount = CoinsReserve.Value;
			CoinsReserve.Value = 0;
			return amount;
		}
		
	}
}