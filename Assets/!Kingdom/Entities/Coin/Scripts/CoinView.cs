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
		
		public Subject<Unit> OnDissapear;
		
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
		
		public void ShowDisappearance()
		{
			renderer.DOFadeOut(sharedSettings.FadeTime).OnComplete(()=>OnDissapear.OnNext(Unit.Default));
		}
	}
}