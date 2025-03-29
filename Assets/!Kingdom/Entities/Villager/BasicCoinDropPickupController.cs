using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Kingdom.Entities
{
	public class BasicCoinDropPickupController : MonoBehaviour
	{
		[SerializeField] private Transform coinDropStart;
		[SerializeField] private Transform checkingPoint;
		[SerializeField] private FloatReference coinPickupRadius;
		[SerializeField] LayerMask layerMask;
		[SerializeField] protected PouchModel pouchModel;
		[SerializeField] Color gizmosColor;

		[SerializeField] List<CoinDropData> coinsDroppedByThisEntityAndUnavailableForPickupYet;
		

		SharedSettings sharedSettings;
		CoinPool coinPool;
		CoinSetting coinSetting;

		public PouchModel Model => pouchModel;

		[Inject]
		void Construct(SharedSettings sharedSettings,
			CoinPool coinPool,
			CoinSetting coinSetting)
		{
			this.sharedSettings = sharedSettings;
			this.coinPool = coinPool;
			this.coinSetting = coinSetting;
		}
		
		private void OnDrawGizmos()
		{
			Gizmos.color = gizmosColor;
			Gizmos.DrawSphere(checkingPoint.position, coinPickupRadius.Value);
		}
		
		private void FixedUpdate()
		{
			var availableSpace = Model.AvailableSpace();
			if (availableSpace <=0) return;
			
			CheckNearbyCoins(availableSpace);
		}

		void CheckNearbyCoins(int availableSpace)
		{
			var collider2Ds = Physics2D.OverlapCircleAll(checkingPoint.position, coinPickupRadius.Value, layerMask);
			var coinsNearby = collider2Ds.
				Select(col => col.GetComponent<CoinPresenter>()).ToList();
			
			if (coinsNearby.Count == 0)
				return;
			
			coinsDroppedByThisEntityAndUnavailableForPickupYet = coinsDroppedByThisEntityAndUnavailableForPickupYet.Where(coinDropData=> Time.time < coinDropData.DropTime + coinSetting.RePickupTimeout).ToList();
			
			var droppedAndAvailableForPickup = coinsDroppedByThisEntityAndUnavailableForPickupYet.Where(coin => coin.DropTime + coinSetting.RePickupTimeout < Time.time);
			coinsDroppedByThisEntityAndUnavailableForPickupYet = coinsDroppedByThisEntityAndUnavailableForPickupYet.Except(droppedAndAvailableForPickup).ToList();
			var shouldBePickedUpCoins = coinsNearby.Except(coinsDroppedByThisEntityAndUnavailableForPickupYet.Select(dropData => dropData.CoinPresenter))
				.Where(coin=>coin.Model.IsPickable)
				.Take(availableSpace).ToList();
			shouldBePickedUpCoins.ForEach(PickupCoin);
		}

		void PickupCoin(CoinPresenter coin)
		{
			coin.Deactivate();
			pouchModel.TryAddCoins();
		}
		
		public void DropCoins(int amount)
		{
			for (int i = 0; i < amount; i++)
				DropCoin();
		}
		
		public void DropCoin()
		{
			var coin = coinPool.Get();
			coinsDroppedByThisEntityAndUnavailableForPickupYet.Add(new CoinDropData(coin));
			coin.transform.position = coinDropStart.position;
		}

		[Serializable]
		public class CoinDropData
		{
			[SerializeField] private float dropTime;
			[SerializeField] private CoinPresenter coinPresenter;

			public CoinDropData(CoinPresenter coinPresenter)
			{
				this.coinPresenter = coinPresenter;
				dropTime = Time.time;
			}

			public float DropTime => dropTime;

			public CoinPresenter CoinPresenter => coinPresenter;
		}
	}
}