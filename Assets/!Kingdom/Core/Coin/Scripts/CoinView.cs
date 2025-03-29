using System;
using DG.Tweening;
using R3;
using UnityEngine;
using Zenject;

namespace Kingdom.Entities
{
	public class CoinView: MonoBehaviour
	{
		[SerializeField] private SpriteRenderer renderer;
		
		public Subject<bool> OnShown = new();
		
		private SharedSettings sharedSettings;
		
		[Inject]
		void Construct(SharedSettings sharedSettings)
		{
			this.sharedSettings = sharedSettings;
		}

		public void SetColor(Color color)
		{
			renderer.color = color;
		}
		
		public void Hide()
		{
			renderer.DOFadeOut(sharedSettings.FadeTime).OnComplete(()=>OnShown.OnNext(false));
		}
		
		public void Show()
		{
			renderer.DOFadeIn(sharedSettings.FadeTime, isStartValueOverwritten: true).OnComplete(()=>OnShown.OnNext(true));
		}
	}
}