using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Kingdom.Entities
{
	public class CoinPool
	{
		private ObjectPool<CoinPresenter> pool;
		private CoinSetting coinSetting;
		DiContainer container;

		private GameObject poolParent;
		
		CoinPool(CoinSetting coinSetting, DiContainer container)
		{
			this.container = container;
			this.coinSetting = coinSetting;
			poolParent = new GameObject("CoinPool");
			pool = new ObjectPool<CoinPresenter>(Create, OnGet, OnRelease, null, true, 200, int.MaxValue);
		}
		
		CoinPresenter Create()
		{
			var coinPresenter = container.InstantiatePrefab(coinSetting.CoinPrefab).GetComponent<CoinPresenter>();
			coinPresenter.transform.SetParent(poolParent.transform);
			coinPresenter.name = "Coin";
			return coinPresenter;
		}


		void OnGet(CoinPresenter presenter)
		{
			
		}
		
		void OnRelease(CoinPresenter presenter)
		{
			
		}
		
		public CoinPresenter Get()
		{
			return pool.Get();
		}
		
		public void Release(CoinPresenter coinPresenter)
		{
			pool.Release(coinPresenter);
		}
	}
}