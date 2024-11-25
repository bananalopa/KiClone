using System;
using R3;
using UnityEngine;

namespace Kingdom.Monarch
{
	public class PouchPresenter : MonoBehaviour
	{
		[SerializeField] private PouchModel pouchModel;
		[SerializeField] private PouchView view;

		private void Awake()
		{
			pouchModel.currentCoinCount.Subscribe(count =>
				{
					view.SetCoinAmount(count);
				}).
				AddTo(this);
		}

		public bool AddCoins(int amount = 1)
		{
			return pouchModel.AddCoins(amount);
			
			//add dropping coins into water if pouch capacity exceeded
		}
		
		public bool TryRemoveCoins(int amount = 1)
		{
			return pouchModel.RemoveCoins(amount);
		}
		
	}
}