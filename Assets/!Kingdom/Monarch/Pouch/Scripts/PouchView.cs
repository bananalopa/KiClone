using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

namespace Kingdom.Monarch
{
	public class PouchView : MonoBehaviour
	{
		[SerializeField] private Text coinsCounterText;
		
		public void SetCoinAmount(int amount)
		{
			coinsCounterText.text = amount.ToString();
		}
	}
}