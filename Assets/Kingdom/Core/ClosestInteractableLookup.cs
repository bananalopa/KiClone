using System;
using System.Linq;
using Kingdom.DataStructures;
using UnityEngine;

namespace Kingdom
{
	public class ClosestInteractableLookup: MonoBehaviour
	{
		[SerializeField] private Transform checkingPoint;
		[SerializeField] private LayerMask interactableLayer;
		[SerializeField] Color gizmosColor;
		
		[SerializeField] private FloatReference radius;

		private void OnDrawGizmos()
		{
			Gizmos.color = gizmosColor;
			Gizmos.DrawSphere(checkingPoint.position, radius.Value);
		}

		void CheckInteractables()
		{
			var collider2Ds = Physics2D.OverlapCircleAll(checkingPoint.position, radius.Value, interactableLayer);
			collider2Ds.OrderBy(col=> Vector3.Magnitude(col.transform.position - checkingPoint.transform.position)).
				Select(col=> col.GetComponent<Interactable>()).ToList().ForEach(interactable =>
			{
				
			});
		}
		
	}
}