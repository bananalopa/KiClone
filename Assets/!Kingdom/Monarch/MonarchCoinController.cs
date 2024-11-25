using Kingdom.Entities;
using Kingdom.Input;
using R3;
using UnityEngine;
using Zenject;

namespace Kingdom.Monarch
{
	public class MonarchCoinController : MonoBehaviour
	{
		[SerializeField] private Transform coinDropStart;
		
		private PouchPresenter pouchPresenter;
		private InputHandler inputHandler;
		private CoinPool coinPool;
		
		[Inject]
		private void Construct(PouchPresenter pouchPresenter,
			InputHandler inputHandler,
			CoinPool coinPool)
		{
			this.pouchPresenter = pouchPresenter;
			this.inputHandler = inputHandler;
			this.coinPool = coinPool;
		}


		private void Start()
		{
			inputHandler.DownTap.Subscribe(_ =>
			{
				if (pouchPresenter.TryRemoveCoins())
					DropCoin();
			});
		}

		void DropCoin()
		{
			var coinPresenter = coinPool.Get();
			coinPresenter.transform.position = coinDropStart.position;
		}
	}
}