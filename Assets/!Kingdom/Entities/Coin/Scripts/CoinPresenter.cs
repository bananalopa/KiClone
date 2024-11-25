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
		
		[Inject]
		void Construct(CoinSetting coinSetting)
		{
			this.coinSetting = coinSetting;
		}

		private void Awake()
		{
			coinView.SetColor(coinSetting.GetRandomColor());
		}

		private void Start()
		{
			coinView.OnDissapear.Subscribe(_ =>
			{
				Destroy(gameObject);
			});
		}

		public void Activate()
		{
			coinModel.State = CoinModel.CoinStateEnum.Activated;
			rigidBody.bodyType = RigidbodyType2D.Dynamic;
		}

		public void Deactivate()
		{
			coinModel.State = CoinModel.CoinStateEnum.Deactivated;
			rigidBody.bodyType = RigidbodyType2D.Static;
			coinView.Hide();
		}

		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}