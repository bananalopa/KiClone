using System;
using R3;
using UnityEngine;
using Zenject;

namespace Kingdom.Entities
{
	public class CoinPresenter : MonoBehaviour, IPoolable
	{
		[SerializeField] CoinModel coinModel;
		[SerializeField] CoinView coinView;
		[SerializeField] private Rigidbody2D rigidBody;

		CoinSetting coinSetting;
		CoinPool coinPool;

		public CoinModel Model => coinModel;

		[Inject]
		void Construct(CoinSetting coinSetting, CoinPool coinPool)
		{
			this.coinSetting = coinSetting;
			this.coinPool = coinPool;
		}

		private void Awake()
		{
			coinView.SetColor(coinSetting.GetRandomColor());
		}

		private void Start()
		{
			coinView.OnShown.Where(isShown=>!isShown).Subscribe(_ =>
			{
				coinPool.Release(this);
			}).AddTo(this);
		}

		public void Activate()
		{
			coinModel.State.Value = CoinModel.CoinStateEnum.Idle;
			rigidBody.bodyType = RigidbodyType2D.Dynamic;
			coinView.Show(); 
		}

		public void Deactivate()
		{
			coinModel.State.Value = CoinModel.CoinStateEnum.Deactivated;
			rigidBody.bodyType = RigidbodyType2D.Static;
			coinView.Hide();
		}

		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}