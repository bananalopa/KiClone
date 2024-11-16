using System;
using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening;
using UnityEngine.UI;
using Zenject;

namespace Kingdom.Entities
{
	public class TreeView : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer mark;

		SharedSettings sharedSettings;
		
		[Inject]
		void Construct(SharedSettings sharedSettings)
		{
			this.sharedSettings = sharedSettings;
		}
		
		private void Awake()
		{
			mark.DOFadeOut(sharedSettings.FadeTime).Complete();
		}

		public void ShowMark()
		{
			mark.DOFadeIn(sharedSettings.FadeTime);
		}
	}
}