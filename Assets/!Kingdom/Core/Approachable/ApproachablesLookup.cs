using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kingdom
{
	public class ApproachablesLookup: MonoBehaviour
	{
		[SerializeField] private Transform checkingPoint;
		[SerializeField] private LayerMask layerMask;
		[SerializeField] Color gizmosColor;
		[SerializeField] private FloatReference radius;
		[SerializeField] List<Approachable> previousTickApproachables;

		public Subject<List<Approachable>> OnApproachablesChange = new();
		public List<Approachable> Approachables => previousTickApproachables;

		private void OnDrawGizmos()
		{
			Gizmos.color = gizmosColor;
			Gizmos.DrawSphere(checkingPoint.position, radius.Value);
		}

		private void FixedUpdate()
		{
			CheckApproachables();
		}

		void CheckApproachables()
		{
			var collider2Ds = Physics2D.OverlapCircleAll(checkingPoint.position, radius.Value, layerMask);

			var approachables = collider2Ds.
				OrderBy(col => Vector3.Magnitude(col.transform.position - checkingPoint.transform.position)).
				Select(col => col.GetComponent<Approachable>()).ToList();

			var notApproachablesAnymore = Approachables.Except(approachables).ToList();
			notApproachablesAnymore.ForEach(approachable => approachable.Approach(false));
			
			approachables.
				//Where(approachable => approachable.InteractableObject.isInteractable.Value).
				ForEach(approachable =>
				{
					approachable.Approach(true);
					//Debug.Log($"Overlap {approachable.name}");
				});


			bool listsAreEqual = previousTickApproachables.Count == approachables.Count;
			if (listsAreEqual)
				approachables.ForEach(a =>
				{
					int index = approachables.IndexOf(a);
					if (approachables[index] != previousTickApproachables[index])
						listsAreEqual = false;
				});
			
			if (!listsAreEqual)
				OnApproachablesChange.OnNext(approachables);
			
			previousTickApproachables = approachables;
		}
	}
}