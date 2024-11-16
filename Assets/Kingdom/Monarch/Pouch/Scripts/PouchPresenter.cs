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

		public void AddCoins(int amount = 1)
		{
			pouchModel.AddCoins(amount);
		}
		
		public void DropCoins(int amount = 1)
		{
			pouchModel.RemoveCoins(amount);
		}
		
		public void PayCoins(int amount = 1)
		{
			pouchModel.RemoveCoins(amount);
		}
		
	}
}