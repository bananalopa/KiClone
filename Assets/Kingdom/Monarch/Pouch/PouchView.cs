using System;
using Kingdom.Monarch;
using TMPro;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;
namespace Kingdom.Monarch
{
	public class PouchView : MonoBehaviour
	{
		[SerializeField] private Text coinsCounterText;
		[SerializeField] Pouch pouch = new Pouch();


		private void Awake()
		{
			//pouch.CurrentCoinCount.AsObservable().Subscribe(x => coinsCounterText.text = x.ToString()).AddTo(this);
		}

		
		
		public void AddCoin(int amount)
		{
			
		}
		
		public void DropCoin()
		{
			
		}
		
	}
}