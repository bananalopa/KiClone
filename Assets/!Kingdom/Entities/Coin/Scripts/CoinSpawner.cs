using UnityEngine;
using Zenject;

namespace Kingdom.Entities
{
	public class CoinSpawner : MonoBehaviour
	{
		CoinPool coinPool;
		
		[Inject]
		void Construct(CoinPool coinPool)
		{
			this.coinPool = coinPool;
		}
		
		void Update()
		{
			var coinPresenter = coinPool.Get();
			coinPresenter.transform.position = transform.position;
		}
	}
}