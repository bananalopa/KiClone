using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom
{
	[Serializable]
	public class SharedSettings
	{
		[SerializeField] private FloatReference fadeTime;

		public float FadeTime => fadeTime.Value;
	}
}