using System;
using R3;
using UnityEngine;
using Zenject;

namespace Kingdom.Entities
{
	public class CoinPresenter : MonoBehaviour
	{
		[SerializeField] CoinModel coinModel;
		[SerializeField] CoinView coinView;
		[SerializeField] InteractableObject interactableObject;

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
			interactableObject.OnInteract().Subscribe(_ =>
			{
				Destroy(gameObject);
			}).AddTo(this);

			coinView.OnDissapear.Subscribe(_ =>
			{
				Destroy(gameObject);
			});

		}
		
	}
}