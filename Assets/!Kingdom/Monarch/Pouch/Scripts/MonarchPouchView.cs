using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

namespace Kingdom.Monarch
{
	public class MonarchPouchView : MonoBehaviour
	{
		[SerializeField] private Text coinsCounterText;
		
		public void SetCoinAmount(int amount)
		{
			coinsCounterText.text = amount.ToString();
		}
	}
}