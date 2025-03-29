using System;
using UnityEngine;
using Zenject;
using Text = TMPro.TextMeshProUGUI;

namespace Kingdom.Monarch
{
	public class SpendingView : MonoBehaviour
	{
		[SerializeField] private Transform root;
		[SerializeField] CanvasGroup canvasGroup;
		[SerializeField] private Text priceText;
		[SerializeField] private Text reservedCoinsText;

		Camera camera;

		[Inject]
		void Construct(Camera camera)
		{
			this.camera = camera;
		}
		
		private void Awake()
		{
			Hide();
		}

		public void Show(int price, Vector3 pos)
		{
			priceText.text = price.ToString();
			reservedCoinsText.text = "";
			//root.position = camera.WorldToScreenPoint(pos);
			canvasGroup.alpha = 1;
		}

		public void Hide()
		{
			canvasGroup.alpha = 0;
		}
		
		public void SetReservedCoins(int coins)
		{
			reservedCoinsText.text = coins.ToString();
		}

		public void ResetReservedCoins()
		{
			reservedCoinsText.text = "";
		}
	}
}