using System;
using UnityEngine;

namespace Kingdom.Entities
{
	[Serializable]
	public class TreeModel
	{
		[SerializeField] private TreeStateEnum state;
		
		public TreeStateEnum State
		{
			get => state;
			set => state = value;
		}

		public enum TreeStateEnum
		{
			Normal = 0,
			Marked = 1,
			IsCutting = 2,
			Falling = 3
		}
		
	}
}