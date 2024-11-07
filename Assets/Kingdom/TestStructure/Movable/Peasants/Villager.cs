using System;
using UnityEngine;

namespace Kingdom.Peasants
{
	[Serializable]
	public class Villager : MonoBehaviour, IMovable, ICoinsSpawner
	{
		[SerializeField] private int coins;
		public virtual void Move()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SpawnCoins()
		{
			throw new System.NotImplementedException();
		}
	}
}