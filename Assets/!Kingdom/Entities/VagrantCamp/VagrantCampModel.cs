using System;
using System.Collections.Generic;
using Kingdom.Peasants;
using UnityEngine;

namespace Kingdom.Entities
{
	[Serializable]
	public class VagrantCampModel
	{
		[SerializeField] private List<VagrantPresenter> vagrants = new List<VagrantPresenter>();
		[SerializeField] private bool isMarkedForDestruction;

		public bool IsMarkedForDestruction
		{
			get => isMarkedForDestruction;
			set => isMarkedForDestruction = value;
		}
		
		void AddVagrant(VagrantPresenter vagrant)
		{
			vagrants.Add(vagrant);
		}

		void RemoveVagrant(VagrantPresenter vagrant)
		{
			vagrants.Remove(vagrant);
		}
		
		
	}
}