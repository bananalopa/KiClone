using System;
using UnityEngine;

namespace Kingdom
{
	[Serializable]
	public class InteractableSetting
	{
		[SerializeField] private int interactionPrice;

		public int InteractionPrice => interactionPrice;
	}
}