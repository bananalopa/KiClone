using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kingdom
{
	[Serializable]
	public class BuildingSetting
	{
		[SerializeField] Sprite sprite;

		public Sprite Sprite1 => sprite;
	}
}