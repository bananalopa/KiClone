using R3;
using UnityEngine;

namespace Kingdom
{
	public class Approachable : MonoBehaviour
	{
		public Subject<bool> OnApproach = new();
		public SerializableReactiveProperty<bool> isApproached  = new();
	
		public void Approach(bool isIn)
		{
			if (isIn == isApproached.Value)
				return;
			isApproached.Value = isIn;
			OnApproach.OnNext(isIn);
		}
	}
}