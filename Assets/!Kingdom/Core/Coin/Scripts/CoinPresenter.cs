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


		public void Activate()
		{
			coinModel.State.Value = CoinModel.CoinStateEnum.Dropping;
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