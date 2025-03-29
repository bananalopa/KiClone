using UnityEngine;
using Zenject;

namespace Kingdom.Entities
{
	public class CoinPool : GenericPool<CoinPresenter>
	{
		protected CoinPool(DiContainer container, CoinSetting coinSetting) : base(container)
		{
			prefab = coinSetting.CoinPrefab.gameObject;
		}
	}
}