using Kingdom.Entities;
using Kingdom.Input;
using R3;
using UnityEngine;
using Zenject;

namespace Kingdom.Monarch
{
	public class MonarchCoinPickupController : VillagerCoinDropController
	{
		private InputHandler inputHandler;
		
		[Inject]
		private void Construct(InputHandler inputHandler)
		{
			this.inputHandler = inputHandler;
		}


		private void Start()
		{
			inputHandler.DownTap.Subscribe(_ =>
			{
				if (Model.TryRemoveCoins())
					DropCoin();
			});
		}

	}
}