using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kingdom.Entities
{
	[Serializable]
	public class CoinSetting
	{
		[SerializeField] private CoinPresenter coinPrefab;
		[SerializeField] private List<Color> colors;
		[SerializeField] private FloatReference disappearTimeout;

		public FloatReference DisappearTimeout => disappearTimeout;

		public CoinPresenter CoinPrefab => coinPrefab;

		public Color GetRandomColor()
		{
			return colors[Random.Range(0, colors.Count)];
		}
	}
}